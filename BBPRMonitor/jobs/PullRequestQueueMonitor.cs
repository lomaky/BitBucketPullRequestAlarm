﻿using Amazon.SQS;
using Amazon.SQS.Model;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using NumatoRelayHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BBPRMonitor
{
    public class PullRequestQueueMonitor
    {
        IRelayHelper _relayHelper;

        public string AccessKeyId { get { return ConfigurationManager.AppSettings["AccessKeyId"]; } }
        public string SecretKey { get { return ConfigurationManager.AppSettings["SecretKey"];  } }
        public string SQSUrl { get { return ConfigurationManager.AppSettings["SQSUrl"]; } }


        [DisableConcurrentExecution(60)]
        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public void ExecuteAction(PerformContext context) {
            var console = new HangFireConsole(context, context.WriteProgressBar());
            console.Write("Initializing PullRequestQueueMonitor Service ...", HangFireConsole.ActivityType.Info);

            var _client = new AmazonSQSClient(
                  awsAccessKeyId: AccessKeyId,
                  awsSecretAccessKey: SecretKey,
                  region: Amazon.RegionEndpoint.USEast1);

            var _request = new ReceiveMessageRequest
            { QueueUrl = SQSUrl, MaxNumberOfMessages = 10 };

            console.Write("Querying Queue " + SQSUrl + "...", HangFireConsole.ActivityType.Info);

            var _response = _client.ReceiveMessage(_request);

            if (_response.Messages.Count > 0)
            {
                console.Write(_response.Messages.Count + " PullRequest(s) Found", HangFireConsole.ActivityType.Info);

                (new Thread(() =>
                {
                    _relayHelper = Program.container.Resolve<IRelayHelper>();
                    _relayHelper.Alarm();
                })).Start();

                _client.PurgeQueue(new PurgeQueueRequest { QueueUrl = SQSUrl });

                console.Write("Purging Queue", HangFireConsole.ActivityType.Info);

            }

            console.Write("Finalizing PullRequestQueueMonitor Service.", HangFireConsole.ActivityType.Info);

        }
    }
}
