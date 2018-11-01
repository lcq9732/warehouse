using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warehouse.Models;

namespace Warehouse
{
    public partial class FrmRepairedBillList : FormBase
    {
        Entities entities;
        string frmRepairedBillName = "FrmRepairedBill";
        public FrmRepairedBillList()
        {
            InitializeComponent();
            entities = new Entities();
        }
        private void FrmRepairedBillList_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        private void FrmRepairedBillList_Activated(object sender, EventArgs e)
        {
            DataBind();
        }
        private void FrmRepairedBillList_Resize(object sender, System.EventArgs e)
        {
            FormResize(gridRepairedBill, gridRepairedBill);
        }
        private void DataBind()
        {
            //gridRepairedBill.AutoGenerateColumns = false;
            //var datas = entities.RepairedBill.OrderByDescending(item => item.BillDate).ToList();
            //gridRepairedBill.DataSource = datas;
        }
        private void gridRepariedBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            int id = Convert.ToInt32(gridRepairedBill.Rows[e.RowIndex].Cells[0].Value);
            if (e.ColumnIndex == gridRepairedBill.Columns.Count - 1)
            {
                if (MessageBox.Show("确定要删除此数据？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        entities.Delete<RepairedBill>(id);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("数据删除失败！", "提示");
                        LogHelper.WriteLog(LogType.Error, exp, this.GetType());
                    }
                    DataBind();
                }
            }
            else if (e.ColumnIndex == gridRepairedBill.Columns.Count - 2)
            {
                var existFrm = GetForm(frmRepairedBillName);
                if (existFrm != null)
                {
                    existFrm.Close();
                }
                FrmRepairedBill frm = new FrmRepairedBill();
                frm.MdiParent = this.MdiParent;
                frm.RepairedBillId = id;
                frm.Show();
            }*/
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            var existFrm = GetForm(frmRepairedBillName);
            if (existFrm != null)
            {
                existFrm.Close();
            }
            FrmRepairedBill frm = new FrmRepairedBill();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
    }
}
