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
    public partial class FrmProduct : FormBase
    {
        Entities entities;
        public int ProductId;
        public FrmProduct()
        {
            InitializeComponent();
            entities = new Entities();
            ProductId = 0;
            txtQuantity.Text = "0";
            txtUnitPrice.Text = "0.00";
        }
        private void FrmProduct_Load(object sender, EventArgs e)
        {
            if (ProductId > 0)
            {
                Product objP = entities.Product.Find(ProductId);
                if (objP != null)
                {
                    txtProductName.Text = objP.ProductName;
                    txtBrand.Text = objP.Brand;
                    txtSpecification.Text = objP.Specification;
                    txtUnit.Text = objP.Unit;
                    txtUnitPrice.Text = objP.UnitPrice.ToString("##,##0.00");
                    txtQuantity.Text = objP.StockQuantity.ToString();
                }
            }
        }
        private void FrmProduct_Resize(object sender, System.EventArgs e)
        {
            FormResize();
        }
        public override void FormResize()
        {
            FormResize(txtQuantity, btnSave);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Product objP = new Product();
                objP.Id = ProductId;
                objP.ProductName = txtProductName.Text.Trim();
                objP.Brand = txtBrand.Text.Trim();
                objP.Specification = txtSpecification.Text.Trim();
                objP.Unit = txtUnit.Text.Trim();
                objP.UnitPrice = Convert.ToDouble(txtUnitPrice.Text.Trim());
                objP.StockQuantity = Convert.ToDouble(txtQuantity.Text);
                objP.Memo = txtMemo.Text.Trim();

                using (TransactionScope scope = new TransactionScope())
                {
                    entities.Save<Product>(objP);
                    scope.Complete();
                }
                MessageBox.Show("数据保存成功！", "提示");
            }
            catch (Exception exp)
            {
                MessageBox.Show("数据保存失败！", "提示");
                LogHelper.WriteLog(LogType.Error, exp, this.GetType());
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            var temp = txtUnitPrice.Text;
            if (txtUnitPrice.SelectionLength > 0)
            {
                temp = txtUnitPrice.Text.Replace(txtUnitPrice.Text.Substring(txtUnitPrice.SelectionStart, txtUnitPrice.SelectionLength), "");
            }
            if (e.KeyChar == '.' && temp.IndexOf(".") != -1)
            {
                e.Handled = true;
            }

            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == '.' || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }
    }
}
