using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerFormTest
{
    public partial class ServerForm : Form
    {
        private delegate void SetStatusCallback(string text);
        private ServerHelper helper;

        public ServerForm()
        {
            InitializeComponent();
        }

        private void serverStart_Click(object sender, EventArgs e)
        {
            this.serverStart.Enabled = false;
            this.stopButton.Enabled = true;
            helper = new ServerHelper(this);            
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.stopButton.Enabled = false;
            this.serverStart.Enabled = true;
            helper.stopServer.Set();
            //this.statusBox.AppendText("Stopping Server..." + System.Environment.NewLine);
        }

        public void appendStatusBox(string text)
        {
            if (this.statusBox.InvokeRequired)
            {
                var d = new SetStatusCallback(appendStatusBox);
                this.Invoke(d, new string[] { text });
            }
            else
            {
                this.statusBox.AppendText(text);
                this.statusBox.AppendText(System.Environment.NewLine);
            }
        }
    }
}
