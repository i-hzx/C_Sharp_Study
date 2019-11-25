using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public String [] headerText = { "编号","姓名","邮箱","内容"};
        public Hashtable map = new Hashtable();
        public DataSet dataSet = new DataSet();
        public String SqlStr = "Server=(local);User Id=hzx;Pwd=hzx;DataBase=hzx";
        public SqlConnection sqlConnection;
        public Form1()
        {
            InitializeComponent();
        }
        public void Table_set()
        {
            foreach(DataGridViewRow dataGridViewRow in dataGridView1.Rows)
            {
                if (dataGridViewRow.Index% 2 != 0)
                {
                    dataGridView1.Rows[dataGridViewRow.Index].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                }
            }
            for (int i = 0; i < headerText.Length; i++)
            {
                dataGridView1.Columns[i].HeaderText = headerText[i];
                dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            String path = openFileDialog1.FileName;
            textBox1.Text = path;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            
            sqlConnection = new SqlConnection(SqlStr);
            sqlConnection.Open();
            if(sqlConnection.State==ConnectionState.Open)
            {
                //MessageBox.Show("连接成功！","提示");
                
                SqlCommand sqlCommand = new SqlCommand("select * from Guests",sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataSet.Clear();
                sqlDataAdapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
                Table_set();
            }
            else
            {
                MessageBox.Show("连接失败！","提示");
            }
            sqlConnection.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            foreach (Control control in this.panel3.Controls)
            {
                
                if(control is TextBox)
                {
                    map.Add(control.Name, (TextBox)control);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String str_read = "";
            Model.Guest guest =null;
            List<Model.Guest> list = new List<Model.Guest>();
            sqlConnection = new SqlConnection(SqlStr);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "select * from Guests";
            sqlCommand.CommandType = CommandType.Text;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            textBox1.Text=sqlDataReader.HasRows.ToString();
            while(sqlDataReader.Read())
            {
                guest = new Model.Guest();
                guest.Id = Convert.ToInt32(sqlDataReader[0]);
                guest.Name = sqlDataReader[1].ToString();
                guest.Email = sqlDataReader[2].ToString();
                guest.Content = sqlDataReader[3].ToString();
                list.Add(guest);
               //MessageBox.Show(sqlDataReader[2].ToString());
            }
            dataGridView1.DataSource = list;
            Table_set();
            //MessageBox.Show(str_read);
            sqlConnection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(SqlStr);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType= CommandType.Text;
            sqlCommand.CommandText = "Insert into Guests values(@textBox2,@textBox3,@textBox4,@textBox5)";
            sqlCommand.Parameters.Add("@textBox2",SqlDbType.Int,255).Value = textBox2.Text; 
            sqlCommand.Parameters.Add(new SqlParameter("@textBox3", SqlDbType.NVarChar, 255));
            sqlCommand.Parameters.Add(new SqlParameter("@textBox4", SqlDbType.NVarChar, 255)); 
            sqlCommand.Parameters.Add(new SqlParameter("@textBox5", SqlDbType.NVarChar, 255));
            //sqlCommand.Parameters["@textBox2"].Value = textBox2.Text;
            sqlCommand.Parameters["@textBox3"].Value = textBox3.Text;
            sqlCommand.Parameters["@textBox4"].Value = textBox4.Text;
            sqlCommand.Parameters["@textBox5"].Value = textBox5.Text;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
            sqlConnection.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
             {
                TextBox t = (TextBox)map["textBox"+(i+2)];             
                t.Text= dataGridView1.SelectedCells[i].Value.ToString();
             }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(SqlStr);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "delete from Guests where Id=@id";
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.Add("@id",SqlDbType.Int,255);
            sqlCommand.Parameters["@id"].Value = Convert.ToInt32(dataGridView1.SelectedCells[0].Value);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
            //dataGridView1.DataSource = dataSet.Tables[0];
        }

       
    }
}
