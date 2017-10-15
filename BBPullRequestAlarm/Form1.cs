using NumatoRelayHelper;
using System.ComponentModel;
using System.Windows.Forms;

namespace BBPullRequestAlarm
{
    public partial class Form1 : Form
    {
        IRelayHelper _relayHelper;

        public Form1()
        { 
            InitializeComponent();
            _relayHelper = Program.container.Resolve<IRelayHelper>();  
            _relayHelper.Initialize();
            bwMonitor.RunWorkerAsync();
        }

        private void bwMonitor_DoWork(object sender, DoWorkEventArgs e)
        {
            _relayHelper.Alarm();
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
