using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ServiceCenter.Services
{
    public class EmailService
    {
        public bool Sendmail(string tomail, string body, string subject, string file = "", string ccmail = "")
        {
            bool blnSendMail = false;

            try
            {
                MailMessage mail = new MailMessage();

                if (file != "")
                {
                    Attachment at = new Attachment(file);
                    mail.Attachments.Add(at);

                    /*
                    Attachment at = new Attachment(HttpContext.Current.Server.MapPath("../Uploads/" + file));
                    mail.Attachments.Add(at);
                    */
                }
                //for (rows = 0; rows <= tomail.Length - 1; rows++)
                //{
                //    mail.To.Add(tomail[rows]);
                //}
                mail.To.Add(tomail);
                if (!string.IsNullOrEmpty(ccmail))
                {
                    List<string> ccEmailList = ccmail.Split(',').ToList();

                    if(ccEmailList.Count > 0)
                    {
                        foreach (string emailItem in ccEmailList)
                        {
                            mail.To.Add(emailItem);
                        }
                    }
                    
                }
                mail.From = new MailAddress("ketan_services@yahoo.in", "KETAN ENTERPRISE");
                mail.Subject = subject;
                mail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                //Dim Body As String = "Hi, this mail is to test sending mail" & "using Gmail in ASP.NET"
                //mail.Body = body;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.mail.yahoo.com");
                smtp.Port = 587;
                smtp.Timeout = 200000;
                //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential("ketan_services@yahoo.in", "fvcufqgcpoqzxpyp");
                //smtp.UseDefaultCredentials = false;
                //Or your Smtp Email ID and Password
                smtp.EnableSsl = true;
                smtp.Send(mail);

                blnSendMail = true;


            }
            catch (Exception ex)
            {
                blnSendMail = false;
                CommonService.WriteErrorLog(ex);
                //return ex.InnerException.Message;
            }

            return blnSendMail;
        }
    }
}