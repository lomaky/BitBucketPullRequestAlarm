using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using NumatoRelayHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BBPRMonitor
{
    public class PullRequestQueueMonitor
    {
        IRelayHelper _relayHelper;


        [DisableConcurrentExecution(60)]
        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public void ExecuteAction(PerformContext context) {
            var console = new HangFireConsole(context, context.WriteProgressBar());
            console.Write("Initializing PullRequestQueueMonitor Service ...", HangFireConsole.ActivityType.Info);


            (new Thread(() => {
                _relayHelper = Program.container.Resolve<IRelayHelper>();
                _relayHelper.Alarm();
            })).Start();

        }
    }
}
