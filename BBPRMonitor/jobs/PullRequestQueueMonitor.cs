using Hangfire;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBPRMonitor
{
    public class PullRequestQueueMonitor
    {
        [DisableConcurrentExecution(60)]
        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public void ExecuteAction(PerformContext context) { }
    }
}
