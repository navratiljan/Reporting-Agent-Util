using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace ReportingAgent
{
    public class MailingAgent
    {
        public static void SendMail(string mailFrom, string mailTo, string file){

            try
            {

            SmtpClient mySmtpClient = new SmtpClient("my.smtp.exampleserver.net");

            // IMPORTANT NOTICE: Please note that these are dummy values, do not ever hard-code credentials in code :)
            mySmtpClient.UseDefaultCredentials = false;
            System.Net.NetworkCredential basicAuthenticationInfo = new
            System.Net.NetworkCredential("EXAMPLEUSER", "EXAMPLEPASSWORD");
            mySmtpClient.Credentials = basicAuthenticationInfo;

            // add from,to mailaddresses
            MailAddress from = new MailAddress(mailFrom);
            MailAddress to = new MailAddress(mailTo);
            MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

            // set subject and encoding
            myMail.Subject = "Watchlist Reports";
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            // set body-message and encoding
            myMail.Body = "This is an automated mail, please do not response";
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            
            // Create  the file attachment for this email message.
            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);

            // Add time stamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            // Add the file attachment to this email message.
            myMail.Attachments.Add(data);

            myMail.IsBodyHtml = true;

            mySmtpClient.Send(myMail);
            }

            catch (SmtpException ex)
            {
            throw new ApplicationException
                ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
            throw ex;
            }
        }
    }
}