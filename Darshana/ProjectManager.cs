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
    public partial class ProjectManager : Form
    {
        Database db = new Database();
        public ProjectManager()
        {
            InitializeComponent();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            User user = new User();
            this.Hide();
            user.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManageSites ms = new ManageSites();
            ms.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Materials mt = new Materials();
            mt.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            string date = DateTime.Now.ToString().Split()[0];
            using (MySqlConnection c = new MySqlConnection(db.connectionString))
            {
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                string query = "SELECT labor.Name,attendancelabor.AttendanceDate,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.AttendanceDate = " + date+"; ";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Refresh();
                connection.Close();


            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString().Split()[0];
            using (MySqlConnection c = new MySqlConnection(db.connectionString))
            {
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                string query = "SELECT supervisor.Name,attendancesupervisor.AttendanceDate,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.AttendanceDate = " + date + "; ";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
                dataGridView2.Refresh();
                connection.Close();


            }
        }

        private void ProjectManager_Load(object sender, EventArgs e)
        {
            timer1.Start();
            string date = DateTime.Now.ToString().Split()[0];
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string query = "SELECT labor.Name,attendancelabor.AttendanceDate,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.AttendanceDate = " + date + "; ";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            connection.Close();

            connection.Open();
            string query2 = "SELECT supervisor.Name,attendancesupervisor.AttendanceDate,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.AttendanceDate = " + date + "; ";
            MySqlCommand command2 = new MySqlCommand(query2, connection);
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);
            DataTable dataTable2 = new DataTable();
            adapter.Fill(dataTable2);
            dataGridView2.DataSource = dataTable2;
            dataGridView2.Refresh();
            connection.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
