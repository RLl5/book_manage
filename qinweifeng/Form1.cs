using System;
using System.Data;
using System.Windows.Forms;


namespace qinweifeng
{
    public partial class BorrowBookForm : Form
    {
        private int currentUserID; // 登录用户ID（从主窗体传入）

        // 构造函数：接收当前用户ID
        public BorrowBookForm(int userID)
        {
            try
            {
                InitializeComponent();
                currentUserID = userID;
                LoadAvailableBooks(); // 加载可借图书
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化借阅窗体失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); // 关闭窗体
            }
        }

        // 优化后的加载可借图书方法
        private void LoadAvailableBooks()
        {
            try
            {
                // 使用参数化查询防止SQL注入
               string sql = "SELECT BookID, Title FROM BookTable WHERE TotalCopies > 0";

                // 获取数据
                DataTable dt = DbHelperSQL.GetTableFromSql(sql);

                // 检查是否有可借图书
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("当前没有可借阅的图书", "提示",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // 如果没有可借图书，关闭窗口
                    return;
                }

                // 绑定到 ComboBox
                comboBoxBook.DataSource = dt;
                comboBoxBook.DisplayMember = "Title"; // 显示书名
                comboBoxBook.ValueMember = "BookID";   // 实际值是 BookID

                // 设置默认选择项
                comboBoxBook.SelectedIndex = 0;
            }
            catch (Exception )
            {
                // 记录错误日志（生产环境中）
                // Logger.LogError("加载图书失败: ");

                // 用户友好提示
                MessageBox.Show("加载图书信息失败，请稍后重试", "系统错误",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        // 确认借阅按钮点击事件
        private void btnBorrow_Click_1(object sender, EventArgs e)
        {
            if (comboBoxBook.SelectedValue == null)
            {
                MessageBox.Show("请选择要借阅的图书！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookID = (int)comboBoxBook.SelectedValue;
            bool isSuccess = DbHelperSQL.BorrowBook(currentUserID, bookID);

            if (isSuccess)
            {
                MessageBox.Show("借阅成功！应还日期可在「还书界面」中查看", "成功",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // 关闭当前窗体
            }
        }

        /*private void BorrowBookForm_Load(object sender, EventArgs e)
        {
            if (comboBoxBook.SelectedValue == null)
            {
                MessageBox.Show("请选择要借阅的图书！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 获取选中的图书ID
            int bookID = (int)comboBoxBook.SelectedValue;

            try
            {
                // 调用数据库方法插入借阅记录
                // 假设你有 BorrowTableAdapter，方法为 InsertBorrow

                MessageBox.Show("借阅成功！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // 关闭窗体
            }
            catch (Exception ex)
            {
                MessageBox.Show($"借阅失败：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/
    

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}