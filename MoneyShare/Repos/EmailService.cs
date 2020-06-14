using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MoneyShare.Models;

namespace MoneyShare.Repos
{
    public class EmailService : IEmailService
    {
        IConfiguration _configuration;
        private SmtpClient _client;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
            _client.Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"]);
            _client.EnableSsl = true;
        }
        public void EmailTwoFactorCode(MemberModel member)
        {
            string message = $"Hello {member.LastName}, {member.FirstName}\nYour 2FA code is: {member.TwoFactorCode}";
            SendEmailAsync(member.Email, "Authentication code", message);
        }
        public Task SendEmailAsync(string recipient, string subject, string message)
        {
            using (MailMessage mailMessage = new MailMessage(_configuration["Email:From"], recipient, subject, message))
            {
                try
                {
                    _client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email: {0}", ex.ToString());
                }
            }
            return null;
        }
    }
}