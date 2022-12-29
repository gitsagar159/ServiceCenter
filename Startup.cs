using Hangfire;
using Microsoft.Owin;
using Owin;
using ServiceCenter.Controllers;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(ServiceCenter.Startup))]

namespace ServiceCenter
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            // Map Dashboard to the `http://<your-app>/hangfire` URL.

            
            GlobalConfiguration.Configuration.UseSqlServerStorage("KetanServices");
            app.UseHangfireDashboard();

            HangfireJobController objHangfireJobController = new HangfireJobController();

            //RecurringJob.AddOrUpdate(() => objHangfireJobController.SendEmail(), Cron.Minutely);
            /*
            RecurringJob.AddOrUpdate(() => objHangfireJobController.SendCompanyReportMorning(), "0 11 * * *", TimeZoneInfo.Local);

            RecurringJob.AddOrUpdate(() => objHangfireJobController.SendCompanyReportNoon(), "0 16 * * *", TimeZoneInfo.Local);

            RecurringJob.AddOrUpdate(() => objHangfireJobController.SendCompanyReportEvening(), "0 20 * * *", TimeZoneInfo.Local);
            */

            app.UseHangfireServer();
            
            
        }
    }
}
