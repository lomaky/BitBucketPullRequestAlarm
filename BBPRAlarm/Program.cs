using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumatoRelayHelper;

namespace BBPRAlarm
{
    public class MainClass
    {
        public static IWindsorContainer container;

        public static void Main(string[] args)
        {
            container = new WindsorContainer();
            container.Install(FromAssembly.This());

            var relayHelperTest = container.Resolve<IRelayHelper>();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Processing SQS");
                relayHelperTest.Alarm();
                System.Threading.Thread.Sleep(10000);
            }
        }
    }
}
