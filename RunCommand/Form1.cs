using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunCommand
{
    public partial class Form1 : Form
    {
        SQLiteConnection conn;
        SQLiteCommand cmd;
        SQLiteDataReader dr;
        string path = "deneme.db";
        string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";
        public Form1()
        {
            InitializeComponent();
            Create_db();
            data_show();
           // comboBox1.Items.Add("ping google.com");
           // comboBox1.Items.Add("dir");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                    }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Create_db()
        {
            if (!System.IO.File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                using (var sqlite = new SQLiteConnection(@"Data Source="+ path))
                {
                    sqlite.Open();
                    string sql = "CREATE TABLE komut (id INTEGER, yol TEXT, PRIMARY KEY(id AUTOINCREMENT))";
                    SQLiteCommand command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();

                }

            }



        }
        private void data_show()
        {
            try
            {
                var con = new SQLiteConnection(cs);
                con.Open();

                string stm = "select * FROM komut";
                var cmd = new SQLiteCommand(stm, con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    comboBox1.Items.Add(dr.GetValue(1));
                    //dataGridView1.Rows.Insert(0, dr.GetValue(0).ToString(), dr.GetValue(1).ToString(), dr.GetValue(2).ToString());

                }
                comboBox1.SelectedIndex = 0;

                dr.Close();
            }
            catch (Exception ex)
            { }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string q = "";
            try
            {
                String komut = comboBox1.Text;
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/C " + komut;

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                process.Start();

             
    
             
                    while (!process.HasExited)
                    {
                        q += process.StandardOutput.ReadToEnd();
                    }
            


            }
            catch (Exception ex)
            {

                q += "error";

            }
            textBox1.Text = q;



        }

        private void addCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
