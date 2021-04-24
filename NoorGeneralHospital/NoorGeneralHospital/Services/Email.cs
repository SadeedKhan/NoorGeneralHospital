using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace NoorGeneralHospital.Services
{
    public class Email
    {
        public static string Mail(string ToEmail, string subject, string Body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(ToEmail);
                // mail.To.Add("Another Email ID where you wanna send same email");
                mail.From = new MailAddress("aalerts7@gmail.com");
                mail.Subject = subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment(ConfigurationManager.ConnectionStrings["DBbackup"].ConnectionString));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential
                     ("aalerts7@gmail.com", "!23#Alerts");
                //Or your Smtp Email ID and Password
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return "Email has been successfuly sent";
            }
            catch
            {
                return "Connection error! Please check the connection and try again";
            }
        }
    }
}