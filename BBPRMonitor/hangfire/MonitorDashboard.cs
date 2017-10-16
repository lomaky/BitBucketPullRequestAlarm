using System;
using Owin;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.Annotations;

namespace BBPRMonitor
{
    public class MonitorDashboard
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization = new[] { new DashboardAuthorizationFilter() }
            });

        }
    }

    public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
