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
    public partial class ProjectManager : Form
    {
        Database db = new Database();
        int reportID = 0;
        int reportIDSuper = 0;
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
                    reportID = 1;

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
                    reportID = 2;

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
                    reportIDSuper = 1;

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
                    reportIDSuper = 2;

                }
            }
        }

        
        private void button6_Click_1(object sender, EventArgs e)
        {
            reportID = 0;
            string date = DateTime.Now.ToString().Split()[0].Trim();

            MySqlConnection connection = new MySqlConnection(db.connectionString);
            connection.Open();
            // WHERE attendancelabor.AttendanceDate ="+date+"
            string query = "SELECT labor.Name,attendancelabor.AttendanceDate,attendancelabor.Site,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.AttendanceDate =@date;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@date", date);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();
            connection.Close();

            
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            reportIDSuper = 0;
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


        //labourer report generator
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
            int totalAttendance=0;
            int totalOT=0;
            double totalAdvance=0;
            double totalFood=0;

            //to day all site report
            if (reportID == 0)
            {
                query = "SELECT labor.Name,attendancelabor.AttendanceDate as Date,attendancelabor.Site,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS Attendance_Status,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.AttendanceDate =@date;";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", date);
                adapterReport = new MySqlDataAdapter(command);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Attendance_Status"].ToString()=="Present")
                        {
                            totalAttendance++;
                        }
                        totalOT += Int32.Parse(reader["OT"].ToString());
                        totalAdvance += double.Parse(reader["Advance"].ToString());
                        totalFood += double.Parse(reader["Food"].ToString());



                    }
                }
            }
            //date and site changed
            else if (reportID == 1)
            {
                string site = comboBox1.Text;
                query = "SELECT labor.Name,attendancelabor.AttendanceDate as Date,attendancelabor.Site,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS Attendance_Status,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.AttendanceDate =@date and attendancelabor.Site=@site; ";
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
            }
            //all sites and date changed
            else if(reportID == 2)
            {
                query = "SELECT labor.Name,attendancelabor.AttendanceDate as Date,attendancelabor.Site,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS Attendance_Status,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.AttendanceDate = @date; ";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", dateTimePicker1.Text.ToString());
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

                    if (reportID == 0)
                    {
                        document.Add(new Paragraph("Site:All Sites"));
                        document.Add(new Paragraph("Date:" + date));
                    }
                    else if (reportID == 1)
                    {
                        document.Add(new Paragraph("Site:" + comboBox1.Text));
                        document.Add(new Paragraph("Date:" + dateTimePicker1.Text.ToString()));
                    }
                    else if (reportID == 2)
                    {
                        document.Add(new Paragraph("Site:All Sites"));
                        document.Add(new Paragraph("Date:" + dateTimePicker1.Text.ToString()));
                    }
                    
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

                    MessageBox.Show("PDF file saved successfully.","Report",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }



            
        }


        //supervisor report generator
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

            //to day all site report
            if (reportIDSuper == 0)
            {
                query = "SELECT supervisor.Name,attendancesupervisor.AttendanceDate,attendancesupervisor.Site,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS Attendance_Status,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.AttendanceDate = @date; ";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", date);
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
            }
            //date and site changed
            else if (reportIDSuper == 1)
            {
                string site = comboBox2.Text;
                query = "SELECT supervisor.Name,attendancesupervisor.AttendanceDate,attendancesupervisor.Site,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS Attendance_Status,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.AttendanceDate = @date and attendancesupervisor.Site=@site; ";
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
            }
            //all sites and date changed
            else if (reportIDSuper == 2)
            {
                query = "SELECT supervisor.Name,attendancesupervisor.AttendanceDate,attendancesupervisor.Site,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS Attendance_Status,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.AttendanceDate = @date; ";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", dateTimePicker2.Text.ToString());
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

                    if (reportIDSuper == 0)
                    {
                        document.Add(new Paragraph("Site:All Sites"));
                        document.Add(new Paragraph("Date:" + date));
                    }
                    else if (reportIDSuper == 1)
                    {
                        document.Add(new Paragraph("Site:" + comboBox2.Text));
                        document.Add(new Paragraph("Date:" + dateTimePicker2.Text.ToString()));
                    }
                    else if (reportIDSuper == 2)
                    {
                        document.Add(new Paragraph("Site:All Sites"));
                        document.Add(new Paragraph("Date:" + dateTimePicker2.Text.ToString()));
                    }

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
    }
}
