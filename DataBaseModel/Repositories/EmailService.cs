﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RepositoryModel.IRepository;


namespace DataBaseModel.Repositories
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("mo0278673@gmail.com", "wesf pluj ikwy ckoh")
            };

 

            return client.SendMailAsync(
                new MailMessage(from: "mo0278673@gmail.com",
                                to: email,
                                subject,
                                message
                                )
                {
                    IsBodyHtml = true //if you want to send message with html body
                }
                );
        }
    }
}