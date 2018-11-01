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
    public partial class FrmProductList : FormBase
    {
        Entities entities;
        string frmFrmProductName = "FrmProduct";
        public FrmProductList()
        {
            InitializeComponent();
            entities = new Entities();
        }
        private void FrmProductList_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        private void FrmProductList_Activated(object sender, EventArgs e)
        {
            DataBind();
        }
        private void FrmProductList_Resize(object sender, System.EventArgs e)
        {
            FormResize();
        }
        public override void FormResize()
        {
            FormResize(gridProduct, gridProduct);
        }
        private void DataBind()
        {
            gridProduct.AutoGenerateColumns = false;
            var datas = entities.Product.OrderByDescending(item => item.ProductName).ToList();
            gridProduct.DataSource = datas;
        }
        private void gridProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int id = Convert.ToInt32(gridProduct.Rows[e.RowIndex].Cells[0].Value);
            if (e.ColumnIndex == gridProduct.Columns.Count - 1)
            {
                if (MessageBox.Show("确定要删除此数据？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        entities.Delete<Product>(id);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("数据删除失败！", "提示");
                        LogHelper.WriteLog(LogType.Error, exp, this.GetType());
                    }
                    DataBind();
                }
            }
            else if (e.ColumnIndex == gridProduct.Columns.Count - 2)
            {
                var existFrm = GetForm(frmFrmProductName);
                if (existFrm != null)
                {
                    existFrm.Close();
                }
                FrmProduct frm = new FrmProduct();
                frm.MdiParent = this.MdiParent;
                frm.ProductId = id;
                frm.Show();
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            var existFrm = GetForm(frmFrmProductName);
            if (existFrm != null)
            {
                existFrm.Close();
            }
            FrmProduct frm = new FrmProduct();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
    }
}
