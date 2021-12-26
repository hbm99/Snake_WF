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
    public partial class SpaceCreator : Form
    {
        public SpaceCreator()
        {
            InitializeComponent();
        }

        public int Rows { get; internal set; }
        public int Columns { get; internal set; }
        public int Eggs { get; internal set; }

        private void button1_Click(object sender, EventArgs e)
        {
            Rows = (int)numericUpDown1.Value;
            Columns = (int)numericUpDown2.Value;
            Eggs = (int)numericUpDown3.Value;
            Close();
        }

    }
}
