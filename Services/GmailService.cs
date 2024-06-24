
using ServiceCenter.Services;
using ServiceCenter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Services.Description;
using Message = ServiceCenter.Models.Message;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net;

namespace ServiceCenter.Services
{
    internal class GmailService
    {
       
        public bool SendMail(string tomail, string body, string subject, string file)
        {
            bool blnSendMail = false;

            try
            {
                //string host = "smtp.gmail.com";
                //int port = 465;

                var senderEmail = new MailAddress("keservices2111@gmail.com", "Keten Electronics");
                var receiverEmail = new MailAddress(tomail, "Receiver");
                var password = "rlhb ysbt wdmk tizz";
                var sub = subject;
                
                using (MailMessage mess = new MailMessage(senderEmail, receiverEmail))
                {
                    mess.Subject = subject;
                    mess.Body = body;
                    mess.Attachments.Add(new Attachment(file));


                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };

                    smtp.Send(mess);

                }
               
                Console.WriteLine("Gmail send");
                blnSendMail = true;
            }
            catch (Exception ex)
            {
                blnSendMail = false;
                CommonService.WriteErrorLog(ex);
            }

            return blnSendMail;

        }

    }
}
