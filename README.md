# OrderService

Backend API for **FastShip**, a cross-border shopping service. Customers order products from US online stores, and staff handle quoting, payment, shipping to Vietnam and after-sales issues.

![.NET](https://img.shields.io/badge/.NET-7.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![RabbitMQ](https://img.shields.io/badge/RabbitMQ-FF6600?logo=rabbitmq&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-DC382D?logo=redis&logoColor=white)

## Overview

OrderService is an ASP.NET Core Web API built on the [Ardalis Clean Architecture](https://github.com/ardalis/CleanArchitecture) template. It covers the whole order lifecycle: from a pasted product URL, through price quotation, two-stage online payment, international and local delivery, and product return or refund. Product data is scraped on demand by a separate Selenium worker ([OrderServiceBot](https://github.com/khangzxrr/OrderServiceBot)). The two services talk over RabbitMQ, and results are pushed to the browser in real time over SignalR.

## Features

- **Product fetching from a URL.** A client sends a product URL through the `/hub` SignalR hub. The request goes onto the `eshop_queue` RabbitMQ queue. A hosted background service reads results from `eshop_result`, then saves the category, product, image and currency exchange rate, and pushes the product back to that client's SignalR connection. Fetched products are cached in Redis for 2 days so repeat lookups skip the scraper.
- **Order lifecycle.** Orders move through the states *no price quotation → not paid → waiting to order from seller → ordering from seller → delivering US → VN → in VN warehouse → delivering to customer → finished*. There are also *reselling*, *denied* and *failed delivery* states.
- **Role-based API.** Endpoints are grouped by role: Customer, Employee, Shipper, Manager and Admin. JWT bearer authentication is used, and roles and demo users are seeded on startup.
- **VNPay payments.** The API generates HMAC-SHA512-signed VNPay payment URLs for a first (deposit) and a second (remaining) payment. It also exposes an IPN callback endpoint that records completed payments.
- **Workload-based assignment.** New orders go to the employee with the fewest orders, and shipments go to the free shipper with the fewest deliveries.
- **Real-time order chat.** An authenticated `/chat` SignalR hub lets a customer and the assigned employee chat about an order, with email notifications for new messages.
- **Domain events and email notifications.** MediatR domain events (order status updated, price or weight updated, payment created, shipping created or updated) trigger emails. The emails are sent in the background with Hangfire.
- **Shipping and product issues.** Shippers can update their GPS position, set delivery status and collect the remaining cost. Customers can open return or refund requests, which follow a state machine (employee, customer or seller fault → refund / finish).
- **Media storage.** Uploaded files and scraped product images are stored in MinIO (S3-compatible).
- **Manager dashboard data.** Endpoints return orders, payments, shippers, customers and totals, and let managers set the USD currency exchange rate and the price table configuration.
- **Swagger / OpenAPI** docs with JWT support, and a Hangfire dashboard.

## Tech stack

| Area | Technologies |
| --- | --- |
| Framework | ASP.NET Core (.NET 7), C# |
| Architecture | Clean Architecture, DDD aggregates, Ardalis.Specification, Ardalis.ApiEndpoints, Ardalis.Result, SmartEnum |
| Data | Entity Framework Core, SQL Server |
| Messaging and real time | RabbitMQ (`RabbitMQ.Client`), SignalR |
| Caching | Redis (StackExchange.Redis.Extensions) |
| Background jobs | Hangfire (SQL Server storage) |
| Storage | MinIO |
| Other | MediatR, AutoMapper, Autofac, JWT bearer auth, Swashbuckle, VNPay |
| Tests | xUnit, Moq (unit, integration and functional projects) |
| Deployment | Docker (multi-stage `dotnet/sdk:7.0-alpine` → `aspnet:7.0-alpine`) |

## Project structure

```
src/
├── OrderService.Core            # Domain: aggregates (Order, Product, ProductIssue, OrderShipping,
│                                #   Shipper, User, Currency, Configuration), domain events & handlers,
│                                #   specifications, domain services, interfaces
├── OrderService.Infrastructure  # EF Core DbContext + configurations + migrations, repositories,
│                                #   VNPay PaymentService, MinIO MediaService, RabbitMQ producer, SMTP email sender
├── OrderService.SharedKernel    # Base entity, value objects, domain event dispatcher, repository interfaces
└── OrderService.Web             # API endpoints (grouped by role), SignalR hubs, hosted RabbitMQ consumer,
                                 #   JWT token service, AutoMapper profiles, seed data, Program.cs
tests/
├── OrderService.UnitTests
├── OrderService.IntegrationTests
└── OrderService.FunctionalTests
sql_queries/                     # Helper SQL scripts
```

Dependencies point inward: `Web → Infrastructure → Core → SharedKernel`. Autofac modules (`DefaultCoreModule`, `DefaultInfrastructureModule`) wire up the layers.

## Getting started

### Prerequisites

- [.NET SDK 7.0](https://dotnet.microsoft.com/download)
- SQL Server
- RabbitMQ (port 5672), Redis (port 6379) and MinIO (port 9000), all reachable at the same host
- [OrderServiceBot](https://github.com/khangzxrr/OrderServiceBot) running against the same RabbitMQ, to fetch products

### Configuration

Settings are read from `src/OrderService.Web/appsettings.json` and environment variables. Supply your own values and do not commit real credentials.

| Setting | Purpose |
| --- | --- |
| `CONNECTION_STRING` | SQL Server connection string (falls back to `databases:azure`). Also used for Hangfire storage |
| `HOSTNAME` | Host running RabbitMQ, Redis and MinIO |
| `MINIO_ACCESSKEY`, `MINIO_SECRETKEY` | MinIO credentials |
| `SERVER_ORIGIN` | Front-end origin used in email links |
| `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience` | JWT signing and validation |
| `VnPay:Url`, `VnPay:TmnCode`, `VnPay:HashSecret` | VNPay merchant configuration |
| `Redis:*` | Redis client options (password, database, timeouts) |

CORS allows `http://localhost:3000` and `http://localhost:5001` for local front-end development.

### Run locally

```bash
dotnet restore
dotnet run --project src/OrderService.Web
```

On startup the database is created with `EnsureCreated()`, and roles and demo users are seeded. Swagger UI is served at `/swagger`, and the Hangfire dashboard at `/hangfire`.

### Run with Docker

```bash
docker build -t orderservice .
docker run -p 5000:5000 \
  -e CONNECTION_STRING="..." -e HOSTNAME="..." \
  -e MINIO_ACCESSKEY="..." -e MINIO_SECRETKEY="..." \
  orderservice
```

### Tests

```bash
dotnet test
```

## Related projects

- [OrderServiceBot](https://github.com/khangzxrr/OrderServiceBot): Selenium worker that scrapes product pages requested through RabbitMQ.

## Credits

The solution layout is based on [ardalis/CleanArchitecture](https://github.com/ardalis/CleanArchitecture) by Steve Smith.

## License

Released under the MIT License. See [LICENSE](LICENSE).
