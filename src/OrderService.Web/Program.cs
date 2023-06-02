using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using OrderService.Core;
using OrderService.Infrastructure;
using OrderService.Infrastructure.Data;
using OrderService.Web;
using Microsoft.OpenApi.Models;
//using Serilog;
using OrderService.Web.Interfaces;
using OrderService.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OrderService.Web.SignalR;
using StackExchange.Redis.Extensions.Newtonsoft;
using StackExchange.Redis.Extensions.Core.Configuration;
using Hangfire;
using Minio.AspNetCore;
using Minio;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
  .ConfigureServices((hostContext, services) =>
  {
    SignalRStartup.AddSignal(services);
  });

//builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});


string? connectionString = builder.Configuration["CONNECTION_STRING"];

if (connectionString == null)
{
  connectionString = builder.Configuration["databases:azure"];
}

Console.WriteLine(connectionString);

builder.Services.AddDbContext(connectionString!);

const string CORS_POLICY = "CorsPolicy";
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: CORS_POLICY,
                    corsPolicyBuilder =>
                    {
                      corsPolicyBuilder.WithOrigins("http://localhost:3000", "http://localhost:5001", "https://fastship-staging.sontran.us")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
                    });
});


var redisConfiguration = builder.Configuration.GetSection("Redis").Get<RedisConfiguration>();
redisConfiguration!.Hosts = new List<RedisHost>() {
  new RedisHost()
  {
    Host = builder.Configuration["HOSTNAME"]!,
    Port = 6379
  }
}.ToArray();

Console.WriteLine($"Using {redisConfiguration.Hosts[0].Host}:{redisConfiguration.Hosts[0].Port} for reddis host");

builder.Services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration!);

builder.Services.AddHangfire(configuration => 
  configuration.UseSqlServerStorage(connectionString));

builder.Services.AddHangfireServer();

builder.Services.AddMinio(options =>
{
  options.Endpoint = $"{builder.Configuration["HOSTNAME"]}:9000";

  options.ConfigureClient(client =>
  {
    var accessKey = builder.Configuration["MINIO_ACCESSKEY"];
    var secretKey = builder.Configuration["MINIO_SECRETKEY"];

    client.WithCredentials(accessKey, secretKey);
  });

});

builder.Services.AddControllers(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
  c.EnableAnnotations();
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
  });
  c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);

  // optional - default path to view services is /listallservices - recommended to choose your own path
  config.Path = "/listservices";
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHostedService<ConsumeProductResultHostedService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
  options.SaveToken = true;

  string key = builder.Configuration.GetSection("Jwt:Key").Get<String>()!;

  options.TokenValidationParameters = new TokenValidationParameters()
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidAudience = builder.Configuration["Jwt:Audience"],
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
  };


  //JWT authen for signalR hub
  options.Events = new JwtBearerEvents
  {
    OnMessageReceived = context =>
    {
      var accessToken = context.Request.Query["access_token"];

      var path = context.HttpContext.Request.Path;

      if (!string.IsNullOrEmpty(accessToken) &&
                  (path.StartsWithSegments("/hub") || path.StartsWithSegments("/chat"))
         )
      {
        // Read the token out of the query string
        context.Token = accessToken;
      }

      return Task.CompletedTask;
    }
};

});

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});

//builder.Logging.AddAzureWebAppDiagnostics(); add this if deploying to Azure

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware();
}
else
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}


app.UseRouting();
app.MapControllers();


app.UseCors(CORS_POLICY);

app.UseHttpsRedirection();

SignalRStartup.MapSignalR(app);


app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard();




// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));


// Seed Database
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<AppDbContext>();
    //                    context.Database.Migrate();
    context.Database.EnsureCreated();
    SeedData.Initialize(services);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  }
}

app.Run();
