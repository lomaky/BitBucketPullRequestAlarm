using Hangfire;
using Hangfire.Console;
using Hangfire.SQLite;
using Microsoft.Owin.Hosting;
using System;
using System.Configuration;

namespace BBPRMonitor
{
    public class Startup : System.ServiceProcess.ServiceBase
    {
        protected BackgroundJobServer _server;

        public const string DO_NOT_RUN = "DO_NOT_RUN";

        public Startup()
        {
            // Hangfire Connection String
            GlobalConfiguration.Configuration.UseSQLiteStorage(ConfigurationManager.AppSettings["HangfireSQLiteConn"]);
        }

        protected override void OnStart(string[] args)
        {
            StartUp();
        }

        public void StartUp()
        {
            string baseAddress = ConfigurationManager.AppSettings["HangfireDashboardURL"];
            //As SQLite cannot handle concurrent request, set WorkerCount = 1.
            _server = new BackgroundJobServer(new BackgroundJobServerOptions { WorkerCount = 1 });
            Console.WriteLine("Hangfire Server started.");

            ConfigureLogging();

            // Disable Retries
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });

            //Default is en-US
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo(ConfigurationManager.AppSettings["HangfireCulture"]);
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = new System.Globalization.CultureInfo(ConfigurationManager.AppSettings["HangfireCulture"]);

            CustomStartUp(baseAddress);

            GlobalConfiguration.Configuration.UseConsole();

            AddServices();
        }

        private void ConfigureLogging()
        {
        }

        protected void CustomStartUp(string baseAddress)
        {
            WebApp.Start<OwinStartupObjects>(url: baseAddress);
            Console.WriteLine("Owing Server started at " + baseAddress);
        }

        protected void AddServices()
        {
            if (ConfigurationManager.AppSettings["FOO_DEV_TEST_SERVICE"] != DO_NOT_RUN)
            {
                var svc = new PullRequestQueueMonitor();
                RecurringJob.AddOrUpdate("PR_QUEUE_MONITOR_SERVICE", () => svc.ExecuteAction(null),
                    ConfigurationManager.AppSettings["PR_QUEUE_MONITOR_SERVICE"]);
            }
            else
            {
                RecurringJob.RemoveIfExists("PR_QUEUE_MONITOR_SERVICE");
            }
        }

        public new void Dispose()
        {
            _server.Dispose();
        }

        protected override void OnStop()
        {
            Dispose();
        }
    }
}
