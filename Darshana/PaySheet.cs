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
    public partial class PaySheet : Form
    {
        Database db = new Database();
        public PaySheet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string from = dateTimePicker1.Text;
            string to = dateTimePicker2.Text;
            string type = comboBox1.Text;
            string id = textBox1.Text;
            MySqlConnection connection = new MySqlConnection(db.connectionString);
            if (type != null && id  !=null)
            {
                if (type == "Supervisor")
                {
                    
                    connection.Open();
                    string query2 = "SELECT attendancesupervisor.AttendanceDate,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.SupervisorID=@id AND STR_TO_DATE(attendancesupervisor.AttendanceDate, '%m/%d/%Y') BETWEEN STR_TO_DATE(@from, '%m/%d/%Y') AND STR_TO_DATE(@to, '%m/%d/%Y');";
                    MySqlCommand command2 = new MySqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("@id", id);
                    command2.Parameters.AddWithValue("@from", from);
                    command2.Parameters.AddWithValue("@to", to);
                    MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);
                    DataTable dataTable2 = new DataTable();
                    adapter2.Fill(dataTable2);
                    dataGridView1.DataSource = dataTable2;
                    dataGridView1.Refresh();
                    connection.Close();
                    //database loading part end
                   
                }
                else if (type == "Labourer")
                {
                    connection.Open();
                    // WHERE attendancelabor.AttendanceDate ="+date+"
                    string query = "SELECT attendancelabor.AttendanceDate,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.LaborerID =@id AND STR_TO_DATE(attendancelabor.AttendanceDate, '%m/%d/%Y') BETWEEN STR_TO_DATE(@from, '%m/%d/%Y') AND STR_TO_DATE(@to, '%m/%d/%Y');";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@from", from);
                    command.Parameters.AddWithValue("@to", to);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                    dataGridView1.Refresh();
                    connection.Close();
                }
                else
                {
                    connection.Open();
                    // WHERE attendancelabor.AttendanceDate ="+date+"
                    string query4 = "SELECT attendanceskilllabor.AttendanceDate,CASE WHEN attendanceskilllabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendanceskilllabor.OT,attendanceskilllabor.Advance,attendanceskilllabor.FA as Food FROM skilllabor LEFT JOIN attendanceskilllabor ON skilllabor.LaborID = attendanceskilllabor.LaborerID WHERE attendanceskilllabor.LaborerID =@id AND STR_TO_DATE(attendanceskilllabor.AttendanceDate, '%m/%d/%Y') BETWEEN STR_TO_DATE(@from, '%m/%d/%Y') AND STR_TO_DATE(@to, '%m/%d/%Y');";
                    MySqlCommand command4 = new MySqlCommand(query4, connection);
                    command4.Parameters.AddWithValue("@id", id);
                    command4.Parameters.AddWithValue("@from", from);
                    command4.Parameters.AddWithValue("@to", to);
                    MySqlDataAdapter adapter4 = new MySqlDataAdapter(command4);
                    DataTable dataTable4 = new DataTable();
                    adapter4.Fill(dataTable4);
                    dataGridView1.DataSource = dataTable4;
                    dataGridView1.Refresh();
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select ID and Type !","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        MySqlDataAdapter adapterReport;
        private void button2_Click(object sender, EventArgs e)
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

            string from = dateTimePicker1.Text;
            string to = dateTimePicker2.Text;
            string type = comboBox1.Text;
            string id = textBox1.Text;

            string name="";
            string site="";

            //to day all site report
            if (type != null && id != null)
            {
                if (type == "Supervisor")
                {

                    string query2 = "SELECT attendancesupervisor.AttendanceDate,CASE WHEN attendancesupervisor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancesupervisor.OT,attendancesupervisor.Advance,attendancesupervisor.FA as Food FROM supervisor LEFT JOIN attendancesupervisor ON supervisor.SupervisorID = attendancesupervisor.SupervisorID WHERE attendancesupervisor.SupervisorID=@id AND STR_TO_DATE(attendancesupervisor.AttendanceDate, '%m/%d/%Y') BETWEEN STR_TO_DATE(@from, '%m/%d/%Y') AND STR_TO_DATE(@to, '%m/%d/%Y');";
                    MySqlCommand command2 = new MySqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("@id", id);
                    command2.Parameters.AddWithValue("@from", from);
                    command2.Parameters.AddWithValue("@to", to);
                    adapterReport = new MySqlDataAdapter(command2);
                    using (MySqlDataReader reader = command2.ExecuteReader())
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
                    //database loading part end
                    string query5 = "SELECT * from supervisor where SupervisorID=@id LIMIT 1;";
                    MySqlCommand command5 = new MySqlCommand(query5, connection);
                    command5.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = command5.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            name += reader["Name"].ToString();
                            site += reader["Site"].ToString();

                        }
                    }
                }
                else if (type == "Labourer")
                {
                    // WHERE attendancelabor.AttendanceDate ="+date+"
                    string query1 = "SELECT attendancelabor.AttendanceDate,CASE WHEN attendancelabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendancelabor.OT,attendancelabor.Advance,attendancelabor.FA as Food FROM labor LEFT JOIN attendancelabor ON labor.LaborID = attendancelabor.LaborerID WHERE attendancelabor.LaborerID =@id AND STR_TO_DATE(attendancelabor.AttendanceDate, '%m/%d/%Y') BETWEEN STR_TO_DATE(@from, '%m/%d/%Y') AND STR_TO_DATE(@to, '%m/%d/%Y');";
                    MySqlCommand command1 = new MySqlCommand(query1, connection);
                    command1.Parameters.AddWithValue("@id", id);
                    command1.Parameters.AddWithValue("@from", from);
                    command1.Parameters.AddWithValue("@to", to);
                    adapterReport = new MySqlDataAdapter(command1);
                    using (MySqlDataReader reader = command1.ExecuteReader())
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

                    string query5 = "SELECT * from labor where LaborID=@id LIMIT 1;";
                    MySqlCommand command5 = new MySqlCommand(query5, connection);
                    command5.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = command5.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            name += reader["Name"].ToString();
                            site += reader["Site"].ToString();
                        }
                    }
                }
                else
                {
                    // WHERE attendancelabor.AttendanceDate ="+date+"
                    string query4 = "SELECT attendanceskilllabor.AttendanceDate,CASE WHEN attendanceskilllabor.IsPresent = 1 THEN 'Present' ELSE 'Absent' END AS AttendanceStatus,attendanceskilllabor.OT,attendanceskilllabor.Advance,attendanceskilllabor.FA as Food FROM skilllabor LEFT JOIN attendanceskilllabor ON skilllabor.LaborID = attendanceskilllabor.LaborerID WHERE attendanceskilllabor.LaborerID =@id AND STR_TO_DATE(attendanceskilllabor.AttendanceDate, '%m/%d/%Y') BETWEEN STR_TO_DATE(@from, '%m/%d/%Y') AND STR_TO_DATE(@to, '%m/%d/%Y');";
                    MySqlCommand command4 = new MySqlCommand(query4, connection);
                    command4.Parameters.AddWithValue("@id", id);
                    command4.Parameters.AddWithValue("@from", from);
                    command4.Parameters.AddWithValue("@to", to);
                    adapterReport = new MySqlDataAdapter(command4);
                    using (MySqlDataReader reader = command4.ExecuteReader())
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

                    string query5 = "SELECT * from skilllabor where LaborID=@id LIMIT 1;";
                    MySqlCommand command5 = new MySqlCommand(query5, connection);
                    command5.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = command5.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            name += reader["Name"].ToString();
                            site += reader["Site"].ToString();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select ID and Type !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    Paragraph text = new Paragraph("Pay Sheet Report");
                    // Set the alignment of the paragraph to center
                    text.Alignment = Element.ALIGN_CENTER;
                    document.Add(text);

                    document.Add(new Paragraph("ID:"+id));
                    document.Add(new Paragraph("Name:"+name));
                    document.Add(new Paragraph("Site:"+site));
                    document.Add(new Paragraph("From:" + from));
                    document.Add(new Paragraph("To:" + to));
                    

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
