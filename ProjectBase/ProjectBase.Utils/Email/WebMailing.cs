using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.Security;
using log4net;
using log4net.Core;
using ProjectBase.Utils.MailSender;

namespace ProjectBase.Utils.Email
{
    /// <summary>
    /// Allow to send email through standard Smtp configuration of .NET Framework
    /// </summary>
    /// <remarks>
    /// You can inherint this class to modify the defaults for template usage.
    /// We currently suggest to base on the current settings for projects compatibility.
    /// </remarks>
    public class WebMailing : Mailing
    {
        private const string FROM_ADDRESS = "Mailing_FromAddress";
        private const string SITE_CODE = "Mailing_SiteCode";

        public override void SendMail(string to, string title, string subject, string body, bool sendToAdmins, bool async, string replyToAddress, string replyToName, string filename, byte[] filecontent)
        {
            string from = ConfigurationSettings.AppSettings[FROM_ADDRESS];

            if (string.IsNullOrEmpty(from))
                throw new Exception(FROM_ADDRESS + " not available on configuration file");

            string template = ApplyTemplate(title, subject, body);

            Mail mail = new Mail();
            if (!string.IsNullOrEmpty(filename))
                mail.SendEmailWithAttachment(from, to, subject, template, replyToAddress, replyToName, filename, filecontent);
            else
                mail.SendEmail(from, to, subject, template, replyToAddress, replyToName);
        }

        public void SendMail(string to, string title, string subject, string body, bool sendToAdmins, bool async, string fileName, string filePath)
        {
            SendMail(to, title, subject, body, sendToAdmins, async, null, null, fileName, File.ReadAllBytes(filePath));
        }
   }
}