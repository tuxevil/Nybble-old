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

namespace ProjectBase.Utils.Email
{
    /// <summary>
    /// Allow to send email through standard Smtp configuration of .NET Framework
    /// </summary>
    /// <remarks>
    /// You can inherint this class to modify the defaults for template usage.
    /// We currently suggest to base on the current settings for projects compatibility.
    /// </remarks>
	public class SmtpMailing : Mailing
	{
        public override void SendMail(string to, string title, string subject, string body, bool sendToAdmins, bool async, string replyToAddress, string replyToName, string filename, byte[] filecontent)
		{
			MailMessage mm = new MailMessage( );
#if !DEBUG
			if( !string.IsNullOrEmpty( to ) )
			{
				foreach( string toMail in to.Split( ',' ) )
					mm.To.Add( toMail );
                
				if( sendToAdmins )
                    foreach (string userName in Roles.GetUsersInRole(ROLE_ADMIN))
						mm.Bcc.Add( System.Web.Security.Membership.GetUser( userName ).Email );
			}
			else
			{
                foreach (string userName in Roles.GetUsersInRole(ROLE_ADMIN))
					mm.To.Add( System.Web.Security.Membership.GetUser( userName ).Email );
			}
#else
            mm.To.Add( DEBUG_MAIL );
#endif

            if (!string.IsNullOrEmpty(replyToAddress))
                mm.ReplyTo = new MailAddress(replyToAddress, replyToName);

            if (filecontent != null)
            {
                MemoryStream ms = new MemoryStream(filecontent);
                Attachment attachment = new Attachment(ms, filename);
                mm.Attachments.Add(attachment);
            }

            mm.Subject = subject;
            mm.Body = ApplyTemplate(title, subject, body);
			mm.IsBodyHtml = true;

			SmtpClient client = new SmtpClient( );
			object userState = mm;
            if (async)
            {
                client.SendCompleted += SmtpClient_OnCompleted;
                client.SendAsync( mm , userState );
            }
            else
            {
                client.Send(mm);
            }
		}

		private static void SmtpClient_OnCompleted(object sender, AsyncCompletedEventArgs e)
		{
            if (e.Error != null)
            {
                ILog emailLogger = log4net.LogManager.GetLogger("Email");
                if (emailLogger != null && emailLogger.IsErrorEnabled)
                    emailLogger.Error(e.UserState, e.Error);
            }
		}
	}
}