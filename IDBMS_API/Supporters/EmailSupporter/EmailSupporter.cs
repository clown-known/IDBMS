using System.Net.Mail;
using System.Net;

namespace IDBMS_API.Supporters.EmailSupporter
{
    public class EmailSupporter
    {
        public static void SendEmail(string email, string subject, string body)
        {
            // send otp
            using (MailMessage mm = new MailMessage("idtautomailer@gmail.com", email))
            {
                mm.Subject = subject;

                mm.Body = body;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("idtautomailer@gmail.com", "npufojpvlcxiowda");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }
        public static void SendMentionedEnglishEmail(string email,string projectName,string link)
        {
            string subject = "Someone mentioned you in "+projectName;
            string body = "";
            // send otp
            using (MailMessage mm = new MailMessage("idtautomailer@gmail.com", email))
            {
                mm.Subject = subject;

                mm.Body = body;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("idtautomailer@gmail.com", "npufojpvlcxiowda");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }
    }
}
