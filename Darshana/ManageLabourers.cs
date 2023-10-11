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
    public partial class ManageLabourers : Form
    {
        Database db = new Database();
        string site;
        public ManageLabourers(SiteEngineer se)
        {
            InitializeComponent();
            site = se.site;
        }


        private void ManageLabourers_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string querySelect = "SELECT * FROM labor where Site=@site";
            MySqlCommand command = new MySqlCommand(querySelect, connection);
            command.Parameters.AddWithValue("@site", site);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            connection.Close();
        }

        private void buttonAddNew_Click_1(object sender, EventArgs e)
        {
            string LaborID = textBoxLabourID.Text;
            string Name = textBoxName.Text;
            //string site = comboBoxSite.Text;
            string NIC = textBoxNIC.Text;
            int phone = 0;
            try
            {
                phone = Int32.Parse(textBoxPhone.Text);
            }
            catch (Exception ex)
            {

            }
            string address = textBoxAddress.Text;


            if (LaborID != "" && Name != "")
            {
                string queryAddLabor = "INSERT INTO labor(`LaborID`,`Site`, `Name`, `NIC`,`Phone`,`Address`) VALUES ('" + LaborID + "','" + site + "', '" + Name + "', '" + NIC + "', '" + phone + "', '" + address + "')";
                //string query1 = "INSERT INTO sites(`SEID`,`SiteName`) VALUES ('" + SEID + "','" + site + "')";

                MySqlConnection databaseConnection = new MySqlConnection(db.connectionString);
                databaseConnection.Open();
                MySqlCommand commandDatabase = new MySqlCommand(queryAddLabor, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                databaseConnection.Close();


                DialogResult result = MessageBox.Show("Data Saved !", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxLabourID.Text = "";
                textBoxName.Text = "";
                //comboBoxSite.Text = "";
                textBoxNIC.Text = "";
                textBoxPhone.Text = "";
                textBoxAddress.Text = "";
                dataGridView1.Refresh();
            }
            else
            {
                DialogResult result = MessageBox.Show("Please fill details !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonLoadByID_Click_1(object sender, EventArgs e)
        {
            string LaborID = textBoxLoad.Text;
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string querySelect = "SELECT * FROM Labor WHERE LaborID = @laborid and Site=@site LIMIT 1";
            MySqlCommand loadCommand = new MySqlCommand(querySelect, connection);
            loadCommand.Parameters.AddWithValue("@laborid", LaborID);
            loadCommand.Parameters.AddWithValue("@site", site);
            using (MySqlDataReader reader = loadCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    textBoxLabourID.Text = (reader["LaborID"].ToString());
                    textBoxName.Text = (reader["Name"].ToString());
                    textBoxNIC.Text = (reader["NIC"].ToString());
                    textBoxPhone.Text = (reader["Phone"].ToString());
                    textBoxAddress.Text = (reader["Address"].ToString());
                }
            }
            connection.Close();
        }

        private void buttonUpdate_Click_1(object sender, EventArgs e)
        {
            string LaborID = textBoxLabourID.Text;
            string Name = textBoxName.Text;
            //string site = comboBoxSite.Text;
            string NIC = textBoxNIC.Text;
            int phone = 0;
            try
            {
                phone = Int32.Parse(textBoxPhone.Text);
            }
            catch (Exception ex)
            {

            }
            string address = textBoxAddress.Text;

            if (LaborID != "")
            {

                string updateQuery = "UPDATE labor SET Name=@name, NIC=@nic, Phone=@phone, Address=@address WHERE LaborID = @LaborID";
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@name", Name);
                //updateCommand.Parameters.AddWithValue("@site", site);
                updateCommand.Parameters.AddWithValue("@nic", NIC);
                updateCommand.Parameters.AddWithValue("@phone", phone);
                updateCommand.Parameters.AddWithValue("@address", address);
                updateCommand.Parameters.AddWithValue("@LaborID", LaborID);
                int rowsAffected = updateCommand.ExecuteNonQuery();
                connection.Close();
                textBoxLabourID.Text = "";
                textBoxName.Text = "";
                //comboBoxSite.Text = "";
                textBoxNIC.Text = "";
                textBoxPhone.Text = "";
                textBoxAddress.Text = "";
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

        private void buttonDelete_Click_1(object sender, EventArgs e)
        {
            string LaborID = textBoxLabourID.Text;
            if (LaborID != "")
            {
                string deleteQuery = "DELETE FROM `labor` WHERE LaborID=@laborid";
                //string updateQuery2 = "DELETE FROM `sites` WHERE SEID=@seid";
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@laborid", LaborID);
                int rowsAffected = deleteCommand.ExecuteNonQuery();


                connection.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Deleted !", "Done", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Labour ID Not Valid !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRefresh_Click_1(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string querySelect = "SELECT * FROM Labor where Site=@site";
            MySqlCommand command = new MySqlCommand(querySelect, connection);
            command.Parameters.AddWithValue("@site", site);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            connection.Close();
        }
    }
}
