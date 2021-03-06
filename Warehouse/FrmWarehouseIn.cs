﻿using System;
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
    public partial class FrmWarehouseIn : FormBase
    {
        Entities entities;
        public FrmWarehouseIn()
        {
            InitializeComponent();
            entities = new Entities();
            WarehouseInId = 0;
        }
        public int WarehouseInId { get; set; }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                WarehouseIn objW = new WarehouseIn();
                objW.Id = WarehouseInId;
                objW.Vender = txtVendor.Text.Trim();
                objW.ReceivedBy = txtReceivedBy.Text.Trim();
                objW.AuditBy = txtAuditBy.Text.Trim();
                objW.BillDate = dtBillDate.Value.ClearTime();
                objW.ReviewedBy = txtReviewedBy.Text.Trim();
                if (objW.ReceivedBy == objW.ReviewedBy)
                {
                    MessageBox.Show("收货人与复核人不能是同一个人！", "提示");
                    return;
                }
                IList<WarehouseInItem> lst = (gridIn.DataSource as BindingSource).DataSource as IList<WarehouseInItem>;
                //foreach (DataGridViewRow row in gridIn.Rows)
                //{
                //    WarehouseInItem item = new WarehouseInItem();
                //    item.Id = Convert.ToInt32(row.Cells["Id"].Value);
                //    item.Product_Id = Convert.ToInt32(row.Cells["ProductName"].Value);
                //    item.ProductName = Convert.ToString(row.Cells["ProductName"].FormattedValue);
                //    item.Brand = Convert.ToString(row.Cells["Brand"].Value);
                //    item.Specification = Convert.ToString(row.Cells["Specification"].Value);
                //    item.Unit = Convert.ToString(row.Cells["Unit"].Value);
                //    item.Memo = Convert.ToString(row.Cells["Memo"].Value);
                //    item.Quantity = Convert.ToDouble(row.Cells["Quantity"].Value);
                //    item.UnitPrice = Convert.ToDouble(row.Cells["UnitPrice"].Value);
                //    if (item.Quantity > 0)
                //        lst.Add(item);
                //}
                IList<int> needDelItem = new List<int>();
                var oldItem = (new Entities()).WarehouseInItem.Where(item => item.WarehouseIn_Id == WarehouseInId).ToList();
                if (WarehouseInId > 0)
                {
                    IList<int> lstId = oldItem.Where(item => item.WarehouseIn_Id == WarehouseInId).Select(d => d.Id).ToList();
                    needDelItem = lstId.Except(lst.Where(item => item.Id > 0 && item.Quantity > 0 && item.Product_Id > 0).Select(item => item.Id).ToList()).ToList();
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    entities.Save<WarehouseIn>(objW);
                    foreach (WarehouseInItem item in lst)
                    {
                        if (item.Quantity <= 0|| item.Product_Id<=0)
                        {
                            continue;
                        }
                        double changedQty = item.Quantity;
                        item.WarehouseIn_Id = objW.Id;
                        if (item.Id > 0)
                        {
                            var temp = oldItem.First(tmp => tmp.Id == item.Id);
                            changedQty = item.Quantity - temp.Quantity;
                        }
                        entities.Save<WarehouseInItem>(item);
                        var product = entities.Product.Find(item.Product_Id);
                        product.StockQuantity = product.StockQuantity + changedQty;
                        entities.Save<Product>(product);
                    }
                    if (needDelItem.Count > 0)
                    {
                        var delItems = oldItem.Where(tmp => needDelItem.Contains(tmp.Id));
                        var temp = from t in delItems
                                   group t by t.Product_Id into tt
                                   select new
                                   {
                                       ProductId = tt.Key,
                                       Quantity = tt.Sum(x => x.Quantity)
                                   };
                        foreach (var t1 in temp)
                        {
                            var product = entities.Product.Find(t1.ProductId);

                            product.StockQuantity = product.StockQuantity - t1.Quantity;
                            entities.Save<Product>(product);
                        }
                        entities.Delete<WarehouseInItem>(needDelItem);
                    }
                    scope.Complete();
                }
                MessageBox.Show("数据保存成功！", "提示");
                this.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show("数据保存失败！", "提示");
                LogHelper.WriteLog(LogType.Error, exp, this.GetType());
            }
        }
        private void frmWarehouseIn_Load(object sender, EventArgs e)
        {
            IList<Product> lstProducts = entities.Product.OrderBy(p => p.ProductName).ToList();
            lstProducts.Add(new Product());
            ProductName.DataSource = lstProducts;
            ProductName.DisplayMember = "ProductName";
            ProductName.ValueMember = "Id";
            gridIn.EditMode = DataGridViewEditMode.EditOnEnter;
            gridIn.AutoGenerateColumns = false;

            BindingSource bs = new BindingSource();
            if (WarehouseInId > 0)
            {
                WarehouseIn objW = entities.WarehouseIn.Find(WarehouseInId);
                if (objW != null)
                {
                    txtVendor.Text = objW.Vender;
                    txtReceivedBy.Text = objW.ReceivedBy;
                    txtAuditBy.Text = objW.AuditBy;
                    dtBillDate.Value = objW.BillDate;
                    txtReviewedBy.Text = objW.ReviewedBy;
                }
                IList<WarehouseInItem> lst =
                    entities.WarehouseInItem.Where(item => item.WarehouseIn_Id == WarehouseInId).ToList();
                foreach (var item in lst)
                {
                    var product = entities.Product.Find(item.Product_Id);
                    item.ProductName = product.ProductName;
                    item.Brand = product.Brand;
                    item.Unit = product.Unit;
                    item.Specification = product.Specification;
                }
                bs.DataSource = lst;
            }
            else
            {
                bs.DataSource = new List<WarehouseInItem>();
            }
            bs.AllowNew = true;
            bs.AddingNew += bs_AddingNew;
            gridIn.DataSource = bs;
            dtBillDate.Focus();
        }
        private void bs_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new WarehouseInItem();
        }
        private void FrmWarehouseIn_Resize(object sender, System.EventArgs e)
        {
            FormResize();
        }
        public override void FormResize()
        {
            FormResize(gridIn, btnSave);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridIn_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (gridIn.CurrentCell.ColumnIndex == 0)
            {
                ComboBox combo = e.Control as ComboBox;
                if (combo != null)
                {
                    combo.SelectedIndexChanged -= productName_SelectedIndexChanged;
                    combo.SelectedIndexChanged += productName_SelectedIndexChanged;
                }
            }
            else if (gridIn.CurrentCell.ColumnIndex == 3)
            {
                TextBox txt = e.Control as TextBox;
                if (txt != null)
                {
                    txt.KeyPress += txt_KeyPress;
                }
            }
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            int kc = (int)e.KeyChar;
            if ((kc < 48 || kc > 57) && kc != 8)
                e.Handled = true;
        }
        private void productName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            var product = combo.SelectedItem;
            if (product != null)
            {
                var prd = product as Product;
                var currentCell = this.gridIn.CurrentCell;
                var rowIndex = currentCell.RowIndex;
                var dataSource = ((gridIn.DataSource as BindingSource).DataSource as IList<WarehouseInItem>);
                WarehouseInItem item = dataSource[rowIndex];
                item.Brand = prd.Brand;
                item.Specification = prd.Specification;
                item.Unit = prd.Unit;
                item.UnitPrice = prd.UnitPrice;

                //gridIn.Rows[rowIndex].Cells["Brand"].Value = prd.Brand;
                //gridIn.Rows[rowIndex].Cells["Specification"].Value = prd.Specification;
                //gridIn.Rows[rowIndex].Cells["Unit"].Value = prd.Unit;
                //gridIn.Rows[rowIndex].Cells["UnitPrice"].Value = prd.UnitPrice.ToString();
            }

        }
    }
}
