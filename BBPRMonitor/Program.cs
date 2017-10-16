using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BBPRMonitor
{
    class Program
    {
        public static IWindsorContainer container;

        static void Main(string[] args)
        {
            bool runAsService = ConfigurationManager.AppSettings["RunAsService"].Trim().ToLower() == "true";
            container = new WindsorContainer();
            container.Install(FromAssembly.This());
            if (runAsService)
            {
                var servicesToRun = new ServiceBase[]
                {
                    new Startup()
                };
                ServiceBase.Run(servicesToRun);
            }
            else
            {
                new Startup().StartUp();
                Console.WriteLine("Press any key to exit....");
                Console.ReadLine();
            }
        }
    }
}
