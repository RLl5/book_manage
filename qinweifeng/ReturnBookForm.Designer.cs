
namespace qinweifeng
{
    partial class ReturnBookForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridViewBorrow = new System.Windows.Forms.DataGridView();
            this.recordIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bookTitleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.borrowDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dueDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.viewBorrowRecordsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.booksys_DBDataSet1 = new qinweifeng.Booksys_DBDataSet1();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.booksys_DBDataSet = new qinweifeng.Booksys_DBDataSet();
            this.userBookViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.userBookViewTableAdapter = new qinweifeng.Booksys_DBDataSetTableAdapters.UserBookViewTableAdapter();
            this.view_BorrowRecordsTableAdapter = new qinweifeng.Booksys_DBDataSet1TableAdapters.View_BorrowRecordsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBorrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBorrowRecordsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.booksys_DBDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.booksys_DBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userBookViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewBorrow
            // 
            this.dataGridViewBorrow.AutoGenerateColumns = false;
            this.dataGridViewBorrow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBorrow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.recordIDDataGridViewTextBoxColumn,
            this.bookTitleDataGridViewTextBoxColumn,
            this.borrowDateDataGridViewTextBoxColumn,
            this.dueDateDataGridViewTextBoxColumn});
            this.dataGridViewBorrow.DataSource = this.viewBorrowRecordsBindingSource;
            this.dataGridViewBorrow.Location = new System.Drawing.Point(67, 50);
            this.dataGridViewBorrow.Name = "dataGridViewBorrow";
            this.dataGridViewBorrow.RowHeadersWidth = 51;
            this.dataGridViewBorrow.RowTemplate.Height = 27;
            this.dataGridViewBorrow.Size = new System.Drawing.Size(551, 150);
            this.dataGridViewBorrow.TabIndex = 0;
            // 
            // recordIDDataGridViewTextBoxColumn
            // 
            this.recordIDDataGridViewTextBoxColumn.DataPropertyName = "RecordID";
            this.recordIDDataGridViewTextBoxColumn.HeaderText = "RecordID";
            this.recordIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.recordIDDataGridViewTextBoxColumn.Name = "recordIDDataGridViewTextBoxColumn";
            this.recordIDDataGridViewTextBoxColumn.Width = 125;
            // 
            // bookTitleDataGridViewTextBoxColumn
            // 
            this.bookTitleDataGridViewTextBoxColumn.DataPropertyName = "BookTitle";
            this.bookTitleDataGridViewTextBoxColumn.HeaderText = "BookTitle";
            this.bookTitleDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.bookTitleDataGridViewTextBoxColumn.Name = "bookTitleDataGridViewTextBoxColumn";
            this.bookTitleDataGridViewTextBoxColumn.Width = 125;
            // 
            // borrowDateDataGridViewTextBoxColumn
            // 
            this.borrowDateDataGridViewTextBoxColumn.DataPropertyName = "BorrowDate";
            this.borrowDateDataGridViewTextBoxColumn.HeaderText = "BorrowDate";
            this.borrowDateDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.borrowDateDataGridViewTextBoxColumn.Name = "borrowDateDataGridViewTextBoxColumn";
            this.borrowDateDataGridViewTextBoxColumn.Width = 125;
            // 
            // dueDateDataGridViewTextBoxColumn
            // 
            this.dueDateDataGridViewTextBoxColumn.DataPropertyName = "DueDate";
            this.dueDateDataGridViewTextBoxColumn.HeaderText = "DueDate";
            this.dueDateDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dueDateDataGridViewTextBoxColumn.Name = "dueDateDataGridViewTextBoxColumn";
            this.dueDateDataGridViewTextBoxColumn.Width = 125;
            // 
            // viewBorrowRecordsBindingSource
            // 
            this.viewBorrowRecordsBindingSource.DataMember = "View_BorrowRecords";
            this.viewBorrowRecordsBindingSource.DataSource = this.booksys_DBDataSet1;
            // 
            // booksys_DBDataSet1
            // 
            this.booksys_DBDataSet1.DataSetName = "Booksys_DBDataSet1";
            this.booksys_DBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(67, 247);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(94, 23);
            this.btnReturn.TabIndex = 1;
            this.btnReturn.Text = "确认归还";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click_1);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(642, 247);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(92, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // booksys_DBDataSet
            // 
            this.booksys_DBDataSet.DataSetName = "Booksys_DBDataSet";
            this.booksys_DBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // userBookViewBindingSource
            // 
            this.userBookViewBindingSource.DataMember = "UserBookView";
            this.userBookViewBindingSource.DataSource = this.booksys_DBDataSet;
            // 
            // userBookViewTableAdapter
            // 
            this.userBookViewTableAdapter.ClearBeforeFill = true;
            // 
            // view_BorrowRecordsTableAdapter
            // 
            this.view_BorrowRecordsTableAdapter.ClearBeforeFill = true;
            // 
            // ReturnBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.dataGridViewBorrow);
            this.Name = "ReturnBookForm";
            this.Text = "ReturnBookForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBorrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBorrowRecordsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.booksys_DBDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.booksys_DBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userBookViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewBorrow;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnExit;
        private Booksys_DBDataSet booksys_DBDataSet;
        private System.Windows.Forms.BindingSource userBookViewBindingSource;
        private Booksys_DBDataSetTableAdapters.UserBookViewTableAdapter userBookViewTableAdapter;
        private Booksys_DBDataSet1 booksys_DBDataSet1;
        private System.Windows.Forms.BindingSource viewBorrowRecordsBindingSource;
        private Booksys_DBDataSet1TableAdapters.View_BorrowRecordsTableAdapter view_BorrowRecordsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn recordIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bookTitleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn borrowDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dueDateDataGridViewTextBoxColumn;
    }
}