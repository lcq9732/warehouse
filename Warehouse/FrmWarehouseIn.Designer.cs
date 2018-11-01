using System.Windows.Forms;

namespace Warehouse
{
    partial class FrmWarehouseIn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtBillDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.gridIn = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVendor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReceivedBy = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAuditBy = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReviewedBy = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ProductName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Specification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Memo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridIn)).BeginInit();
            this.SuspendLayout();
            // 
            // dtBillDate
            // 
            this.dtBillDate.Location = new System.Drawing.Point(70, 37);
            this.dtBillDate.Name = "dtBillDate";
            this.dtBillDate.Size = new System.Drawing.Size(130, 21);
            this.dtBillDate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "日期：";
            // 
            // gridIn
            // 
            this.gridIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridIn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductName,
            this.Brand,
            this.Specification,
            this.Quantity,
            this.Unit,
            this.Memo,
            this.Id,
            this.UnitPrice});
            this.gridIn.Location = new System.Drawing.Point(8, 69);
            this.gridIn.Name = "gridIn";
            this.gridIn.RowTemplate.Height = 23;
            this.gridIn.Size = new System.Drawing.Size(1064, 550);
            this.gridIn.TabIndex = 2;
            this.gridIn.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridIn_EditingControlShowing);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(412, 704);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 637);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "送货单位:";
            // 
            // txtVendor
            // 
            this.txtVendor.Location = new System.Drawing.Point(70, 633);
            this.txtVendor.Name = "txtVendor";
            this.txtVendor.Size = new System.Drawing.Size(250, 21);
            this.txtVendor.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(336, 637);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "收货人:";
            // 
            // txtReceivedBy
            // 
            this.txtReceivedBy.Location = new System.Drawing.Point(389, 633);
            this.txtReceivedBy.Name = "txtReceivedBy";
            this.txtReceivedBy.Size = new System.Drawing.Size(250, 21);
            this.txtReceivedBy.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(348, 673);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "审批:";
            // 
            // txtAuditBy
            // 
            this.txtAuditBy.Location = new System.Drawing.Point(389, 669);
            this.txtAuditBy.Name = "txtAuditBy";
            this.txtAuditBy.Size = new System.Drawing.Size(250, 21);
            this.txtAuditBy.TabIndex = 9;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(530, 704);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 673);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "复核：";
            // 
            // txtReviewedBy
            // 
            this.txtReviewedBy.Location = new System.Drawing.Point(70, 669);
            this.txtReviewedBy.Name = "txtReviewedBy";
            this.txtReviewedBy.Size = new System.Drawing.Size(250, 21);
            this.txtReviewedBy.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(450, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 24);
            this.label6.TabIndex = 13;
            this.label6.Text = "维修材料签收表";
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "Product_Id";
            this.ProductName.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.ProductName.HeaderText = "物品名称";
            this.ProductName.Name = "ProductName";
            this.ProductName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ProductName.Width = 200;
            // 
            // Brand
            // 
            this.Brand.DataPropertyName = "Brand";
            this.Brand.HeaderText = "牌子";
            this.Brand.Name = "Brand";
            this.Brand.ReadOnly = true;
            // 
            // Specification
            // 
            this.Specification.DataPropertyName = "Specification";
            this.Specification.HeaderText = "规格型号";
            this.Specification.Name = "Specification";
            this.Specification.ReadOnly = true;
            this.Specification.Width = 200;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = "0";
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle1;
            this.Quantity.HeaderText = "数量";
            this.Quantity.Name = "Quantity";
            this.Quantity.Width = 80;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "Unit";
            this.Unit.HeaderText = "单位";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 80;
            // 
            // Memo
            // 
            this.Memo.DataPropertyName = "Memo";
            this.Memo.HeaderText = "备注";
            this.Memo.Name = "Memo";
            this.Memo.Width = 280;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            dataGridViewCellStyle2.NullValue = "0";
            this.Id.DefaultCellStyle = dataGridViewCellStyle2;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // UnitPrice
            // 
            this.UnitPrice.DataPropertyName = "UnitPrice";
            this.UnitPrice.HeaderText = "单价";
            this.UnitPrice.Name = "UnitPrice";
            this.UnitPrice.ReadOnly = true;
            this.UnitPrice.Visible = false;
            // 
            // FrmWarehouseIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1084, 733);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtReviewedBy);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtAuditBy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtReceivedBy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtVendor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gridIn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtBillDate);
            this.Name = "FrmWarehouseIn";
            this.Text = "维修材料签收表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmWarehouseIn_Load);
            this.Resize += new System.EventHandler(this.FrmWarehouseIn_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.gridIn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtBillDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridIn;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVendor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReceivedBy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAuditBy;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtReviewedBy;
        private System.Windows.Forms.Label label6;
        private DataGridViewComboBoxColumn ProductName;
        private DataGridViewTextBoxColumn Brand;
        private DataGridViewTextBoxColumn Specification;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn Unit;
        private DataGridViewTextBoxColumn Memo;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn UnitPrice;
    }
}