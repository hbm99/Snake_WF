using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeVisual
{
    public partial class SetObject : Form
    {
        public int Shape { get; set; }
        public SetObject()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Shape = 1;
            Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Shape = 2;
            Close();
        }
    }
}
