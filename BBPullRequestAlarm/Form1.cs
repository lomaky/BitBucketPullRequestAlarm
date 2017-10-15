using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBPullRequestAlarm
{
    public partial class Form1 : Form
    {
        RelayHelper _relayHelper;

        public Form1()
        { 
            InitializeComponent();
            _relayHelper = new RelayHelper(3);
            bwMonitor.RunWorkerAsync();
        }

        private void bwMonitor_DoWork(object sender, DoWorkEventArgs e)
        {
            _relayHelper.Alarm(5);
        }

        private void bwMonitor_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bwMonitor_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
