using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB3
{
    public partial class EnterZForm : Form
    {
        public int Z;
        public EnterZForm()
        {
            InitializeComponent();
        }

        private void saveZButton_Click(object sender, EventArgs e)
        {
            if (this.zTextBox.Text.ToString() != "") {
                Z = int.Parse(this.zTextBox.Text.ToString());
                this.Close();
            } 
            else MessageBox.Show("Error", "Ошибка получения Z", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
