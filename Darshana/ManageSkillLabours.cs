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
    public partial class ManageSkillLabours : Form
    {
        Database db = new Database();
        string site;
        public ManageSkillLabours(SiteEngineer se)
        {
            InitializeComponent();
            site = se.site;
        }

        private void buttonLoadByID_Click(object sender, EventArgs e)
        {
            string SkillLaborID = textBoxLoad.Text;
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string querySelect = "SELECT * FROM skilllabor WHERE LaborID = @skilllaborid and Site=@site LIMIT 1";
            MySqlCommand loadCommand = new MySqlCommand(querySelect, connection);
            loadCommand.Parameters.AddWithValue("@skilllaborid", SkillLaborID);
            loadCommand.Parameters.AddWithValue("@site", site);
            using (MySqlDataReader reader = loadCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    textBoxSkilledLabourID.Text = (reader["LaborID"].ToString());
                    textBoxName.Text = (reader["Name"].ToString());
                    textBoxNIC.Text = (reader["NIC"].ToString());
                    textBoxPhone.Text = (reader["Phone"].ToString());
                    textBoxAddress.Text = (reader["Address"].ToString());
                }
            }
            connection.Close();
        }

        private void ManageSkillLabours_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string querySelect = "SELECT * FROM skilllabor where Site=@site";
            MySqlCommand command = new MySqlCommand(querySelect, connection);
            command.Parameters.AddWithValue("@site", site);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            connection.Close();
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            string SkillLaborID = textBoxSkilledLabourID.Text;
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


            if (SkillLaborID != "" && Name != "")
            {
                string queryAddLabor = "INSERT INTO skilllabor(`LaborID`,`Site`, `Name`, `NIC`,`Phone`,`Address`) VALUES ('" + SkillLaborID + "','" + site + "', '" + Name + "', '" + NIC + "', '" + phone + "', '" + address + "')";

                MySqlConnection databaseConnection = new MySqlConnection(db.connectionString);
                databaseConnection.Open();
                MySqlCommand commandDatabase = new MySqlCommand(queryAddLabor, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                databaseConnection.Close();


                DialogResult result = MessageBox.Show("Data Saved !", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxSkilledLabourID.Text = "";
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


        private void buttonUpdate_Click_1(object sender, EventArgs e)
        {
            string SkillLaborID = textBoxSkilledLabourID.Text;
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

            if (SkillLaborID != "")
            {

                string updateQuery = "UPDATE skilllabor SET Name=@name, NIC=@nic, Phone=@phone, Address=@address WHERE LaborID = @SkillLaborID";
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@name", Name);
                //updateCommand.Parameters.AddWithValue("@site", site);
                updateCommand.Parameters.AddWithValue("@nic", NIC);
                updateCommand.Parameters.AddWithValue("@phone", phone);
                updateCommand.Parameters.AddWithValue("@address", address);
                updateCommand.Parameters.AddWithValue("@SkillLaborID", SkillLaborID);
                int rowsAffected = updateCommand.ExecuteNonQuery();
                connection.Close();
                textBoxSkilledLabourID.Text = "";
                textBoxName.Text = "";
                //comboBoxSite.Text = "";
                textBoxNIC.Text = "";
                textBoxPhone.Text = "";
                textBoxAddress.Text = "";
                dataGridView1.Refresh();
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

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string querySelect = "SELECT * FROM skilllabor Site=@site";
            MySqlCommand command = new MySqlCommand(querySelect, connection);
            command.Parameters.AddWithValue("@site", site);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            connection.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string SkillLaborID = textBoxSkilledLabourID.Text;
            if (SkillLaborID != "")
            {
                string deleteQuery = "DELETE FROM `skilllabor` WHERE LaborID=@SkillLaborID and Site=@site";

                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@SkillLaborID", SkillLaborID);
                deleteCommand.Parameters.AddWithValue("@site", site);
                int rowsAffected = deleteCommand.ExecuteNonQuery();


                connection.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Deleted !", "Done", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Skilled Labor ID Not Valid !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Skilled Labor ID Not Valid !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}
