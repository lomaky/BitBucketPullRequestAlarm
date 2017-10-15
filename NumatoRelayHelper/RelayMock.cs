using System;
using System.Diagnostics;
using System.Threading;

namespace NumatoRelayHelper
{
    public class RelayMock : IRelayHelper
    {
        private bool _initialised = false;

        public RelayMock()
        {
            Console.WriteLine("Relay Constructor");
        }

        public void Initialize() {
            if (!_initialised)
            {
                Console.WriteLine("Opening COMPort " + RelaySettings.COMPort);
                _initialised = true;
            }
        }

        public void Alarm()
        {
            Initialize();
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                Console.WriteLine("Relay On for " + RelaySettings.OnForSeconds + " seconds" );
                Thread.Sleep(RelaySettings.OnForSeconds * 1000);
                Console.WriteLine("Relay Off (after " +  stopwatch.Elapsed.TotalSeconds + " secods)");
            }
            catch (Exception ex) {
                ClosePort();
                throw ex;
            }
        }

        private void ClosePort()
        {
            try
            {
                _initialised = false;
                Console.WriteLine("Closing COMPort " + RelaySettings.COMPort);
            }
            catch { }
        }

        ~RelayMock()
        {
            Console.WriteLine("Relay Destructor");
            ClosePort();
        }

    }
}
