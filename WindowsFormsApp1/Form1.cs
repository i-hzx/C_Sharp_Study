using System;
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
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            String path = openFileDialog1.FileName;
            textBox1.Text = path;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            String SqlStr = "Server=(local);User Id=hzx;Pwd=hzx;DataBase=hzx";
            SqlConnection sqlConnection = new SqlConnection(SqlStr);
            sqlConnection.Open();
            if(sqlConnection.State==ConnectionState.Open)
            {
                MessageBox.Show("连接成功！","提示");
            }
            else
            {
                MessageBox.Show("连接失败！","提示");
            }
        }
    }
}
