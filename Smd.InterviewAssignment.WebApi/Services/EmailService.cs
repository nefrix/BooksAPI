using Microsoft.Extensions.Configuration;
using Smd.InterviewAssignment.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Smd.InterviewAssignment.WebApi.Services
{
    public class EmailService : IEmailService
    {
        IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmail(EmailDto email)
        {
            var emailClient = new SmtpClient()
            {
                Host = _config["EmailHost"],
                Port = int.Parse(_config["EmailPort"]),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_config["EmailUsername"], _config["EmailPassword"]),
                EnableSsl = true
            };
            await emailClient.SendMailAsync(email.Sender, email.Recipient, email.Subject, email.Body);
        }
    }
}
