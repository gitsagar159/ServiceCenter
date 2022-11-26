using Hangfire;
using Hangfire.Common;
using Microsoft.Ajax.Utilities;
using ServiceCenter.Models;
using ServiceCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServiceCenter.Controllers
{
    public class HangfireJobController : Controller
    {
        // GET: HangfireJob
        [HttpPost]
        public JsonResult Job1()
        {
            RecurringJob.AddOrUpdate("easyjob", () => Console.Write("Easy!"), Cron.Daily);

            return Json(new { data = "Ok" });
        }

        public async Task<ActionResult> SendEmail()
        {
            await SendMailAsync("dsagar159@gmail.com", "Test Subject", "Test Message");

            return Json(0, JsonRequestBehavior.AllowGet);
        }

        public async static Task SendMailAsync(string toEmail, string subject, string message)
        {

            try
            {
                string FromEmail = "mail4dudhaiya@gmail.com";
                string Pasword = "Mylife@159";
                string DisplayName = "Test Mail";

                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(toEmail);
                myMessage.From = new MailAddress(FromEmail, DisplayName);
                myMessage.Subject = subject;
                myMessage.Body = message;
                myMessage.IsBodyHtml = true;

                using(SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new NetworkCredential(FromEmail, Pasword);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(myMessage);

                }

            }
            catch(Exception ex)
            {

            }
        }
    }
}