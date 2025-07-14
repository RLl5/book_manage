using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
namespace qinweifeng

{
    
    public partial class Login : Form
    {
        public static class UserInfo { public static string UserType { get; set; }public static string UserName { get; set; } }
        public Login()
        {
            InitializeComponent();
            this.FormClosing +=
                
                
             
                
                
                Login_FormClosing;
        }

       public static int currentUserID = 0;
        public static class Global
        {
            public static int currentUserID = 0;
            // 其他全局变量（如 isAdmin 等）也可以放这里
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string phone = LoginCode.Text.Trim();
            string pwd = Password.Text.Trim();

            string sql = @"
SELECT UserID, UserType, PhoneNumber 
FROM UserTable 
WHERE PhoneNumber = @Phone AND Password = @Pwd";//参数占位符

            SqlParameter[] paras = {
        new SqlParameter("@Phone", phone),//赋值
        new SqlParameter("@Pwd", pwd)
    };

            DataTable dt = DbHelperSQL.ExecuteDataTable(sql, paras);

            if (dt != null && dt.Rows.Count > 0)
            {
                int userID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                Global.currentUserID = userID;
                 //【关键调试】打印所有查询结果，确认 UserID 实际值
                //MessageBox.Show($"数据库查询结果：\nUserID: {dt.Rows[0]["UserID"]}\nUserType: {dt.Rows[0]["UserType"]}", "调试信息");

                //【强制校验】确保 UserID 是有效数值
               if (dt.Rows[0]["UserID"] is DBNull)
               {
                   MessageBox.Show("数据库中 UserID 为 null！检查表结构", "错误");
                   return;
                }

               
               if (!int.TryParse(dt.Rows[0]["UserID"].ToString(), out userID) || userID == 0)
               {
                   MessageBox.Show($"UserID 无效：{dt.Rows[0]["UserID"]}！检查数据库数据", "错误");
                    return;
                }

                // 赋值给全局变量
                currentUserID = userID;

                // 原有逻辑
                string userType = dt.Rows[0]["UserType"].ToString();
                UserInfo.UserType = userType;
                GlobalData.isAdmin = (userType == "管理员");

               // MessageBox.Show($"当前用户类型：{userType}", "登录验证");

                Main main = new Main(userID);
                this.Hide();
                main.Show();
            }
            else
            {
                MessageBox.Show("手机号或密码错误！", "登录失败");
            }
        }
        /*private void btnLogin_Click(object sender, EventArgs e)
        {
            string phone = Password.Text.Trim(); // 手机号输入框
            string pwd = Password.Text.Trim();    // 密码输入框
            string sql = "SELECT UserID, UserType FROM UserTable WHERE Phone = @Phone AND Password = @Pwd";
            SqlParameter[] paras = {
             new SqlParameter("@Phone", phone),
             new SqlParameter("@Pwd", pwd)
};
            DataTable dt = DbHelperSQL.GetTableFromSql(sql, paras);
            if (dt != null && dt.Rows.Count > 0)
            {
               
                UserInfo.UserName = phone; // 或存真实用户名，看表设计
                this.Hide();
                Main main = new Main();
                main.Show();
            }
            else
            {
                MessageBox.Show("账号或密码错误！", "登录失败",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */
        // 1. 先定义全局标记（示例，实际可放登录模块或统一工具类）
        public static class GlobalData
        {
            public static bool isAdmin = false;
        }

        // 2. 注册按钮点击事件：判断身份后决定是否打开注册窗口
        private void button2_Click(object sender, EventArgs e)
        {
            // 核心逻辑：仅管理员可触发注册
            if (GlobalData.isAdmin)
            {
                AddUser addUser = new AddUser();
                addUser.ShowDialog();
            }
            else
            {
                MessageBox.Show("仅管理员可注册新用户，请联系管理员操作！", "权限提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoginCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
