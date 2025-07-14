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
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }
        private string selectedGender;
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select * from UserTable where PhoneNumber = '" + textBox3.Text + "'";
            DataTable table1 = DbHelperSQL.GetTableFromSql(sql);
            if (table1.Rows.Count > 0)
            {
                MessageBox.Show("该用户已存在，请更换手机号。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(selectedGender))
            {
                MessageBox.Show("请选择性别！", "注册失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            var sql1 = "INSERT INTO UserTable (UserName, UserType, PhoneNumber, Password, Gender) VALUES('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "','" + textBox4.Text + "','"+ selectedGender + "')";
            var num = DbHelperSQL.ExecuteSql(sql1);
            if (num == 1)
            {
                MessageBox.Show("注册成功");
                this.Close();
            }
            else
            {
                MessageBox.Show("注册失败", "注册失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // 当CheckBox1选中时
            if (checkBox1.Checked)
            {
                // 如果CheckBox2也选中，则报错
                if (checkBox2.Checked)
                {
                    MessageBox.Show("不能同时选择男和女。请选择一个性别。");
                    checkBox1.Checked = false; // 取消CheckBox1的选中状态
                }
                else
                {
                    // 如果CheckBox2未选中，表示选择了男
                    selectedGender = "男";
                }
            }
            else
            {
                // 当CheckBox1取消选中时，设置selectedGender为空
                selectedGender = null;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // 当CheckBox2选中时
            if (checkBox2.Checked)
            {
                // 如果CheckBox1也选中，则报错
                if (checkBox1.Checked)
                {
                    MessageBox.Show("不能同时选择男和女。请选择一个性别。");
                    checkBox2.Checked = false; // 取消CheckBox2的选中状态
                }
                else
                {
                    // 如果CheckBox1未选中，表示选择了女
                    selectedGender = "女";
                }
            }
            else
            {
                // 当CheckBox2取消选中时，设置selectedGender为空
                selectedGender = null;
            }
        }

        private void AddUser_Load(object sender, EventArgs e)
        {

        }
    }
}
