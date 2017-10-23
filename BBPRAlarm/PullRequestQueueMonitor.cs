using Amazon.SQS;
using Amazon.SQS.Model;
using NumatoRelayHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BBPRAlarm
{
    public class PullRequestQueueMonitor
    {
        IRelayHelper _relayHelper;

        public string AccessKeyId { get { return ConfigurationManager.AppSettings["AccessKeyId"]; } }
        public string SecretKey { get { return ConfigurationManager.AppSettings["SecretKey"];  } }
        public string SQSUrl { get { return ConfigurationManager.AppSettings["SQSUrl"]; } }


        public void ExecuteAction()
        {
            QueryQueue();
        }

        private void QueryQueue()
        {
            var _client = new AmazonSQSClient(
                  awsAccessKeyId: AccessKeyId,
                  awsSecretAccessKey: SecretKey,
                  region: Amazon.RegionEndpoint.USEast1);

            var _request = new ReceiveMessageRequest
            { QueueUrl = SQSUrl, MaxNumberOfMessages = 10 }; 

            var _response = _client.ReceiveMessage(_request);

            if (_response.Messages.Count > 0)
            {
                (new Thread(() =>
                {
                    _relayHelper = MainClass.container.Resolve<IRelayHelper>();
                    _relayHelper.Alarm();
                })).Start();

                try { _client.PurgeQueue(new PurgeQueueRequest { QueueUrl = SQSUrl }); } catch { }
            }
        }
    }
}
