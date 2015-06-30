using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Json_Client_Form
{
    public partial class InputBox : Form
    {
        public InputBox(string title, string label)
        {
            InitializeComponent(title, label);
        }

        private void inputConfirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
