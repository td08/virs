using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Virs_Client_Form
{
    public partial class SerialSettings : Form
    {
        private bool firstLoad = true;  // bool indicating if form has been previously displayed

        public string baud { get { return this.baudSelect.Text; } }
        public string dataBits { get { return this.dataBitSelect.Text; } }
        public string stopBits { get { return this.stopBitSelect.Text; } }
        public string parity { get { return this.paritySelect.Text; } }
        public string flow { get { return this.flowControlSelect.Text; } }

        public SerialSettings()
        {
            InitializeComponent();
            if (firstLoad)
            {
                loadSettings();
                firstLoad = false;  // set load flag so settings are not reloaded
            }
        }

        private void loadSettings()
        {
            // add baud rates
            this.baudSelect.Items.Add(300);
            this.baudSelect.Items.Add(600);
            this.baudSelect.Items.Add(1200);
            this.baudSelect.Items.Add(2400);
            this.baudSelect.Items.Add(9600);
            this.baudSelect.Items.Add(14400);
            this.baudSelect.Items.Add(19200);
            this.baudSelect.Items.Add(38400);
            this.baudSelect.Items.Add(57600);
            this.baudSelect.Items.Add(115200);
            this.baudSelect.Text = this.baudSelect.Items[9].ToString(); // default to 115200

            // add data bits
            this.dataBitSelect.Items.Add(7);
            this.dataBitSelect.Items.Add(8);
            this.dataBitSelect.Text = this.dataBitSelect.Items[1].ToString(); // default to 8
        
            // add stop bits
            this.stopBitSelect.Items.Add("One");
            this.stopBitSelect.Items.Add("OnePointFive");
            this.stopBitSelect.Items.Add("Two");
            this.stopBitSelect.Text = this.stopBitSelect.Items[0].ToString(); // default to 1
        
            // add parity
            this.paritySelect.Items.Add("None");
            this.paritySelect.Items.Add("Even");
            this.paritySelect.Items.Add("Mark");
            this.paritySelect.Items.Add("Odd");
            this.paritySelect.Items.Add("Space");
            this.paritySelect.Text = this.paritySelect.Items[0].ToString(); // default to none
            
            // add flow control
            this.flowControlSelect.Items.Add("None");
            this.flowControlSelect.Items.Add("XOnXOff");
            this.flowControlSelect.Items.Add("RequestToSend");
            this.flowControlSelect.Items.Add("RequestToSendXOnXOff");
            this.flowControlSelect.Text = this.flowControlSelect.Items[0].ToString(); // default to none       
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void defaultButton_Click(object sender, EventArgs e)
        {
            this.baudSelect.Text = this.baudSelect.Items[9].ToString(); // default to 115200
            this.dataBitSelect.Text = this.dataBitSelect.Items[1].ToString(); // default to 8
            this.stopBitSelect.Text = this.stopBitSelect.Items[0].ToString(); // default to 1
            this.paritySelect.Text = this.paritySelect.Items[0].ToString(); // default to none
            this.flowControlSelect.Text = this.flowControlSelect.Items[0].ToString(); // default to none
        }
    }
}
