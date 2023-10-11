using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Darshana
{
    public partial class LaborersAttendance : Form
    {
        Database db = new Database();
        string site;
        public LaborersAttendance(SiteEngineer se)
        {
            InitializeComponent();
            site = se.site;
        }

        private void LaborersAttendance_Load(object sender, EventArgs e)
        {

        }
    }
}
