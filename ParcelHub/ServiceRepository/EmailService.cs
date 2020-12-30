using Microsoft.Extensions.Options;
using ParcelHub.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ParcelHub.ServiceRepository
{

    // provide service to Email consumer
    // All Email templates are in [EmailTemplates] Folder 
    // Each member function provide solutions for different senario
    // EmailService function is main 




    public class EmailService : IEmailService
    {
        private readonly SMTPConfig _smtpConfig;
        private const string templatePath = @"emailTemplates/{0}.html ";

        // inject SMTPConfig From service.  IConfig (register) => IOption (receive) 
        public EmailService(IOptions<SMTPConfig> smtpConfig) 
        {
            _smtpConfig = smtpConfig.Value;
        }


        // configure SMTP server config. ports/ password/ username all in appsetting.json
        private async Task SendEmail(UserEmailOption userEmailOption)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOption.SubjectLine,
                Body = userEmailOption.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            foreach (var toEmail in userEmailOption.Receiver)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential
                = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };
            mail.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }

        // testing only => used at HomeController => localhost/home/test
        public async Task SendtestEmail(UserEmailOption userEmailOption)
        {
            userEmailOption.SubjectLine = "Hello";
            userEmailOption.Body = GetEmailBody("templateForNewRegistationVerification");
            await SendEmail(userEmailOption);
        }


        public async Task SendConsumerAccountVerification(UserEmailOption userEmailOption)
        {
            // update what to be sent to consumer as verfication email content
            userEmailOption.SubjectLine = "Kiwi Parcel account Verification";
            userEmailOption.Body = UpdatePlaceHolderInTemplate
              (GetEmailBody("templateForNewRegistationVerification"), userEmailOption.PlaceHolder);

            //send email with configuration by userEmailOption
            await SendEmail(userEmailOption);
        }

        private string UpdatePlaceHolderInTemplate(string bodyString, List<KeyValuePair<string, string>> placeholder)
        {
            if (!string.IsNullOrEmpty(bodyString) && placeholder != null)
            {
                foreach (var element in placeholder)
                {
                    if (bodyString.Contains(element.Key))
                    {
                        bodyString = bodyString.Replace(element.Key, element.Value);
                    }
                }

            }
            return bodyString;
        }
    }
}
