using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qinweifeng
{
    public partial class AddOrEditBook : Form
    {

        string selectedImagePath;
        string bookid;
        int Type1 = 0;
        public AddOrEditBook(string id,int type)
        {
            Type1 = type;
            InitializeComponent();
            bookid = id;
            if(!string.IsNullOrEmpty(id))
            {
                button1.Text = "修改";
            }
        }

        private void AddOrEditBook_Load(object sender, EventArgs e)
        {
            string usersql = "SELECT UserID, UserName FROM UserTable";
            DataTable userdt = DbHelperSQL.GetTableFromSql(usersql);

            // 将用户数据绑定到comboBox1
            comboBox1.DataSource = userdt;
            comboBox1.DisplayMember = "UserName"; // 显示的列名
            comboBox1.ValueMember = "UserID"; // 对应的值列名

            if (!string.IsNullOrEmpty(bookid))
            {
                string booksql = "select * from BookTable where BookID='"+bookid+"'";
                DataTable bookdt = DbHelperSQL.GetTableFromSql(booksql);
                textBox1.Text = bookdt.Rows[0][1].ToString();
                textBox2.Text = bookdt.Rows[0][2].ToString();
                textBox3.Text = bookdt.Rows[0][3].ToString();
                textBox4.Text = bookdt.Rows[0][4].ToString();
                textBox5.Text = bookdt.Rows[0][5].ToString();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // 设置文件类型过滤器，只显示图片文件
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取所选文件的路径
                selectedImagePath=openFileDialog.FileName;
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(DbHelperSQL.GetTableFromSql($"select * from [UserTable] where UserType = '管理员' and UserID = '{Type1}'").Rows.Count == 0)
            {
                MessageBox.Show("失败,只允许管理员操作", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(button1.Text == "新增")
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
    string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) ||
    string.IsNullOrWhiteSpace(textBox5.Text) || 
    comboBox1.SelectedValue == null || string.IsNullOrWhiteSpace(comboBox1.SelectedValue.ToString()))
                {
                    MessageBox.Show("请填写所有必填字段", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                var sql = $"INSERT INTO BookTable (Title, Author, Genre, ISBN, TotalCopies, UserID) VALUES ('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}', '{textBox5.Text}','{comboBox1.SelectedValue}')";
                var num = DbHelperSQL.ExecuteSql(sql);
                if (num == 1)
                {
                    MessageBox.Show("新增成功");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("新增失败", "新增失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if(button1.Text == "修改")
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
    string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) ||
    string.IsNullOrWhiteSpace(textBox5.Text) ||
    comboBox1.SelectedValue == null || string.IsNullOrWhiteSpace(comboBox1.SelectedValue.ToString()))
                {
                    MessageBox.Show("请填写所有必填字段", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                var sql = $"UPDATE BookTable SET Title = '{textBox1.Text}', Author = '{textBox2.Text}', Genre = '{textBox3.Text}', ISBN = '{textBox4.Text}', TotalCopies = '{textBox5.Text}', UserID = '{comboBox1.SelectedValue}' WHERE BookID = '{bookid}'";

                var num = DbHelperSQL.ExecuteSql(sql);

                if (num == 1)
                {
                    MessageBox.Show("更新成功");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("更新失败", "更新失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
