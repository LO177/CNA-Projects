using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        bool press;
        public Form1()
        {
            InitializeComponent();
            press = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.Opacity = (double)trackBar1.Value / trackBar1.Maximum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            press = !press;

            if (press == true)
                MessageBox.Show("Hello World!");
            else
                MessageBox.Show("");
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Form1 MessageBox = this;
            
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            int red = trackBar2.Value;
            int blue = this.BackColor.G;
            int green = this.BackColor.B;
            this.ForeColor = System.Drawing.Color.FromArgb(red,green,blue);
        }
    }
}
