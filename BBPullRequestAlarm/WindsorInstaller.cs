using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NumatoRelayHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBPullRequestAlarm
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            var alarmConnected = bool.Parse(ConfigurationManager.AppSettings["AlarmConnected"]);

            if (alarmConnected)
            { container.Register(Component.For<IRelayHelper>().ImplementedBy<RelayNumato>()); }
            else
            { container.Register(Component.For<IRelayHelper>().ImplementedBy<RelayMock>());}
        }
    }
}
