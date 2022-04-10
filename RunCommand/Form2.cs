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
    public partial class Form2 : Form
    {
        SQLiteConnection conn;
        SQLiteCommand cmd;
        SQLiteDataReader dr;
        string path = "deneme.db";
        string cs = @"URI=file:"+Application.StartupPath+"\\deneme.db";
        public string idyaz = "";

        public Form2()
        {
            InitializeComponent();
            data_show();
            textBox2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(con);

            cmd.CommandText = "INSERT INTO komut(yol) VALUES(@yol )";
            string yol = textBox1.Text;
         

            cmd.Parameters.AddWithValue("@yol", yol);
          

            //string[] row = new string[] { "" ,yol, dosya };
            //   dataGridView1.Rows.Add(row);

            cmd.ExecuteNonQuery();
            dataGridView1.Rows.Clear();
            data_show();

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
                    
                    dataGridView1.Rows.Insert(0, dr.GetValue(1).ToString() , dr.GetValue(0).ToString());

                }

                dr.Close();
            }
            catch (Exception ex)
            {
            
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[e.RowIndex];

                String task1 = dataGridViewRow.Cells["yol"].Value.ToString();

                textBox2.Text = task1;

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { 
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[e.RowIndex];

                String task1 = dataGridViewRow.Cells["yol"].Value.ToString();
               
                textBox2.Text = task1;
                idyaz =  dataGridViewRow.Cells["id"].Value.ToString();

            }
            }
            catch { }
            
    }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(con);

            try
            {
               
                cmd.CommandText = "DELETE FROM komut where id = @id ";
                cmd.Prepare();
               
                cmd.Parameters.AddWithValue("@id", idyaz);
                cmd.ExecuteNonQuery();
                dataGridView1.Rows.Clear();
                data_show();
                textBox2.Text  ="";
                idyaz = "";
            }
            catch(Exception exp) {
            textBox2.Text = exp.Message;
            }
        }
    }
}
