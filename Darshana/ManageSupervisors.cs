﻿using System;
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
    
    public partial class ManageSupervisors : Form
    {
        Database db = new Database();
        string site;
        public ManageSupervisors(SiteEngineer se)
        {
            InitializeComponent();
        }

        private void ManageSupervisors_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string querySelect = "SELECT * FROM supervisor where Site=@site";
            MySqlCommand command = new MySqlCommand(querySelect, connection);
            command.Parameters.AddWithValue("@site", site);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            connection.Close();
        }

        private void buttonLoadByID_Click(object sender, EventArgs e)
        {
            string SupervisorID = textBoxLoad.Text;
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string querySelect = "SELECT * FROM supervisor WHERE SupervisorID = @supervisorid and Site=@site LIMIT 1";
            MySqlCommand loadCommand = new MySqlCommand(querySelect, connection);
            loadCommand.Parameters.AddWithValue("@supervisorid", SupervisorID);
            loadCommand.Parameters.AddWithValue("@site", site);
            using (MySqlDataReader reader = loadCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    textBoxSupervisorID.Text = (reader["SupervisorID"].ToString());
                    textBoxName.Text = (reader["Name"].ToString());
                    textBoxNIC.Text = (reader["NIC"].ToString());
                    textBoxPhone.Text = (reader["Phone"].ToString());
                    textBoxAddress.Text = (reader["Address"].ToString());
                }
            }
            connection.Close();
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            string SupervisorID = textBoxSupervisorID.Text;
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

            if (SupervisorID != "" && Name != "")
            {
                string queryAddSupervisor = "INSERT INTO supervisor(`SupervisorID`,`Site`, `Name`, `NIC`,`Phone`,`Address`) VALUES ('" + SupervisorID + "','" + site + "', '" + Name + "', '" + NIC + "', '" + phone + "', '" + address + "')";

                MySqlConnection databaseConnection = new MySqlConnection(db.connectionString);
                databaseConnection.Open();
                MySqlCommand commandDatabase = new MySqlCommand(queryAddSupervisor, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                databaseConnection.Close();


                DialogResult result = MessageBox.Show("Data Saved !", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxSupervisorID.Text = "";
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

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            string SupervisorID = textBoxSupervisorID.Text;
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

            if (SupervisorID != "")
            {

                string updateQuery = "UPDATE supervisor SET Name=@name, NIC=@nic, Phone=@phone, Address=@address WHERE SupervisorID = @SupervisorID and Site=@site";
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@name", Name);
                //updateCommand.Parameters.AddWithValue("@site", site);
                updateCommand.Parameters.AddWithValue("@nic", NIC);
                updateCommand.Parameters.AddWithValue("@site", site);
                updateCommand.Parameters.AddWithValue("@phone", phone);
                updateCommand.Parameters.AddWithValue("@address", address);
                updateCommand.Parameters.AddWithValue("@SupervisorID", SupervisorID);
                int rowsAffected = updateCommand.ExecuteNonQuery();
                connection.Close();
                textBoxSupervisorID.Text = "";
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
            string querySelect = "SELECT * FROM Supervisor where Site=@site";
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
            string SupervisorID = textBoxSupervisorID.Text;
            if (SupervisorID != "")
            {
                string deleteQuery = "DELETE FROM `supervisor` WHERE SupervisorID=@supervisorid and Site=@site";
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@supervisorid", SupervisorID);
                deleteCommand.Parameters.AddWithValue("@site", site);
                int rowsAffected = deleteCommand.ExecuteNonQuery();

                connection.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Deleted !", "Done", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Supervisor ID Not Valid !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

