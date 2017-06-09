using System;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace ProjectBase.MailSender
{
    /// <summary>
    /// Summary description for Mail
    /// </summary>
    [WebService(Namespace = "http://mailservice.grundfos.app.nybblenetwork.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Mail : WebService
    {
        [WebMethod]
        public void SendEmail(string from, string to, string subject, string body, string replyToAddress, string replyToName)
        {
            SendEmailWithAttachment(from, to, subject, body, replyToAddress, replyToName, null, null);
        }

        [WebMethod]
        public void SendEmailWithAttachment(string from, string to, string subject, string body, string replyToAddress, string replyToName, string filename, byte[] fileContent)
        {
            MailMessage mm = new MailMessage();
            if(from != null)
                mm.From = new MailAddress(from);
            mm.To.Add(to);

            if (!string.IsNullOrEmpty(replyToAddress))
                mm.ReplyTo = new MailAddress(replyToAddress, replyToName);

            if (fileContent != null)
            {
                MemoryStream ms = new MemoryStream(fileContent);
                Attachment attachment = new Attachment(ms, filename);
                mm.Attachments.Add(attachment);
            }

            mm.IsBodyHtml = true;
            mm.Subject = subject;
            mm.Body = body;

            SmtpClient client = new SmtpClient();
            client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
            client.SendAsync(mm, null);
        }

        void client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                log4net.ILog logger = log4net.LogManager.GetLogger("MailSender");
                if (logger != null)
                    logger.Error(e.Error);
            }
        }
    }
}
