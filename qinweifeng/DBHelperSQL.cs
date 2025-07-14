using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;
using System.Configuration;
using System.Windows.Forms;



namespace qinweifeng
{
    public class DbHelperSQL : MarshalByRefObject
    {


        public static string connectionString = "server=.;database=Booksys_DB;Integrated Security=True;";
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        /// 
        public static int ExecuteSql(string SQLString)
        {


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }//弹出报错
                }
            }
        }

        public static int ExecuteSql2(string SQLString, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(SQLString, connection))
            {
                cmd.Parameters.AddRange(parameters);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    connection.Close();
                    throw e;
                }//弹出报错
            }
        }


        //执行无参数的sql语句，执行结果放入返回的DateTable
        public static DataTable GetTableFromSql(string sql, params SqlParameter[] parameters)
        {
            //SqlParameter[] parameter = new SqlParameter[] { };
            return ExecuteDataTable(sql, parameters);
        }
        public static DataTable ExecuteDataTable(string cmdText,
             params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();

                return ExecuteDataTable(conn, cmdText, parameters);
            }
        }

        public static DataTable ExecuteDataTable(SqlConnection conn, string cmdText,
                         params SqlParameter[] parameters)
        {

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = cmdText;
                cmd.Parameters.AddRange(parameters);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    // 关键：拼接 SQL，把参数替换成实际值
                    string sqlWithValues = cmd.CommandText;
                    foreach (SqlParameter param in cmd.Parameters)
                    {
                        sqlWithValues = sqlWithValues.Replace(
                            param.ParameterName,
                            $"'{param.Value}'"  // 加单引号，适配字符串参数
                        );
                    }
                    //MessageBox.Show($"执行的 SQL：\n{sqlWithValues}", "调试"); // 弹出看实际执行的 SQL

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }

        }

        // -------------------- 借阅相关方法 --------------------
        /// 执行借阅操作
        
        public static bool BorrowBook(int userID, int bookID, int dueDateDays = 30)
        {
            try
            {
                string sql = @"
                    INSERT INTO BorrowRecord (UserID, BookID, BorrowDate, DueDate, Status)
                    VALUES (@UserID, @BookID, GETDATE(), DATEADD(DAY, @DueDateDays, GETDATE()), '借阅中');

                    UPDATE BookTable 
                    SET TotalCopies = TotalCopies - 1 
                    WHERE BookID = @BookID;   
                ";

                SqlParameter[] param = {
                    new SqlParameter("@UserID", userID),
                    new SqlParameter("@BookID", bookID),
                    new SqlParameter("@DueDateDays", dueDateDays)
                };

                

                int rows = ExecuteSql2(sql, param);
                return rows > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("借阅失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

       
        // 执行还书操作
        public static bool ReturnBook(int recordID)
        {
            try
            {
                string sql = @"
                    UPDATE BorrowRecord 
                    SET ReturnDate = GETDATE(),
                        Status = CASE 
                                    WHEN GETDATE() > DueDate THEN '逾期' 
                                    ELSE '已归还' 
                                  END
                    WHERE RecordID = @RecordID;

                    UPDATE BookTable 
                    SET TotalCopies = TotalCopies + 1 
                    WHERE BookID = (SELECT BookID FROM BorrowRecord WHERE RecordID = @RecordID);
                ";

                SqlParameter[] param = {
                    new SqlParameter("@RecordID", recordID)
                };

                int rows = ExecuteSql2(sql, param);
                if (rows == 1)
                {
                    MessageBox.Show("还书成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //MessageBox.Show("还书成功", MessageBoxButtons.OK,
                    //MessageBoxIcon.Warning,)

                }
                MessageBox.Show(rows.ToString());
                
                return rows > 0;
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("还书失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

     
        /// 查询用户的借阅记录
        public static DataTable GetBorrowRecordsByUser(int userID)
        {
            string sql = @"
                SELECT br.RecordID, br.UserID, br.BookID, br.BorrowDate, 
                br.ReturnDate, br.DueDate, br.Status,
                u.UserName, 
                b.Title AS BookTitle
                FROM BorrowRecord br
                INNER JOIN UserTable u ON br.UserID = u.UserID
                INNER JOIN BookTable b ON br.BookID = b.BookID
                WHERE br.UserID = @UserID and br.ReturnDate IS NULL;
            ";

            SqlParameter[] param = {
                new SqlParameter("@UserID", userID)
            };

            return GetTableFromSql(sql, param);
        }
    }
}
    
