using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace NoorGeneralHospital.Helper.Email
{
    public class EmailServices
    {
        public static int SendMail(string ToEmail, string subject, string Body)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(EmailCred.SenderEmail);  //From Email Address
                msg.To.Add(ToEmail);
                msg.Subject = subject;
                msg.Body = Body;
                msg.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient(EmailCred.SMTPHost, EmailCred.SMTPPort);
                client.Credentials = new NetworkCredential(EmailCred.SenderEmail, EmailCred.Password);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}