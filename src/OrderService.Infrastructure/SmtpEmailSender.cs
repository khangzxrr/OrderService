﻿using System.Net.Mail;
using OrderService.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;
using Hangfire;

namespace OrderService.Infrastructure;

public class SmtpEmailSender : IEmailSender
{
  private readonly ILogger<SmtpEmailSender> _logger;

  public SmtpEmailSender(ILogger<SmtpEmailSender> logger)
  {
    _logger = logger;
  }

  public async Task HangFireSendEmail(string to, string subject, string body)
  {
    var emailClient = new SmtpClient("smtp.gmail.com", 587)
    {
      Credentials = new NetworkCredential("datnqse62453@fpt.edu.vn", "smgqgclejkmmnbcp"),
      EnableSsl = true
    };

    var message = new MailMessage
    {
      From = new MailAddress("datnqse62453@fpt.edu.vn"),
      Subject = subject,
      Body = body,
      IsBodyHtml = true
    };

    message.To.Add(new MailAddress(to));

    await emailClient.SendMailAsync(message);

    _logger.LogWarning($"Sending email to {to} with subject {subject}.");
  }

  public void SendEmail(string to, string subject, string body)
  {
    BackgroundJob.Enqueue(() => HangFireSendEmail(to,subject,body));
  }
}
