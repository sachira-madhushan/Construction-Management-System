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
    public partial class Materials : Form
    {

        Database db = new Database();
        public Materials()
        {
            InitializeComponent();
        }

        private void Materials_Load(object sender, EventArgs e)
        {
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
                        }
                    }
                }
               

            }

            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string query = "SELECT * FROM materials";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string query = "SELECT * FROM materials";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            connection.Close();
            textBox1.Text = "";
            comboBox1.Text = "";
            textBox3.Text = "";
            richTextBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string material = textBox1.Text;
            string site = comboBox1.Text;
            double cost =Double.Parse(textBox3.Text);
            string date = DateTime.Now.ToString().Split()[0];
            string description = richTextBox1.Text;



            if (material != "" && site != "" && cost != 0.0)
            {
                string query = "INSERT INTO materials(`Date`, `Material`, `Site`, `Cost`,`Description`) VALUES ('" + date + "', '" + material + "', '" + site + "', '" + cost + "', '" + description + "')";

                MySqlConnection databaseConnection = new MySqlConnection(db.connectionString);
                databaseConnection.Open();
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                commandDatabase.CommandTimeout = 60;
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                databaseConnection.Close();
                DialogResult result = MessageBox.Show("Data Saved !", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = "";
                comboBox1.Text = "";
                textBox3.Text = "";
                richTextBox1.Text = "";
                dataGridView1.Refresh();
            }
            else
            {
                DialogResult result = MessageBox.Show("Please fill details !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Enter ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string material = textBox1.Text;
            string site = comboBox1.Text;
            double cost = Double.Parse(textBox3.Text);
            string description = richTextBox1.Text;
            int id =Int32.Parse(textBox2.Text);

            if (material != "" && site != "" && cost != 0.0)
            {
                
                string updateQuery = "UPDATE materials SET Material = @material,Site=@site,Cost=@cost,Description=@description WHERE ID = @id";
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@material", material);
                updateCommand.Parameters.AddWithValue("@site", site);
                updateCommand.Parameters.AddWithValue("@cost", cost);
                updateCommand.Parameters.AddWithValue("@description", description);
                updateCommand.Parameters.AddWithValue("@id", id);

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

        private void button5_Click(object sender, EventArgs e)
        {

            if (textBox2.Text == "")
            {
                MessageBox.Show("Enter ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (MySqlConnection c = new MySqlConnection(db.connectionString))
            {
                c.Open();


                // Create a MySqlCommand
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM materials where ID="+Int32.Parse(textBox2.Text), c))
                {
                    // Execute the query and read the results
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Add each value to the ComboBox
                            textBox1.Text = reader["Material"].ToString();
                            comboBox1.Text= reader["Site"].ToString();
                            textBox3.Text= reader["Cost"].ToString();
                            richTextBox1.Text= reader["Description"].ToString();
                        }
                    }
                }


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string id = textBox2.Text;

            if (id != "")
            {
                string updateQuery = "DELETE FROM Materials WHERE ID=@id";
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@id", id);
                int rowsAffected = updateCommand.ExecuteNonQuery();
                connection.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Deleted !", "Done", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Enter ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
