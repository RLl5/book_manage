using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static qinweifeng.Login;

namespace qinweifeng
{

    public partial class Main : Form
    {
        private int currentUserID; // 登录用户ID
        public Main()
        {
            
            InitializeComponent();
            // 在构造函数或InitializeComponent()方法中确认存在以下注册
            this.FormClosing += Main_FormClosing;

        }
        public Main(int userId = 0)
        {
            InitializeComponent();
            currentUserID = userId;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
            Finish();

        }

        private void 添加图书ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddOrEditBook AddBook = new AddOrEditBook("", currentUserID);
            AddBook.ShowDialog();
            Finish();
        }

        private void 修改图书ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string selectedBookID = dataGridView1.SelectedRows[0].Cells["bookIDDataGridViewTextBoxColumn"].Value.ToString();

                // 弹出确认删除的对话框
                DialogResult result = MessageBox.Show("确定修改选中的图书吗？", "确认修改", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    AddOrEditBook editBook = new AddOrEditBook(selectedBookID, currentUserID);
                    editBook.ShowDialog();
                    Finish();
                }
            }
            else
            {
                MessageBox.Show("请先选中要修改的图书", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void 删除图书ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count ==1)
            {
                if (DbHelperSQL.GetTableFromSql($"select * from [UserTable] where UserType = '管理员' and UserID = '{currentUserID}'").Rows.Count == 0)
                {
                    MessageBox.Show("失败,只允许管理员操作", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // 获取选中的行的 Book_ID 列的值（假设 Book_ID 是列名）
                int selectedBookID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["bookIDDataGridViewTextBoxColumn"].Value);

                // 弹出确认删除的对话框
                DialogResult result = MessageBox.Show("确认删除选中的图书吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string delsql = "delete BookTable where BookID = '" + selectedBookID + "'";
                    int del = DbHelperSQL.ExecuteSql(delsql);
                    if (del == 1)
                    {
                        MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Finish();
                    }
                    else
                    {
                        MessageBox.Show("删除失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选中要删除的图书", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strTitle = "", strAuthor = "", strGenre = "";
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                strTitle = " AND Book_Title LIKE '%" + textBox1.Text + "%'";
            }
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                strAuthor = " AND Book_Author LIKE '%" + textBox2.Text + "%'";
            }
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                strGenre = " AND Book_Genre LIKE '%" + textBox3.Text + "%'";
            }

            var sql = "SELECT * FROM UserBookView Book_Title  WHERE 1=1 " + strTitle + strAuthor + strGenre;
            

            var data = DbHelperSQL.GetTableFromSql(sql);
            // 先清空原有数据，避免重复叠加
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = data;

            // 检查是否已存在同名列
            if (dataGridView1.Columns["Book_Image"] == null)
            {
                // 创建一个新列用于存储 Image 对象
                DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
                imageColumn.Name = "Book_Image";
                imageColumn.HeaderText = "图书封面";
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // 设置图片布局

                // 添加新列到 DataGridView 中
                dataGridView1.Columns.Add(imageColumn);
            }

            // 数据绑定后，再处理图片列转换（因为绑定后才有行数据）
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // 注意判断单元格值是否为 DBNull.Value ，避免转换异常
                if (row.Cells["bookImagePathDataGridViewTextBoxColumn"].Value != DBNull.Value &&
                    !string.IsNullOrEmpty(row.Cells["bookImagePathDataGridViewTextBoxColumn"].Value.ToString()))
                {
                    try
                    {
                        Image image = Image.FromFile(row.Cells["bookImagePathDataGridViewTextBoxColumn"].Value.ToString());
                        row.Cells["Book_Image"].Value = image;
                    }
                    catch (Exception ex)
                    {
                        // 处理图片加载失败异常，比如路径错误等
                        MessageBox.Show($"加载图片失败：{ex.Message}");
                    }
                }
            }
            // 隐藏原始的字符串路径列
            dataGridView1.Columns["bookImagePathDataGridViewTextBoxColumn"].Visible = false;

            // 刷新 DataGridView
            dataGridView1.Refresh();


        }


            
        private void Finish()
        {
            var sql = "SELECT * FROM UserBookView";
            var data = DbHelperSQL.GetTableFromSql(sql);
            dataGridView1.DataSource = data;

            // 检查是否已存在同名列
            if (dataGridView1.Columns["Book_Image"] == null)
            {
                // 创建一个新列用于存储 Image 对象
                DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
                imageColumn.Name = "Book_Image";
                imageColumn.HeaderText = "图书封面";
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // 设置图片布局

                // 添加新列到 DataGridView 中
                dataGridView1.Columns.Add(imageColumn);
            }

            // 将 ImagePath 列的数据转换为 Image 对象，以便在 DataGridView 中显示图片
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!string.IsNullOrEmpty(row.Cells["bookImagePathDataGridViewTextBoxColumn"].Value.ToString()))
                {
                    Image image = Image.FromFile(row.Cells["bookImagePathDataGridViewTextBoxColumn"].Value.ToString());
                    row.Cells["Book_Image"].Value = image;
                }
            }

            // 隐藏原始的字符串路径列
            dataGridView1.Columns["bookImagePathDataGridViewTextBoxColumn"].Visible = false;

            // 刷新 DataGridView
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            // 打开借书窗体，传入当前用户ID
            BorrowBookForm borrowForm = new BorrowBookForm(currentUserID);
            borrowForm.ShowDialog(); // 模态对话框，阻塞当前窗体
            Finish(); // （可选）刷新主窗体数据
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            ReturnBookForm returnForm = new ReturnBookForm(currentUserID);
            returnForm.ShowDialog();
            Finish();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 判断是否管理员身份
            if (GlobalData.isAdmin)
            {
                AddUser addUser = new AddUser();
                addUser.ShowDialog();
            }
            else
            {
                MessageBox.Show("仅管理员可进行注册操作！", "权限提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
    

