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
    public partial class ManageLabours : Form
    {
        Database db = new Database();
        public ManageLabours()
        {
            InitializeComponent();
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            string LaborID = textBoxLabourID.Text;
            string Name = textBoxName.Text;
            string site = comboBoxSite.Text;
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
            //string password = textBox7.Text;

            if (LaborID != "" && Name != "" && site != "" && phone != 0 && phone.ToString().Length == 10)
            {
                string queryAddLabor = "INSERT INTO labor(`LaborID`, `Name`, `Site`, `NIC`,`Phone`,`Address`) VALUES ('" + LaborID + "', '" + Name + "', '" + site + "', '" + NIC + "', '" + phone + "', '" + address + "')";
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
                comboBoxSite.Text = "";
                textBoxNIC.Text = "";
                textBoxPhone.Text = "";
                textBoxAddress.Text = "";
                dataGridView1.Refresh();
            }
            else
            {
                if(phone == 0 || phone.ToString().Length != 10)
                {
                    MessageBox.Show("Enter valid phone number !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Please fill details !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string querySelect = "SELECT * FROM Labor";
            MySqlCommand command = new MySqlCommand(querySelect, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            connection.Close();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            string LaborID = textBoxLabourID.Text;
            string Name = textBoxName.Text;
            string site = comboBoxSite.Text;
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

            if (LaborID != "" && phone != 0 && phone.ToString().Length == 10)
            {

                string updateQuery = "UPDATE labor SET Name=@name, site=@site, NIC=@nic, Phone=@phone, Address=@address WHERE LaborID = @LaborID";
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@name", Name);
                updateCommand.Parameters.AddWithValue("@site", site);
                updateCommand.Parameters.AddWithValue("@nic", NIC);
                updateCommand.Parameters.AddWithValue("@phone", phone);
                updateCommand.Parameters.AddWithValue("@address", address);
                updateCommand.Parameters.AddWithValue("@LaborID", LaborID);
                int rowsAffected = updateCommand.ExecuteNonQuery();
                connection.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Update success !", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if(phone == 0 || phone.ToString().Length != 10)
                {
                    MessageBox.Show("Enter valid phone number !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Site ID Not Valid !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ManageLabours_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string querySelect = "SELECT * FROM Labor";
            MySqlCommand command = new MySqlCommand(querySelect, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            

            string querySiteSelect = "SELECT * FROM sites";
            MySqlCommand comm = new MySqlCommand(querySiteSelect, connection);
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(comm);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            for(int i=0; i<dt.Rows.Count; i++)
            {
                comboBoxSite.Items.Add(dt.Rows[i]["SiteName"]);
            }
            connection.Close();
        }
    }
}
