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
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace Darshana
{
   
    public partial class SiteEngineer : Form
    {
        Database db = new Database();
        public string site;
        string id;
        public SiteEngineer(SELogin sl)
        {
            InitializeComponent();
            id = sl.Get_SEID();
        }
        
        
        private void SiteEngineer_Load(object sender, EventArgs e)
        {
            SELogin sl = new SELogin();
            label11.Text ="SEID :"+id;

            timer1.Start();
            using (MySqlConnection c = new MySqlConnection(db.connectionString))
            {
                c.Open();

                // Create a MySqlCommand
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM sites where SEID=@id LIMIT 1", c))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    // Execute the query and read the results
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            site = reader["SiteName"].ToString();
                            label2.Text = "Site :" + site;
                        }
                    }
                }
                //end of combo boxes

            }
            //data base loading part
            string date = DateTime.Now.ToString().Split()[0].Trim();

            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            // WHERE attendancelabor.AttendanceDate ="+date+"
            string query = "SELECT labor.LaborID as LaborerID,attendancelabor.AttendanceDate,attendancelabor.Site,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.AttendanceDate =@date AND labor.Site=@site;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@site", site);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            connection.Close();

            connection.Open();
            string query2 = "SELECT supervisor.SupervisorID,attendancesupervisor.AttendanceDate,attendancesupervisor.Site,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.AttendanceDate = @date AND supervisor.Site=@site; ";
            MySqlCommand command2 = new MySqlCommand(query2, connection);
            command2.Parameters.AddWithValue("@date", date);
            command2.Parameters.AddWithValue("@site", site);
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            dataGridView2.DataSource = dataTable2;
            dataGridView2.Refresh();
            connection.Close();
            //database loading part end

            connection.Open();
            string query3 = "SELECT * from materials WHERE Date = @date AND Site=@site; ";
            MySqlCommand command3 = new MySqlCommand(query3, connection);
            command3.Parameters.AddWithValue("@date", date);
            command3.Parameters.AddWithValue("@site", site);
            MySqlDataAdapter adapter3 = new MySqlDataAdapter(command3);
            DataTable dataTable3 = new DataTable();
            adapter3.Fill(dataTable3);
            dataGridView3.DataSource = dataTable3;
            dataGridView3.Refresh();
            connection.Close();

            connection.Open();
            // WHERE attendancelabor.AttendanceDate ="+date+"
            string query4 = "SELECT skilllabor.LaborID as LaborerID,attendanceskilllabor.AttendanceDate,attendanceskilllabor.Site,CASE WHEN attendanceskilllabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendanceskilllabor.OT,attendanceskilllabor.Advance,attendanceskilllabor.FA as Food FROM skilllabor LEFT JOIN attendanceskilllabor ON skilllabor.LaborID = attendanceskilllabor.LaborerID WHERE attendanceskilllabor.AttendanceDate =@date AND skilllabor.Site=@site;";
            MySqlCommand command4 = new MySqlCommand(query4, connection);
            command4.Parameters.AddWithValue("@date", date);
            command4.Parameters.AddWithValue("@site", site);
            MySqlDataAdapter adapter4 = new MySqlDataAdapter(command4);
            DataTable dataTable4 = new DataTable();
            adapter4.Fill(dataTable4);
            dataGridView4.DataSource = dataTable4;
            dataGridView4.Refresh();
            connection.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Text;
            using (MySqlConnection c = new MySqlConnection(db.connectionString))
            {
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                string query = "SELECT labor.LaborID as LaborerID,attendancelabor.AttendanceDate,attendancelabor.Site,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.AttendanceDate =@date and attendancelabor.Site=@site; ";
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

        private void button16_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker4.Text;
            using (MySqlConnection c = new MySqlConnection(db.connectionString))
            {
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                string query = "SELECT skilllabor.LaborID as LaborerID,attendanceskilllabor.AttendanceDate,attendanceskilllabor.Site,CASE WHEN attendanceskilllabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendanceskilllabor.OT,attendanceskilllabor.Advance,attendanceskilllabor.FA as Food FROM skilllabor LEFT JOIN attendanceskilllabor ON skilllabor.LaborID = attendanceskilllabor.LaborerID WHERE attendanceskilllabor.AttendanceDate =@date and attendanceskilllabor.Site =@site;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@site", site);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView4.DataSource = dataTable;
                dataGridView4.Refresh();
                connection.Close();

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker2.Text;
            using (MySqlConnection c = new MySqlConnection(db.connectionString))
            {
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                string query2 = "SELECT supervisor.SupervisorID,attendancesupervisor.Site,attendancesupervisor.AttendanceDate,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.AttendanceDate =@date and attendancesupervisor.Site=@site; ";
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

        private void button14_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker3.Text;
            using (MySqlConnection c = new MySqlConnection(db.connectionString))
            {
                MySqlConnection connection = new MySqlConnection(db.connectionString);
                connection.Open();
                string query = "SELECT * from materials WHERE Date = @date and Site=@site; ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@site", site);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView3.DataSource = dataTable;
                dataGridView3.Refresh();
                connection.Close();

            }
        }
        MySqlDataAdapter adapterReport;
        private void button9_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string date = DateTime.Now.ToString().Split()[0].Trim();
            // Create a MySQL command to retrieve data


            string query;
            MySqlCommand command;
            DataTable dataTable;
            int totalAttendance = 0;
            int totalOT = 0;
            double totalAdvance = 0;
            double totalFood = 0;

           
                query = "SELECT labor.LaborID as LaborerID,attendancelabor.AttendanceDate as Date,attendancelabor.Site,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS Attendance_Status,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.AttendanceDate =@date and attendancelabor.Site=@site; ";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", dateTimePicker1.Text.ToString());
                command.Parameters.AddWithValue("@site", site);
                adapterReport = new MySqlDataAdapter(command);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Attendance_Status"].ToString() == "Present")
                        {
                            totalAttendance++;
                        }
                        totalOT += Int32.Parse(reader["OT"].ToString());
                        totalAdvance += double.Parse(reader["Advance"].ToString());
                        totalFood += double.Parse(reader["Food"].ToString());



                    }
                }
            


            dataTable = new DataTable();
            adapterReport.Fill(dataTable);
            // Close the MySQL connection when done
            connection.Close();

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Save PDF File";
                saveFileDialog.FileName = "Report.pdf"; // Default filename
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected save path and filename
                    string savePath = saveFileDialog.FileName;

                    // Create a PDF document
                    Document document = new Document(PageSize.A4);
                    PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream(savePath, System.IO.FileMode.Create));

                    // Add content to the PDF document (replace with your content creation code)
                    document.Open();
                    // Create a paragraph with custom text
                    Paragraph text2 = new Paragraph("Sachira Company");
                    // Set the alignment of the paragraph to center
                    text2.Alignment = Element.ALIGN_CENTER;
                    document.Add(text2);

                    Paragraph text = new Paragraph("Labour Daily Attendance Report");
                    // Set the alignment of the paragraph to center
                    text.Alignment = Element.ALIGN_CENTER;
                    document.Add(text);

                    document.Add(new Paragraph("Site:"+site));
                    document.Add(new Paragraph("Date:" + dateTimePicker1.Text.ToString()));

                    document.Add(new Paragraph(" "));

                    PdfPTable table = new PdfPTable(dataTable.Columns.Count);
                    // Add table headers
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        table.AddCell(column.ColumnName);
                    }

                    // Add table rows
                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            table.AddCell(item.ToString());
                        }
                    }

                    document.Add(table);
                    document.Add(new Paragraph(""));
                    document.Add(new Paragraph("Total Attendance:" + totalAttendance));
                    document.Add(new Paragraph("Total OT Hours:" + totalOT));
                    document.Add(new Paragraph("Total Advance:Rs." + totalAdvance));
                    document.Add(new Paragraph("Total Food and Accommodation:Rs." + totalFood));
                    document.Close();

                    MessageBox.Show("PDF file saved successfully.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {

            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string date = DateTime.Now.ToString().Split()[0].Trim();
            // Create a MySQL command to retrieve data


            string query;
            MySqlCommand command;
            DataTable dataTable;
            int totalAttendance = 0;
            int totalOT = 0;
            double totalAdvance = 0;
            double totalFood = 0;


                query = "SELECT skilllabor.LaborID as LaborerID,attendanceskilllabor.AttendanceDate,attendanceskilllabor.Site,CASE WHEN attendanceskilllabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendanceskilllabor.OT,attendanceskilllabor.Advance,attendanceskilllabor.FA as Food FROM skilllabor LEFT JOIN attendanceskilllabor ON skilllabor.LaborID = attendanceskilllabor.LaborerID WHERE attendanceskilllabor.AttendanceDate =@date and attendanceskilllabor.Site=@site;";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", dateTimePicker4.Text.ToString());
                command.Parameters.AddWithValue("@site", site);
                adapterReport = new MySqlDataAdapter(command);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["AttendanceStatus"].ToString() == "Present")
                        {
                            totalAttendance++;
                        }
                        totalOT += Int32.Parse(reader["OT"].ToString());
                        totalAdvance += double.Parse(reader["Advance"].ToString());
                        totalFood += double.Parse(reader["Food"].ToString());



                    }
                }
            


            dataTable = new DataTable();
            adapterReport.Fill(dataTable);
            // Close the MySQL connection when done
            connection.Close();

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Save PDF File";
                saveFileDialog.FileName = "Report.pdf"; // Default filename
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected save path and filename
                    string savePath = saveFileDialog.FileName;

                    // Create a PDF document
                    Document document = new Document(PageSize.A4);
                    PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream(savePath, System.IO.FileMode.Create));

                    // Add content to the PDF document (replace with your content creation code)
                    document.Open();
                    // Create a paragraph with custom text
                    Paragraph text2 = new Paragraph("Sachira Company");
                    // Set the alignment of the paragraph to center
                    text2.Alignment = Element.ALIGN_CENTER;
                    document.Add(text2);

                    Paragraph text = new Paragraph("Skilled Labour Daily Attendance Report");
                    // Set the alignment of the paragraph to center
                    text.Alignment = Element.ALIGN_CENTER;
                    document.Add(text);

                        document.Add(new Paragraph("Site:"+site));
                        document.Add(new Paragraph("Date:" + dateTimePicker4.Text.ToString()));


                    document.Add(new Paragraph(" "));

                    PdfPTable table = new PdfPTable(dataTable.Columns.Count);
                    // Add table headers
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        table.AddCell(column.ColumnName);
                    }

                    // Add table rows
                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            table.AddCell(item.ToString());
                        }
                    }

                    document.Add(table);
                    document.Add(new Paragraph(""));
                    document.Add(new Paragraph("Total Attendance:" + totalAttendance));
                    document.Add(new Paragraph("Total OT Hours:" + totalOT));
                    document.Add(new Paragraph("Total Advance:Rs." + totalAdvance));
                    document.Add(new Paragraph("Total Food and Accommodation:Rs." + totalFood));
                    document.Close();

                    MessageBox.Show("PDF file saved successfully.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string date = DateTime.Now.ToString().Split()[0].Trim();
            // Create a MySQL command to retrieve data


            string query;
            MySqlCommand command;
            DataTable dataTable;
            int totalAttendance = 0;
            int totalOT = 0;
            double totalAdvance = 0;
            double totalFood = 0;

                query = "SELECT supervisor.SupervisorID,attendancesupervisor.AttendanceDate,attendancesupervisor.Site,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS Attendance_Status,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.AttendanceDate = @date and attendancesupervisor.Site=@site; ";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", dateTimePicker2.Text.ToString());
                command.Parameters.AddWithValue("@site", site);
                adapterReport = new MySqlDataAdapter(command);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Attendance_Status"].ToString() == "Present")
                        {
                            totalAttendance++;
                        }
                        totalOT += Int32.Parse(reader["OT"].ToString());
                        totalAdvance += double.Parse(reader["Advance"].ToString());
                        totalFood += double.Parse(reader["Food"].ToString());



                    }
                }


            dataTable = new DataTable();
            adapterReport.Fill(dataTable);
            // Close the MySQL connection when done
            connection.Close();

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Save PDF File";
                saveFileDialog.FileName = "Report.pdf"; // Default filename
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected save path and filename
                    string savePath = saveFileDialog.FileName;

                    // Create a PDF document
                    Document document = new Document(PageSize.A4);
                    PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream(savePath, System.IO.FileMode.Create));

                    // Add content to the PDF document (replace with your content creation code)
                    document.Open();
                    // Create a paragraph with custom text
                    Paragraph text2 = new Paragraph("Sachira Company");
                    // Set the alignment of the paragraph to center
                    text2.Alignment = Element.ALIGN_CENTER;
                    document.Add(text2);

                    Paragraph text = new Paragraph("Supervisor Daily Attendance Report");
                    // Set the alignment of the paragraph to center
                    text.Alignment = Element.ALIGN_CENTER;
                    document.Add(text);


                        document.Add(new Paragraph("Site:"+site));
                        document.Add(new Paragraph("Date:" + dateTimePicker2.Text.ToString()));


                    document.Add(new Paragraph(" "));

                    PdfPTable table = new PdfPTable(dataTable.Columns.Count);
                    // Add table headers
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        table.AddCell(column.ColumnName);
                    }

                    // Add table rows
                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            table.AddCell(item.ToString());
                        }
                    }

                    document.Add(table);
                    document.Add(new Paragraph(""));
                    document.Add(new Paragraph("Total Attendance:" + totalAttendance));
                    document.Add(new Paragraph("Total OT Hours:" + totalOT));
                    document.Add(new Paragraph("Total Advance:Rs." + totalAdvance));
                    document.Add(new Paragraph("Total Food and Accommodation:Rs." + totalFood));
                    document.Close();

                    MessageBox.Show("PDF file saved successfully.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            string date = DateTime.Now.ToString().Split()[0].Trim();
            // Create a MySQL command to retrieve data


            string query;
            MySqlCommand command;
            DataTable dataTable;
            double totalCost = 0;

                query = "SELECT * from materials WHERE Date = @date and Site=@site; ";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", dateTimePicker3.Text.ToString());
                command.Parameters.AddWithValue("@site", site);
                adapterReport = new MySqlDataAdapter(command);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        totalCost += Int32.Parse(reader["Cost"].ToString());

                    }
                }


            dataTable = new DataTable();
            adapterReport.Fill(dataTable);
            // Close the MySQL connection when done
            connection.Close();

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Save PDF File";
                saveFileDialog.FileName = "Report.pdf"; // Default filename
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected save path and filename
                    string savePath = saveFileDialog.FileName;

                    // Create a PDF document
                    Document document = new Document(PageSize.A4);
                    PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream(savePath, System.IO.FileMode.Create));

                    // Add content to the PDF document (replace with your content creation code)
                    document.Open();
                    // Create a paragraph with custom text
                    Paragraph text2 = new Paragraph("Sachira Company");
                    // Set the alignment of the paragraph to center
                    text2.Alignment = Element.ALIGN_CENTER;
                    document.Add(text2);

                    Paragraph text = new Paragraph("Material Cost Report");
                    // Set the alignment of the paragraph to center
                    text.Alignment = Element.ALIGN_CENTER;
                    document.Add(text);


                        document.Add(new Paragraph("Site:"+site));
                        document.Add(new Paragraph("Date:" + dateTimePicker3.Text.ToString()));

                    document.Add(new Paragraph(" "));

                    PdfPTable table = new PdfPTable(dataTable.Columns.Count);
                    // Add table headers
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        table.AddCell(column.ColumnName);
                    }

                    // Add table rows
                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            table.AddCell(item.ToString());
                        }
                    }

                    document.Add(table);
                    document.Add(new Paragraph(""));
                    document.Add(new Paragraph("Total Cost:Rs." + totalCost));

                    document.Close();

                    MessageBox.Show("PDF file saved successfully.", "Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            LaborersAttendance la = new LaborersAttendance(this);
            la.Show();
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
            User us = new User();
            this.Hide();
            us.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            SkilledLabourerAttendance sl = new SkilledLabourerAttendance(this);
            sl.Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            SupervisorAttendance sa = new SupervisorAttendance(this);
            sa.Show();
        }
    }
}
