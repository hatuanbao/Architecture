﻿using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Extensions.Mailer
{
    public class MailSender
    {
        public async Task SendEmail(string address, string userName, string subject, string body)
        {
            var fromAddress = new MailAddress(WebConfigurationManager.AppSettings["emailAdress"], WebConfigurationManager.AppSettings["displayName"]);
            var toAddress = new MailAddress(address, userName);

            var smtp = new SmtpClient
            {
                Port = 587,
                Timeout = 20000,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, WebConfigurationManager.AppSettings["emailPassword"])
            };

            using (var smtpClient = smtp)
            {
                await smtpClient.SendMailAsync(new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body });
            }
        }
    }
}