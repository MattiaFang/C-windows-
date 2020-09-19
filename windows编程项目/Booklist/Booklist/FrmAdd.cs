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
    public partial class FrmAdd : Form
    {
        public FrmAdd()
        {
            InitializeComponent();
        }

        public DataGridView dgv;    //用来保存从主窗体传递过来的表格对象
        
        private void button1_Click(object sender, EventArgs e)
        {
            Book fi = new Book();                             //用于暂存输入的数据
            fi.BookId = int.Parse(textBox1.Text);             //取得书籍ID
            fi.BookName = textBox2.Text;                      //取得书籍名称
            fi.BookPrice = float.Parse(textBox3.Text);        //取得书籍价格
            fi.BookDate = textBox4.Text;                      //取得书籍日期
           
            //CheckBook(fi);
            //if (i == 0) return;
            SaveBook(fi);
            UpdateGrid(fi);

            //保存后初始化
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Focus();      //选择输入焦点
        }

        //void Cheak(Book fi)
        //{
        //    SqlConnection conn = new SqlConnection(DB.ConnString);
        //    conn.Open();
        //    string sql = "Select Into Book Where";
        //}

        void SaveBook(Book fi)
        {
            //保存到数据库
            SqlConnection conn = new SqlConnection(DB.ConnString);
            conn.Open();
            string sql = "Insert Into Book(BookId,BookName,BookPrice,BookDate)";
            sql += "values(" + fi.BookId + ",N'" + fi.BookName + "'," + fi.BookPrice + ",N'" + fi.BookDate + "')";   //SQL添加语句

            SqlCommand comm = new SqlCommand(sql, conn);
            comm.ExecuteNonQuery();
            conn.Close();
        }

        void UpdateGrid(Book fi)
        {
            //更新表格
            int count = dgv.Rows.Count;                //当前表格行数
            dgv.Rows.Add();
            dgv.Rows[count].Cells["BookId"].Value = fi.BookId;
            dgv.Rows[count].Cells["BookName"].Value = fi.BookName;
            dgv.Rows[count].Cells["BookPrice"].Value = fi.BookPrice;
            dgv.Rows[count].Cells["BookDate"].Value = fi.BookDate;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
