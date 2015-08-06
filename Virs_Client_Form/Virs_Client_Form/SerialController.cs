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
    public partial class SerialController : Form
    {
        private SerialSettings settings;

        public SerialController()
        {
            InitializeComponent();
            settings = new SerialSettings();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.ShowDialog();
        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            this.debugLabel.Text = settings.baud;
        }
    }
}
