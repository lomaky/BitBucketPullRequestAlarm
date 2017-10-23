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

            Console.WriteLine("Testing NUMATO...");
            relayHelperTest.Alarm();

            var prSearcher = new PullRequestQueueMonitor();

            do
            {
                Console.WriteLine("Processing SQS");
                prSearcher.ExecuteAction();
                System.Threading.Thread.Sleep(15000);

            } while (true);
        }
    }
}
