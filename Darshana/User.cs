using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Darshana
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SELogin se = new SELogin();
            this.Hide();
            se.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PMLogin pm = new PMLogin();
            this.Hide();
            pm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OwnerLogin ol = new OwnerLogin();
            this.Hide();
            ol.Show();
        }
    }
}
