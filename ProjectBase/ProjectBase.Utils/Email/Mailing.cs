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
using System.Web;
using System.Reflection;

namespace ProjectBase.Utils.Email
{
    /// <summary>
    /// Allow to send email through standard Smtp configuration of .NET Framework
    /// </summary>
    /// <remarks>
    /// You can inherint this class to modify the defaults for template usage.
    /// We currently suggest to base on the current settings for projects compatibility.
    /// </remarks>
    public abstract class Mailing
    {
        protected const string ROLE_ADMIN = "Administrator";
        protected const string MAIL_TITLE = "[TITLE]";
        protected const string MAIL_BODY = "[BODY]";
        protected const string MAIL_SUBJECT = "[SUBJECT]";
        protected const string MAIL_SITE_URL = "[SITEURL]";
        protected const string MAIL_TEMPLATE_FILE = "template.htm";
        protected const string DEBUG_MAIL = "app@nybblegroup.com";

        protected string ResourcesPath
        {
            get {
                string path = ConfigurationManager.AppSettings["Mailing_TemplatePath"];

                if (!Path.IsPathRooted(path))
                    if (HttpContext.Current != null)
                        path = HttpContext.Current.Server.MapPath(path);
                    else
                        path = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), path);

                return path; 
            }
        }

        protected string SiteUrl
        {
            get { return ConfigurationManager.AppSettings["Mailing_SiteUrl"]; }
        }

        public void SendAsyncMail(MembershipUser mu, string title, string subject, string body)
        {
            SendMail(mu, title, subject, body, false, true);
        }

        public void SendMail(MembershipUser mu, string title, string subject, string body)
        {
            SendMail(mu, title, subject, body, false, false);
        }

        public void SendMail(MembershipUser mu, string title, string subject, string body, bool sendToAdmins, bool async)
        {
            SendMail(mu.Email, title, subject, body, sendToAdmins, async, null, null, null, null);
        }

        public void SendMail(MembershipUser mu, string title, string subject, string body, bool sendToAdmins)
        {
            SendMail(mu.Email, title, subject, body, sendToAdmins, false, null, null, null, null);
        }

        public void SendAlert(string subject, string body)
        {
            SendMail("", subject, subject, body, true, false, null, null, null, null);
        }

        public void SendMail(string to, string title, string subject, string body, bool sendToAdmins)
        {
            SendMail(to, title, subject, body, sendToAdmins, false, null, null, null, null);
        }

        public void SendMail(string to, string title, string subject, string body, bool sendToAdmins, string filename, byte[] filecontent)
        {
            SendMail(to, title, subject, body, sendToAdmins, false, null, null, filename, filecontent);
        }

        protected string ApplyTemplate(string title, string subject, string body)
        {
            if (!string.IsNullOrEmpty(ResourcesPath))
            {
                // Get the template email and set the body, subject and title.
                string template = File.ReadAllText(Path.Combine(ResourcesPath, MAIL_TEMPLATE_FILE));
                template = template.Replace(MAIL_TITLE, title);
                template = template.Replace(MAIL_SUBJECT, subject);
                template = template.Replace(MAIL_BODY, body);

                // Replace the SITEURL to left fixed images location
                template = template.Replace(MAIL_SITE_URL, SiteUrl);

                return template;
            }

            return body;
        }

        public void SendMail(string to, string title, string subject, string body, bool sendToAdmins, bool async, string filename, byte[] filecontent)
        {
            SendMail(to, title, subject, body, sendToAdmins, async, null, null, filename, filecontent);
        }

        public abstract void SendMail(string to, string title, string subject, string body, bool sendToAdmins, bool async, string replyToAddress, string replyToName, string filename, byte[] filecontent);
    }
}