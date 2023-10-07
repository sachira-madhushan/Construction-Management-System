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
    public partial class SiteEngineer : Form
    {
        public SiteEngineer()
        {
            InitializeComponent();
        }

        private void buttonManageLabours_Click(object sender, EventArgs e)
        {
            ManageLabours mLab = new ManageLabours();
            mLab.Show();
        }

        private void buttonManageSupervisors_Click(object sender, EventArgs e)
        {
            ManageSupervisors mSup = new ManageSupervisors();
            mSup.Show();
        }
    }
}
