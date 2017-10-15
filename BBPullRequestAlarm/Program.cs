using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBPullRequestAlarm
{
    static class Program
    {
        public static IWindsorContainer container;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            container = new WindsorContainer();
            container.Install(FromAssembly.This());
            Application.Run(new Form1());
        }
    }
}
