using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using BBPRMonitor;

[assembly: OwinStartup(typeof(OwinStartupObjects))]
namespace BBPRMonitor
{

    public class OwinStartupObjects
    {
        readonly MonitorDashboard _dashboard = new MonitorDashboard();

        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            _dashboard.Configuration(app);
        }
    }
}
