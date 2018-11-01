﻿namespace Warehouse
{
    partial class FrmWarehouseOutList
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
            this.gridWarehouse = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RequestedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SendBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AuditBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Del = new System.Windows.Forms.DataGridViewLinkColumn();
            this.btnExport = new System.Windows.Forms.Button();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.folderSelect = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gridWarehouse)).BeginInit();
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
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // gridWarehouse
            // 
            this.gridWarehouse.AllowUserToAddRows = false;
            this.gridWarehouse.AllowUserToDeleteRows = false;
            this.gridWarehouse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridWarehouse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.RequestedBy,
            this.SendBy,
            this.BillDate,
            this.AuditBy,
            this.btn,
            this.Del});
            this.gridWarehouse.Location = new System.Drawing.Point(10, 45);
            this.gridWarehouse.Name = "gridWarehouse";
            this.gridWarehouse.ReadOnly = true;
            this.gridWarehouse.RowTemplate.Height = 23;
            this.gridWarehouse.Size = new System.Drawing.Size(715, 605);
            this.gridWarehouse.TabIndex = 1;
            this.gridWarehouse.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridWarehouse_CellContentClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // RequestedBy
            // 
            this.RequestedBy.DataPropertyName = "RequestedBy";
            this.RequestedBy.HeaderText = "领料人";
            this.RequestedBy.Name = "RequestedBy";
            this.RequestedBy.ReadOnly = true;
            // 
            // SendBy
            // 
            this.SendBy.DataPropertyName = "SendBy";
            this.SendBy.HeaderText = "发料人";
            this.SendBy.Name = "SendBy";
            this.SendBy.ReadOnly = true;
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
            // AuditBy
            // 
            this.AuditBy.DataPropertyName = "AuditBy";
            this.AuditBy.HeaderText = "审批";
            this.AuditBy.Name = "AuditBy";
            this.AuditBy.ReadOnly = true;
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
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(445, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "导出出库单";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dtTo
            // 
            this.dtTo.Location = new System.Drawing.Point(310, 10);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(120, 21);
            this.dtTo.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "到";
            // 
            // dtFrom
            // 
            this.dtFrom.Location = new System.Drawing.Point(160, 10);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(120, 21);
            this.dtFrom.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "从：";
            // 
            // FrmWarehouseOutList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(734, 661);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.gridWarehouse);
            this.Name = "FrmWarehouseOutList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "出库单列表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.frmWarehouseOutList_Activated);
            this.Load += new System.EventHandler(this.frmWarehouseOutList_Load);
            this.Resize += new System.EventHandler(this.FrmWarehouseOutList_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.gridWarehouse)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }        

        #endregion

        private System.Windows.Forms.DataGridView gridWarehouse;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn RequestedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn SendBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuditBy;
        private System.Windows.Forms.DataGridViewLinkColumn btn;
        private System.Windows.Forms.DataGridViewLinkColumn Del;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderSelect;
    }
}