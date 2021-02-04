using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace downloadmanager
{
    public partial class codeinput : Form
    {
        public codeinput()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter checkfile = new StreamWriter("APPMANIFEST.DLL");
            string info = textBox1.Text;
            checkfile.WriteLine(info);
            checkfile.Close();
            downloadmanager.a = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult k = MessageBox.Show("確定要取消輸入嗎?若您取消輸入，就只能使用試用版。", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (k == DialogResult.Yes)
            {
                downloadmanager.a = false;
                Close();

            }
        }
    }
}
