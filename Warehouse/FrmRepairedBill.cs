using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using Warehouse.Models;

namespace Warehouse
{
    public partial class FrmRepairedBill : FormBase
    {
        Entities entities;
        public FrmRepairedBill()
        {
            InitializeComponent();
            entities = new Entities();
            RepairedBillId = 0;
        }
        public int RepairedBillId { get; set; }
        private void btnSave_Click(object sender, EventArgs e)
        {/*
            try
            {
                RepairedBill objR = new RepairedBill();
                objR.Id = RepairedBillId;
                objR.BillDate = dtBillDate.Value;
                double otherFee;
                if (!double.TryParse(txtOtherFee.Text, out otherFee))
                { otherFee = 0; }
                objR.OtherFee = otherFee;
                IList<RepairedBillItem> lst = new List<RepairedBillItem>();
                foreach (DataGridViewRow row in gridRepairedBill.Rows)
                {
                    RepairedBillItem item = new RepairedBillItem();
                    item.Id = Convert.ToInt32(row.Cells["Id"].Value);
                    item.ProjectName = Convert.ToString(row.Cells["ProjectName"].Value);
                    item.Brand = Convert.ToString(row.Cells["Brand"].Value);
                    item.Specification = Convert.ToString(row.Cells["Specification"].Value);
                    item.Unit = Convert.ToString(row.Cells["Unit"].Value);
                    item.Quantity = Convert.ToDouble(row.Cells["Quantity"].Value);
                    item.UnitPrice = Convert.ToDouble(row.Cells["UnitPrice"].Value);
                    item.Memo = Convert.ToString(row.Cells["Memo"].Value);
                    if (item.Quantity > 0)
                        lst.Add(item);
                }
                IList<int> needDelItem = new List<int>();
                if (RepairedBillId > 0)
                {
                    IList<int> lstId = entities.RepairedBillItem.Where(item => item.RepairedBill_Id == RepairedBillId).Select(d => d.Id).ToList();
                    needDelItem = lstId.Except(lst.Where(item => item.Id > 0).Select(item => item.Id).ToList()).ToList();
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    entities.Save<RepairedBill>(objR);
                    foreach (RepairedBillItem item in lst)
                    {
                        item.RepairedBill_Id = objR.Id;
                        entities.Save<RepairedBillItem>(item);
                    }
                    if (needDelItem.Count > 0)
                    {
                        entities.Delete<WarehouseInItem>(needDelItem);
                    }
                    scope.Complete();
                }
                MessageBox.Show("数据保存成功！", "提示");
            }
            catch (Exception exp)
            {
                MessageBox.Show("数据保存失败！", "提示");
                LogHelper.WriteLog(LogType.Error, exp, this.GetType());
            }*/
        }
        private void FrmRepairedBill_Load(object sender, EventArgs e)
        {/*
            gridRepairedBill.AutoGenerateColumns = false;
            if (RepairedBillId > 0)
            {
                RepairedBill objR = entities.RepairedBill.Find(RepairedBillId);
                if (objR != null)
                {
                    dtBillDate.Value = objR.BillDate;
                    txtOtherFee.Text = objR.OtherFee.ToString("##,###.00");
                }
                IList<RepairedBillItem> lst = entities.RepairedBillItem.Where(item => item.RepairedBill_Id == RepairedBillId).ToList(); 
                //foreach (var item in lst)
                //{
                //    var product = entities.Product.Find(item.Product_Id);
                //    item.ProductName = product.ProductName;
                //    item.Brand = product.Brand;
                //    item.Unit = product.Unit;
                //    item.Specification = product.Specification;
                //}
                gridRepairedBill.DataSource = lst;
            }*/
        }
        private void FrmRepairedBill_Resize(object sender, System.EventArgs e)
        {
            FormResize(gridRepairedBill, btnSave);
        }
        private void gridRepairedBill_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 5)
            {
                var quantiy = Convert.ToDouble(gridRepairedBill.Rows[e.RowIndex].Cells[3].Value);
                var unitPrice = Convert.ToDouble(gridRepairedBill.Rows[e.RowIndex].Cells[5].Value);
                gridRepairedBill.Rows[e.RowIndex].Cells[6].Value = quantiy * unitPrice;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
