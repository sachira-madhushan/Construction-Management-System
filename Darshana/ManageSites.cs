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
    public partial class ManageSites : Form
    {
        Database db = new Database();
        public ManageSites()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string query = "SELECT * FROM siteengineers";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string SEID = textBox1.Text;
            string Name = textBox2.Text;
            string site = textBox3.Text;
            string NIC = textBox4.Text;
            int phone=0;
            try
            {
                phone = Int32.Parse(textBox5.Text);
            }catch(Exception ex)
            {

            }
            string address = textBox6.Text;
            string password = textBox7.Text;

            if(SEID != "" && Name !="" && site!="" && password!="")
            {
                string query = "INSERT INTO siteengineers(`SEID`, `Name`, `Site`, `NIC`,`Phone`,`Address`,`Password`) VALUES ('" + SEID + "', '" + Name + "', '" + site + "', '" + NIC + "', '" + phone + "', '" + address + "', '" + password + "')";
                string query1 = "INSERT INTO sites(`SEID`,`SiteName`) VALUES ('" + SEID + "','" + site + "')";
                
                MySqlConnection databaseConnection = new MySqlConnection(db.connectionString);
                databaseConnection.Open();
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
                databaseConnection.Open();
                MySqlCommand commandDatabase2 = new MySqlCommand(query1, databaseConnection);
                commandDatabase2.CommandTimeout = 60;
                commandDatabase2.ExecuteNonQuery();
                databaseConnection.Close();
                DialogResult result = MessageBox.Show("Data Saved !", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text="";
                textBox2.Text="";
                textBox3.Text="";
                textBox4.Text="";
                textBox5.Text="";
                textBox6.Text="";
                textBox7.Text="";
                dataGridView1.Refresh();
            }
            else
            {
                DialogResult result = MessageBox.Show("Please fill details !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string SEID = textBox1.Text;
            string Name = textBox2.Text;
            string site = textBox3.Text;
            string NIC = textBox4.Text;
            int phone = 0;
            try
            {
                phone = Int32.Parse(textBox5.Text);
            }
            catch (Exception ex)
            {

            }
            string address = textBox6.Text;
            string password = textBox7.Text;

            if (SEID != "")
            {

                string updateQuery = "UPDATE siteengineers SET Name = @name,NIC=@nic,Password=@password,Address=@address WHERE SEID = @seid";
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@name", Name);
                updateCommand.Parameters.AddWithValue("@nic", NIC);
                updateCommand.Parameters.AddWithValue("@password", password);
                updateCommand.Parameters.AddWithValue("@address", address);
                updateCommand.Parameters.AddWithValue("@seid", SEID);
                int rowsAffected = updateCommand.ExecuteNonQuery();
                connection.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Update success !", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Site ID Not Valid !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string SEID = textBox1.Text;
            if (SEID != "")
            {
                string updateQuery = "DELETE FROM `siteengineers` WHERE SEID=@seid";
                string updateQuery2 = "DELETE FROM `sites` WHERE SEID=@seid";
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@seid", SEID);
                int rowsAffected = updateCommand.ExecuteNonQuery();
                connection.Close();
                connection.Open();
                MySqlCommand updateCommand2 = new MySqlCommand(updateQuery2, connection);
                updateCommand2.Parameters.AddWithValue("@seid", SEID);
                updateCommand2.ExecuteNonQuery();
                connection.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Deleted !", "Done", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Site ID Not Valid !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    }
}
