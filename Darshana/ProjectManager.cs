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
                string query = "SELECT labor.Name,attendancelabor.AttendanceDate,attendencelabor.Site,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.AttendanceDate = " + date+"; ";
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
                string query = "SELECT supervisor.Name,attendancesupervisor.AttendanceDate,attendencesupervisor.Site,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.AttendanceDate = " + date + "; ";
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

            //data base loading part
            string date = DateTime.Now.ToString().Split()[0].Trim();
            
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            // WHERE attendancelabor.AttendanceDate ="+date+"
            string query = "SELECT labor.Name,attendancelabor.AttendanceDate,attendancelabor.Site,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.AttendanceDate =@date;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@date", date);
            MessageBox.Show(date);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            connection.Close();

            connection.Open();
            string query2 = "SELECT supervisor.Name,attendancesupervisor.AttendanceDate,attendancesupervisor.Site,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.AttendanceDate = @date; ";
            MySqlCommand command2 = new MySqlCommand(query2, connection);
            command2.Parameters.AddWithValue("@date", date);
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            dataGridView2.DataSource = dataTable2;
            dataGridView2.Refresh();
            connection.Close();
            //database loading part end

            //load site names to the combo boxes
            using (MySqlConnection c = new MySqlConnection(db.connectionString))
            {
                c.Open();

                // Create a MySqlCommand
                using (MySqlCommand cmd = new MySqlCommand("SELECT SiteName FROM sites", c))
                {
                    // Execute the query and read the results
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Add each value to the ComboBox
                            comboBox1.Items.Add(reader["SiteName"].ToString());
                            comboBox2.Items.Add(reader["SiteName"].ToString());
                        }
                    }
                }
                //end of combo boxes

            }




        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string site=comboBox1.Text;
            string date = dateTimePicker1.Text;

            if (site != "")
            {
                using (MySqlConnection c = new MySqlConnection(db.connectionString))
                {
                    MySqlConnection connection = new MySqlConnection(db.connectionString);
                    connection.Open();
                    string query = "SELECT labor.Name,attendancelabor.AttendanceDate,attendancelabor.Site,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.AttendanceDate =@date and attendancelabor.Site=@site; ";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@site", site);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                    dataGridView1.Refresh();
                    connection.Close();


                }
            }
            else
            {
                using (MySqlConnection c = new MySqlConnection(db.connectionString))
                {
                    MySqlConnection connection = new MySqlConnection(db.connectionString);
                    connection.Open();
                    string query = "SELECT labor.Name,attendancelabor.AttendanceDate,attendancelabor.Site,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.AttendanceDate = @date; ";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@date", date);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                    dataGridView1.Refresh();
                    connection.Close();


                }
            }
            

        }

        private void ProjectManager_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string site = comboBox2.Text;
            string date = dateTimePicker2.Text;

            if (site != "")
            {
                using (MySqlConnection c = new MySqlConnection(db.connectionString))
                {
                    MySqlConnection connection = new MySqlConnection(db.connectionString);
                    string query2 = "SELECT supervisor.Name,attendancesupervisor.Site,attendancesupervisor.AttendanceDate,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.AttendanceDate =@date and attendancesupervisor.Site=@site; ";
                    MySqlCommand command2 = new MySqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("@date", date);
                    command2.Parameters.AddWithValue("@site", site);
                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);
                    DataTable dataTable2 = new DataTable();
                    adapter2.Fill(dataTable2);
                    dataGridView2.DataSource = dataTable2;
                    dataGridView2.Refresh();
                    connection.Close();


                }
            }
            else
            {
                using (MySqlConnection c = new MySqlConnection(db.connectionString))
                {
                    MySqlConnection connection = new MySqlConnection(db.connectionString);
                    string query2 = "SELECT supervisor.Name,attendancesupervisor.Site,attendancesupervisor.AttendanceDate,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.AttendanceDate = @date; ";
                    MySqlCommand command2 = new MySqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("@date", date);
                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);
                    DataTable dataTable2 = new DataTable();
                    adapter2.Fill(dataTable2);
                    dataGridView2.DataSource = dataTable2;
                    dataGridView2.Refresh();
                    connection.Close();


                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString().Split()[0].Trim();

            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            // WHERE attendancelabor.AttendanceDate ="+date+"
            string query = "SELECT labor.Name,attendancelabor.AttendanceDate,attendancelabor.Site,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.AttendanceDate =@date;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@date", date);
            MessageBox.Show(date);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            connection.Close();

            
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString().Split()[0].Trim();
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string query2 = "SELECT supervisor.Name,attendancesupervisor.AttendanceDate,attendancesupervisor.Site,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.AttendanceDate = @date; ";
            MySqlCommand command2 = new MySqlCommand(query2, connection);
            command2.Parameters.AddWithValue("@date", date);
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            dataGridView2.DataSource = dataTable2;
            dataGridView2.Refresh();
            connection.Close();
            //database loading part end
        }
    }
}
