using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Booklist
{
    public partial class FrmUpdate : Form
    {
        public FrmUpdate()
        {
            InitializeComponent();
        }

        public DataGridView dgv;
        public Book fi;

        private void FrmUpdate_Load(object sender, EventArgs e)
        {
            textBox1.Text = fi.BookName;
            textBox2.Text = fi.BookPrice.ToString();
            textBox3.Text = fi.BookDate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fi.BookName = textBox1.Text;
            fi.BookPrice = float.Parse(textBox2.Text);
            fi.BookDate = textBox3.Text;
            SaveFoodItem(fi);
            UpdateGrid(fi);
            Close();
        }

        void SaveFoodItem(Book fi)
        {
            //保存到数据库
            SqlConnection conn = new SqlConnection(DB.ConnString);
            conn.Open();
            string sql = "Update Book Set BookName = N'" + fi.BookName + "',BookPrice=" + fi.BookPrice;
            sql += "Where BookId = " + fi.BookId;                                                       //SQL更新语句

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.ExecuteNonQuery();
            conn.Close();
        }

        void UpdateGrid(Book fi)
        {
            //更新表格
            dgv.CurrentRow.Cells["BookName"].Value = fi.BookName;
            dgv.CurrentRow.Cells["BookPrice"].Value = fi.BookPrice;
            dgv.CurrentRow.Cells["BookDate"].Value = fi.BookDate;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
