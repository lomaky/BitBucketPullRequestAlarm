using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BBPullRequestAlarm
{
    public class RelayHelper
    {
        private System.IO.Ports.SerialPort _serialPort;

        public RelayHelper(int comPort)
        {
            _serialPort = new System.IO.Ports.SerialPort();
            _serialPort.PortName = "COM" + comPort;
            _serialPort.BaudRate = 9600;
            _serialPort.Open();
        }

        public void Alarm(int seconds)
        {
            _serialPort.DiscardInBuffer();
            _serialPort.Write("relay on 1\r");
            Thread.Sleep(10);
            _serialPort.DiscardInBuffer();
            Thread.Sleep(seconds*1000);
            _serialPort.Write("relay off 1\r");
            _serialPort.DiscardInBuffer();
            Thread.Sleep(10);
            _serialPort.DiscardInBuffer();
        }

        ~RelayHelper()
        {
            _serialPort.Close();
        }
    }
}
