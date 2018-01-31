using System;
using System.Threading;

namespace NumatoRelayHelper
{
    public class RelayNumato : IRelayHelper
    {
        private bool _initialised = false;
        private System.IO.Ports.SerialPort _serialPort;

        public RelayNumato()
        {

        }

        public void Initialize() {
            if (!_initialised)
            {
                _serialPort = new System.IO.Ports.SerialPort();
                _serialPort.BaudRate = 9600;
                _serialPort.PortName = RelaySettings.COMPort;
                _serialPort.Open();
                _initialised = true;
            }
        }

        public void Alarm()
        {
            Initialize();
            try
            {
                _serialPort.DiscardInBuffer();
                _serialPort.Write("relay on 1\r");
                Thread.Sleep(10);
                _serialPort.DiscardInBuffer();
                Thread.Sleep(RelaySettings.OnForSeconds * 1000);
                _serialPort.Write("relay off 1\r");
                _serialPort.DiscardInBuffer();
                Thread.Sleep(10);
                _serialPort.DiscardInBuffer();
            }
            catch(Exception ex) {
                ClosePort();
                throw ex;
            }
        }

        private void ClosePort()
        {
            try
            {
                _initialised = false;
                _serialPort.Close();
            }
            catch { }
        }

        public void Test()
        {
            Initialize();
            try
            {
                _serialPort.DiscardInBuffer();
                _serialPort.Write("relay on 1\r");
                Thread.Sleep(10);
                _serialPort.DiscardInBuffer();
                Thread.Sleep(5000);
                _serialPort.Write("relay off 1\r");
                _serialPort.DiscardInBuffer();
                Thread.Sleep(10);
                _serialPort.DiscardInBuffer();
            }
            catch (Exception ex)
            {
                ClosePort();
                throw ex;
            }
        }

        ~RelayNumato()
        {
            ClosePort();
        }

    }
}
