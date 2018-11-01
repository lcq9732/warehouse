using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warehouse.Report;

namespace Warehouse
{
    public partial class Main : FormBase
    {
        public Main()
        {
            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            (new Entities()).Product.Find(0);
        }
        private void menuWarehouseIn_Click(object sender, EventArgs e)
        {
            var existFrm = GetForm("FrmWarehouseInList");
            if (existFrm != null)
            {
                existFrm.Show();
                existFrm.Activate();
                return;
            }
            FrmWarehouseInList frm = new FrmWarehouseInList();
            frm.MdiParent = this;
            frm.Show();
        }
        private void menuWarehouseOut_Click(object sender, EventArgs e)
        {
            var existFrm = GetForm("FrmWarehouseOutList");
            if (existFrm != null)
            {
                existFrm.Show();
                existFrm.Activate();
                return;
            }
            FrmWarehouseOutList frm = new FrmWarehouseOutList();
            frm.MdiParent = this;
            frm.Show();
        }
        private void menuProduct_Click(object sender, EventArgs e)
        {
            var existFrm = GetForm("FrmProductList");
            if (existFrm != null)
            {
                existFrm.Show();
                existFrm.Activate();
                return;
            }
            FrmProductList frm = new FrmProductList();
            frm.MdiParent = this;
            frm.Show();

        }
        private void menuRepaired_Click(object sender, EventArgs e)
        {
            frmExportRepairedBill frm = new frmExportRepairedBill();
            frm.ShowDialog();
        }

        private void mItemIn_Click(object sender, EventArgs e)
        {
            frmReport rpt = new frmReport(ReportType.WarehouseIn);
            rpt.MdiParent = this;
            rpt.Show();
        }

        private void mItemOut_Click(object sender, EventArgs e)
        {
            frmReport rpt = new frmReport(ReportType.WarehouseOut);
            rpt.MdiParent = this;
            rpt.Show();
        }
        private void mItemRepired_Click(object sender, EventArgs e)
        {
            frmReport rpt = new frmReport(ReportType.RepairedBill);
            rpt.MdiParent = this;
            rpt.Show();
        }
    }
}
