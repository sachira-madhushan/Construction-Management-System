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
    public partial class PMLogin : Form
    {

        Database db = new Database();
        public PMLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = new User();
            this.Hide();
            user.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string password = textBox2.Text;
            string con=db.connectionString;
            string querySearch = "SELECT * FROM projectmanager where PMID = @id Limit 1";
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            MySqlCommand userCheck = new MySqlCommand(querySearch, connection);
            userCheck.Parameters.AddWithValue("@id", id);
            string user = userCheck.ExecuteScalar()?.ToString();
            if (user != null)
            {
                string pass = "SELECT Password FROM projectmanager where PMID = @id Limit 1";
                MySqlConnection Passconnection = new MySqlConnection(con);
                MySqlCommand passCheck = new MySqlCommand(pass, connection);
                passCheck.Parameters.AddWithValue("@id", id);
                try
                {
                    string storedPassword = passCheck.ExecuteScalar().ToString();
                    if (storedPassword == password)
                    {
                        ProjectManager pm = new ProjectManager();
                        this.Hide();
                        pm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalied ID or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalied ID or Password"+ ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                
            }
            else
            {
                MessageBox.Show("Invalied ID or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void PMLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.A)
            {
                ProjectManager pm = new ProjectManager();
                this.Hide();
                pm.Show();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.A)
            {
                ProjectManager pm = new ProjectManager();
                this.Hide();
                pm.Show();
            }
        }
    }
}
