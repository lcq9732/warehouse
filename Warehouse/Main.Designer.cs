namespace Warehouse
{
    partial class Main
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
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuWarehouseIn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWarehouseOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRepaired = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemIn = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemOut = new System.Windows.Forms.ToolStripMenuItem();
            this.mItemRepired = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuWarehouseIn,
            this.menuWarehouseOut,
            this.menuProduct,
            this.menuRepaired,
            this.menuReport});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1134, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menuStrip1";
            // 
            // menuWarehouseIn
            // 
            this.menuWarehouseIn.Name = "menuWarehouseIn";
            this.menuWarehouseIn.Size = new System.Drawing.Size(53, 20);
            this.menuWarehouseIn.Text = "签收单";
            this.menuWarehouseIn.Click += new System.EventHandler(this.menuWarehouseIn_Click);
            // 
            // menuWarehouseOut
            // 
            this.menuWarehouseOut.Name = "menuWarehouseOut";
            this.menuWarehouseOut.Size = new System.Drawing.Size(53, 20);
            this.menuWarehouseOut.Text = "出库单";
            this.menuWarehouseOut.Click += new System.EventHandler(this.menuWarehouseOut_Click);
            // 
            // menuProduct
            // 
            this.menuProduct.Name = "menuProduct";
            this.menuProduct.Size = new System.Drawing.Size(53, 20);
            this.menuProduct.Text = "材料库";
            this.menuProduct.Click += new System.EventHandler(this.menuProduct_Click);
            // 
            // menuRepaired
            // 
            this.menuRepaired.Name = "menuRepaired";
            this.menuRepaired.Size = new System.Drawing.Size(89, 20);
            this.menuRepaired.Text = "维修材料清单";
            this.menuRepaired.Click += new System.EventHandler(this.menuRepaired_Click);
            // 
            // menuReport
            // 
            this.menuReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mItemIn,
            this.mItemOut,
            this.mItemRepired});
            this.menuReport.Name = "menuReport";
            this.menuReport.Size = new System.Drawing.Size(41, 20);
            this.menuReport.Text = "报表";
            this.menuReport.Visible = true;
            // 
            // mItemIn
            // 
            this.mItemIn.Name = "mItemIn";
            this.mItemIn.Size = new System.Drawing.Size(152, 22);
            this.mItemIn.Text = "签收单";
            this.mItemIn.Click += new System.EventHandler(this.mItemIn_Click);
            // 
            // mItemOut
            // 
            this.mItemOut.Name = "mItemOut";
            this.mItemOut.Size = new System.Drawing.Size(152, 22);
            this.mItemOut.Text = "出库单";
            this.mItemOut.Click += new System.EventHandler(this.mItemOut_Click);
            // 
            // mItemRepired
            // 
            this.mItemRepired.Name = "mItemRepired";
            this.mItemRepired.Size = new System.Drawing.Size(152, 22);
            this.mItemRepired.Text = "维修单";
            this.mItemRepired.Click += new System.EventHandler(this.mItemRepired_Click);
            this.mItemRepired.Visible = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1134, 728);
            this.Controls.Add(this.menu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menu;
            this.Name = "Main";
            this.Text = "主窗口";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menuWarehouseIn;
        private System.Windows.Forms.ToolStripMenuItem menuWarehouseOut;
        private System.Windows.Forms.ToolStripMenuItem menuProduct;
        private System.Windows.Forms.ToolStripMenuItem menuRepaired;
        private System.Windows.Forms.ToolStripMenuItem menuReport;
        private System.Windows.Forms.ToolStripMenuItem mItemIn;
        private System.Windows.Forms.ToolStripMenuItem mItemOut;
        private System.Windows.Forms.ToolStripMenuItem mItemRepired;
    }
}

