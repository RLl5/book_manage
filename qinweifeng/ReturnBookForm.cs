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
    public partial class ReturnBookForm : Form
    {
        private int currentUserID; // 登录用户ID（从主窗体传入）

        public ReturnBookForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
            LoadBorrowedBooks(); // 加载未归还的借阅记录
        }

        // 加载用户未归还的借阅记录（Status = '借阅中'）
        private void LoadBorrowedBooks()
        {
            // 直接重新查询数据，旧数据会被新的 DataTable 替换
            DataTable dt = DbHelperSQL.GetBorrowRecordsByUser(currentUserID);
            
            dataGridViewBorrow.DataSource = dt;


            // 隐藏不必要的列（只显示关键信息）
            if (dataGridViewBorrow.Columns.Contains("UserID"))
                dataGridViewBorrow.Columns["UserID"].Visible = false;
            if (dataGridViewBorrow.Columns.Contains("BookID"))
                dataGridViewBorrow.Columns["BookID"].Visible = false;
            if (dataGridViewBorrow.Columns.Contains("Status"))
                dataGridViewBorrow.Columns["Status"].Visible = false;
        }

        // 确认归还按钮点击事件
        private void btnReturn_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewBorrow.Rows.Count == 0)
            {
                return;
            }
            if (dataGridViewBorrow.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要归还的记录！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            int columnIndex = 0; // 假设RecordID是第一列
            int recordID = Convert.ToInt32(dataGridViewBorrow.SelectedRows[0].Cells[columnIndex].Value);

            bool isSuccess = DbHelperSQL.ReturnBook(recordID);

            if (isSuccess)
            {
                MessageBox.Show("还书成功！", "成功",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBorrowedBooks(); // 刷新列表
            }
            
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /*private void ReturnBookForm_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“booksys_DBDataSet1.View_BorrowRecords”中。您可以根据需要移动或删除它。
            this.view_BorrowRecordsTableAdapter.Fill(this.booksys_DBDataSet1.View_BorrowRecords);
            // TODO: 这行代码将数据加载到表“booksys_DBDataSet.UserBookView”中。您可以根据需要移动或删除它。
            this.userBookViewTableAdapter.Fill(this.booksys_DBDataSet.UserBookView);

        }*/
    }
}