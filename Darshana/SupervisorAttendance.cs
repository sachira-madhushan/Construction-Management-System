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
    public partial class SupervisorAttendance : Form
    {
        Database db = new Database();
        string site;
        public SupervisorAttendance(SiteEngineer se)
        {
            InitializeComponent();
            site = se.site;
        }

        private void SupervisorAttendance_Load(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString().Split()[0].Trim();
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            // WHERE attendancelabor.AttendanceDate ="+date+"
            string query = "SELECT SupervisorID from supervisor WHERE Site=@site;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@site", site);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
            dataGridView2.Refresh();
            connection.Close();

            connection.Open();
            // WHERE attendancelabor.AttendanceDate ="+date+"
            string query2 = "SELECT SupervisorID,CASE WHEN IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,OT,Advance,FA as Food from attendancesupervisor WHERE Site=@site AND AttendanceDate=@date;";
            MySqlCommand command2 = new MySqlCommand(query2, connection);
            command2.Parameters.AddWithValue("@site", site);
            command2.Parameters.AddWithValue("@date", date);
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            dataGridView1.DataSource = dataTable2;
            dataGridView1.Refresh();
            connection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            updateGridView();
        }

        private void updateGridView()
        {
            string date = DateTime.Now.ToString().Split()[0].Trim();
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            // WHERE attendancelabor.AttendanceDate ="+date+"
            string query2 = "SELECT SupervisorID,CASE WHEN IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,OT,Advance,FA as Food from attendancesupervisor WHERE Site=@site AND AttendanceDate=@date;";
            MySqlCommand command2 = new MySqlCommand(query2, connection);
            command2.Parameters.AddWithValue("@site", site);
            command2.Parameters.AddWithValue("@date", date);
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            dataGridView1.DataSource = dataTable2;
            dataGridView1.Refresh();
            connection.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int ot = 0, advance = 0, food = 0;
            string id = textBox1.Text;
            string t2 = textBox2.Text;
            string t3 = textBox3.Text;
            string t4 = textBox4.Text;
            ot = t2 == "" ? 0 : Int32.Parse(textBox2.Text);
            advance = t3 == "" ? 0 : Int32.Parse(textBox3.Text);
            food = t4 == "" ? 0 : Int32.Parse(textBox4.Text);


            string date = DateTime.Now.ToString().Split()[0].Trim();
            if (id == null)
            {
                return;
            }
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string query2 = "select * from supervisor where Site=@site and SupervisorID=@id;";
            MySqlCommand command2 = new MySqlCommand(query2, connection);
            command2.Parameters.AddWithValue("@site", site);
            command2.Parameters.AddWithValue("@id", id);


            using (MySqlDataReader reader = command2.ExecuteReader())
            {
                reader.Close();
                object result = command2.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                {
                    MessageBox.Show("Invalied ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                reader.Close();
            }
            connection.Close();
            connection.Open();
            string query3 = "select * from attendancesupervisor where SupervisorID=@id and AttendanceDate=@date;";
            MySqlCommand command3 = new MySqlCommand(query3, connection);
            command3.Parameters.AddWithValue("@date", date);
            command3.Parameters.AddWithValue("@id", id);
            using (MySqlDataReader reader = command3.ExecuteReader())
            {

                if (reader.Read())
                {
                    reader.Close();
                    DialogResult dialogresult = MessageBox.Show("Attendance Already Marked! Do you want to update?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (dialogresult == DialogResult.Yes)
                    {
                        // WHERE attendancelabor.AttendanceDate ="+date+"
                        string query = "Update attendancesupervisor set IsPresent=1,OT=@ot,Advance=@advance,FA=@food where SupervisorID=@id and AttendanceDate=@date;";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@site", site);
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@ot", ot);
                        command.Parameters.AddWithValue("@advance", advance);
                        command.Parameters.AddWithValue("@date", date);
                        command.Parameters.AddWithValue("@food", food);
                        command.ExecuteNonQuery();
                        updateGridView();
                        connection.Close();
                        MessageBox.Show("Attendance Updated!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                    }

                }
                else
                {
                    reader.Close();
                    // WHERE attendancelabor.AttendanceDate ="+date+"
                    string query = "Insert into attendancesupervisor( Site,SupervisorID,AttendanceDate,IsPresent,OT,Advance,FA) values(@site,@id,@date,1,@ot,@advance,@food);";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@site", site);
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@ot", ot);
                    command.Parameters.AddWithValue("@advance", advance);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@food", food);
                    command.ExecuteNonQuery();
                    updateGridView();
                    connection.Close();
                    MessageBox.Show("Attendance Marked!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
                reader.Close();
            }

            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ot = 0, advance = 0, food = 0;
            string id = textBox1.Text;
            string t2 = textBox2.Text;
            string t3 = textBox3.Text;
            string t4 = textBox4.Text;
            ot = t2 == "" ? 0 : Int32.Parse(textBox2.Text);
            advance = t3 == "" ? 0 : Int32.Parse(textBox3.Text);
            food = t4 == "" ? 0 : Int32.Parse(textBox4.Text);


            string date = DateTime.Now.ToString().Split()[0].Trim();
            if (id == null)
            {
                return;
            }
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string query2 = "select * from supervisor where Site=@site and SupervisorID=@id;";
            MySqlCommand command2 = new MySqlCommand(query2, connection);
            command2.Parameters.AddWithValue("@site", site);
            command2.Parameters.AddWithValue("@id", id);


            using (MySqlDataReader reader = command2.ExecuteReader())
            {
                reader.Close();
                object result = command2.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                {
                    MessageBox.Show("Invalied ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                reader.Close();
            }
            connection.Close();
            connection.Open();
            string query3 = "select * from attendancesupervisor where SupervisorID=@id and AttendanceDate=@date;";
            MySqlCommand command3 = new MySqlCommand(query3, connection);
            command3.Parameters.AddWithValue("@date", date);
            command3.Parameters.AddWithValue("@id", id);
            using (MySqlDataReader reader = command3.ExecuteReader())
            {

                if (reader.Read())
                {
                    reader.Close();
                    DialogResult dialogresult = MessageBox.Show("Attendance Already Marked! Do you want to update?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                    if (dialogresult == DialogResult.Yes)
                    {
                        // WHERE attendancelabor.AttendanceDate ="+date+"
                        string query = "Update attendancesupervisor set IsPresent=0,OT=@ot,Advance=@advance,FA=@food where SupervisorID=@id and AttendanceDate=@date;";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@site", site);
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@ot", ot);
                        command.Parameters.AddWithValue("@advance", advance);
                        command.Parameters.AddWithValue("@date", date);
                        command.Parameters.AddWithValue("@food", food);
                        command.ExecuteNonQuery();
                        updateGridView();
                        connection.Close();
                        MessageBox.Show("Attendance Updated!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                    }

                }
                else
                {
                    reader.Close();
                    // WHERE attendancelabor.AttendanceDate ="+date+"
                    string query = "Insert into attendancesupervisor( Site,SupervisorID,AttendanceDate,IsPresent,OT,Advance,FA) values(@site,@id,@date,0,@ot,@advance,@food);";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@site", site);
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@ot", ot);
                    command.Parameters.AddWithValue("@advance", advance);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@food", food);
                    command.ExecuteNonQuery();
                    updateGridView();
                    connection.Close();
                    MessageBox.Show("Attendance Marked!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
                reader.Close();
            }

            connection.Close();
        }
    }
}
