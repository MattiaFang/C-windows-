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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        void GetBook()
        {
            SqlConnection conn = new SqlConnection(DB.ConnString);
            conn.Open();

            string sql = "Select * From Book";
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                Book fi = new Book();              //保存行数据的对象fi
                fi.BookId = int.Parse(dr["BookId"].ToString());
                fi.BookName = dr["BookName"].ToString();
                fi.BookPrice = float.Parse(dr["BookPrice"].ToString());
                fi.BookDate = dr["BookDate"].ToString();
                FillGrid(fi);                          //作为行数据填充表格控件
            }
            dr.Close();
            conn.Close();
        }

        void FillGrid(Book fi)
        {
            int count = dgv.Rows.Count;   //取得当前行数
            dgv.Rows.Add();               //添加空行
            //填充空行单元格，Value为object类型，可以赋值任何类型值
            dgv.Rows[count].Cells["BookId"].Value = fi.BookId;
            dgv.Rows[count].Cells["BookName"].Value = fi.BookName;
            dgv.Rows[count].Cells["BookPrice"].Value = fi.BookPrice;
            dgv.Rows[count].Cells["BookDate"].Value = fi.BookDate;
        }

        private void btshow_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();           //初始化表格控件
            GetBook();                  //自定义方法，读取表数据并显示到界面
        }

        private void btShow_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();       //初始化表格控件
            GetBook();                //自定义方法，读取表数据
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            FrmAdd frm = new FrmAdd();          //创建窗体对象
            frm.dgv = this.dgv;                 //传递表格对象
            frm.ShowDialog();                   //打开窗体
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
           
            if (dgv.CurrentRow == null) 
                return;                   //如果没有选中行，终止执行
            FrmUpdate frm = new FrmUpdate();
            frm.dgv = this.dgv;              //传递表格对象
            Book fi = new Book();            //创建保存当前行数据的对象

            //由于单元格属性是object类型，需要转换类型后保存
            fi.BookName = dgv.CurrentRow.Cells["BookName"].Value.ToString();
            fi.BookPrice = float.Parse(dgv.CurrentRow.Cells["BookPrice"].Value.ToString());
            fi.BookDate = dgv.CurrentRow.Cells["BookDate"].Value.ToString();
            frm.fi = fi;                    //传递行数据
            frm.ShowDialog();
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null) return;     //如果没有选中行，终止执行
            int BookId = int.Parse(dgv.CurrentRow.Cells["BookId"].Value.ToString());

            //根据选择的BookID，从数据库中删除该条记录
            SqlConnection conn = new SqlConnection(DB.ConnString);
            conn.Open();
            string sql = "Delete From Book Where BookId=" + BookId;    //SQL删除语句
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.ExecuteNonQuery();
            conn.Close();

            dgv.Rows.Remove(dgv.CurrentRow);         //删除选中的行
        }

        private void btTJ_Click(object sender, EventArgs e)
        {
            double sum = 0;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                sum += Convert.ToDouble(dgv.Rows[i].Cells[2].Value);
            }
            string s1 = "当前共有书籍为 "+ dgv.Rows.Count +"本\n";
            string s2 = "总价值为 " + sum +"元\n";
            string s3 = "请珍惜阅读的机会，加油！";
            MessageBox.Show(s1+s2+s3,"统计结果");
        }


    }
}
