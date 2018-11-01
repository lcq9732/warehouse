namespace Warehouse
{
    partial class FrmRepairedBillList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnNew = new System.Windows.Forms.Button();
            this.gridRepairedBill = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Del = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridRepairedBill)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(10, 10);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "新建";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Visible = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // gridRepairedBill
            // 
            this.gridRepairedBill.AllowUserToAddRows = false;
            this.gridRepairedBill.AllowUserToDeleteRows = false;
            this.gridRepairedBill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRepairedBill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.BillDate,
            this.btn,
            this.Del});
            this.gridRepairedBill.Location = new System.Drawing.Point(10, 45);
            this.gridRepairedBill.Name = "gridRepairedBill";
            this.gridRepairedBill.ReadOnly = true;
            this.gridRepairedBill.RowTemplate.Height = 23;
            this.gridRepairedBill.Size = new System.Drawing.Size(460, 605);
            this.gridRepairedBill.TabIndex = 1;
            this.gridRepairedBill.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRepariedBill_CellContentClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // BillDate
            // 
            this.BillDate.DataPropertyName = "BillDate";
            dataGridViewCellStyle1.Format = "D";
            dataGridViewCellStyle1.NullValue = null;
            this.BillDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.BillDate.HeaderText = "日期";
            this.BillDate.Name = "BillDate";
            this.BillDate.ReadOnly = true;
            // 
            // btn
            // 
            this.btn.HeaderText = "查看";
            this.btn.Name = "btn";
            this.btn.ReadOnly = true;
            this.btn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btn.Text = "查看";
            this.btn.UseColumnTextForLinkValue = true;
            // 
            // Del
            // 
            this.Del.HeaderText = "删除";
            this.Del.Name = "Del";
            this.Del.ReadOnly = true;
            this.Del.Text = "删除";
            this.Del.UseColumnTextForLinkValue = true;
            // 
            // FrmRepairedBillList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(484, 661);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.gridRepairedBill);
            this.Name = "FrmRepairedBillList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "维修单列表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.FrmRepairedBillList_Activated);
            this.Load += new System.EventHandler(this.FrmRepairedBillList_Load);
            this.Resize += new System.EventHandler(this.FrmRepairedBillList_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.gridRepairedBill)).EndInit();
            this.ResumeLayout(false);

        }       

        #endregion

        private System.Windows.Forms.DataGridView gridRepairedBill;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDate;
        private System.Windows.Forms.DataGridViewLinkColumn btn;
        private System.Windows.Forms.DataGridViewLinkColumn Del;
        private System.Windows.Forms.Button btnNew;
    }
}