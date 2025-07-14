using System;

namespace cyl
{
    // 实体类：对应数据库 BorrowRecord 表 + 关联的用户、图书信息
    public class BorrowModel
    {
        // 1. 借阅记录自身字段（对应 BorrowRecord 表）
        public int RecordID { get; set; }    // 借阅记录ID（主键）
        public int UserID { get; set; }      // 借阅人ID（关联 UserTable）
        public int BookID { get; set; }      // 图书ID（关联 BookTable）
        public DateTime BorrowDate { get; set; } // 借阅日期
        public DateTime? ReturnDate { get; set; } // 归还日期（可空，未归还时为 null）
        public DateTime DueDate { get; set; }   // 应还日期
        public string Status { get; set; }     // 状态：借阅中/已归还/逾期

        // 2. 关联的用户信息（从 UserTable 关联查询）
        public string UserName { get; set; }   // 借阅人姓名
        public string UserType { get; set; }   // 借阅人类型（学生/教师等）

        // 3. 关联的图书信息（从 BookTable 关联查询）
        public string BookTitle { get; set; }  // 图书名称
        public string Author { get; set; }     // 图书作者
        public string Genre { get; set; }      // 图书分类

        // 4. 扩展属性（可选，比如计算逾期天数）
        public int OverdueDays
        {
            get
            {
                // 如果已归还，逾期天数为 0；未归还且超期，计算天数
                if (ReturnDate != null) return 0;
                TimeSpan span = DateTime.Now - DueDate;
                return span.TotalDays > 0 ? (int)span.TotalDays : 0;
            }
        }
    }
}