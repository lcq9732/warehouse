using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Warehouse.Models;

namespace Warehouse
{
    public partial class FrmWarehouseOutList : FormBase
    {
        string frmWarehouseOutName = "FrmWarehouseOut";
        Entities entities;
        public FrmWarehouseOutList()
        {
            InitializeComponent();
            entities = new Entities();
        }
        private void frmWarehouseOutList_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        private void FrmWarehouseOutList_Resize(object sender, System.EventArgs e)
        {
            FormResize();
        }
        public override void FormResize()
        {
            FormResize(gridWarehouse, gridWarehouse);
        }
        private void frmWarehouseOutList_Activated(object sender, System.EventArgs e)
        {
            DataBind();
        }
        private void DataBind()
        {
            gridWarehouse.AutoGenerateColumns = false;
            var datas = entities.WarehouseOut.OrderByDescending(item => item.BillDate).ToList();
            gridWarehouse.DataSource = datas;
        }
        private void gridWarehouse_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(gridWarehouse.Rows[e.RowIndex].Cells[0].Value);
            if (e.ColumnIndex == gridWarehouse.Columns.Count - 1)
            {
                if (MessageBox.Show("确定要删除此数据？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        var items = entities.WarehouseOutItem.Where(item => item.WarehouseOut_Id == id).ToList();
                        var productIds = items.Select(p => p.Product_Id).ToList();
                        var products = entities.Product.Where(p => productIds.Contains(p.Id)).ToList();
                        using (TransactionScope scope = new TransactionScope())
                        {
                            foreach (var item in items)
                            {
                                var product = products.First(p => p.Id == item.Product_Id);
                                product.StockQuantity = product.StockQuantity + item.Quantity;
                                entities.Save<Product>(product);
                            }
                            entities.Delete<WarehouseOut>(id);
                            scope.Complete();
                        }
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("数据删除失败！", "提示");
                        LogHelper.WriteLog(LogType.Error, exp, this.GetType());
                    }
                    DataBind();
                }
            }
            if (e.ColumnIndex == gridWarehouse.Columns.Count - 2)
            {
                var existFrm = GetForm(frmWarehouseOutName);
                if (existFrm != null)
                {
                    existFrm.Close();
                }
                FrmWarehouseOut frm = new FrmWarehouseOut();
                frm.MdiParent = this.MdiParent;
                frm.WarehouseOutId = id;
                frm.Show();
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            var existFrm = GetForm(frmWarehouseOutName);
            if (existFrm != null)
            {
                existFrm.Close();
            }
            FrmWarehouseOut frm = new FrmWarehouseOut();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (folderSelect.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            try
            {
                string fileName = folderSelect.SelectedPath + "\\Out" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
                var fromDt = dtFrom.Value.ClearTime();
                var toDt = dtTo.Value.ClearTime();
                var warehouseOuts = entities.WarehouseOut.Where(wi => wi.BillDate >= fromDt && toDt >= wi.BillDate).ToList();
                if (warehouseOuts.Count > 0)
                {
                    foreach (var warehouseOut in warehouseOuts)
                    {
                        warehouseOut.Items = entities.WarehouseOutItem.Where(d => d.WarehouseOut_Id == warehouseOut.Id).ToList();
                        foreach (var item in warehouseOut.Items)
                        {
                            var product = entities.Product.Find(item.Product_Id);
                            item.ProductName = product.ProductName;
                            item.Brand = product.Brand;
                            item.Unit = product.Unit;
                            item.Specification = product.Specification;
                        }
                    }
                }
                if (warehouseOuts.Count <= 0)
                {
                    MessageBox.Show("没有相关数据！", "提示");
                    return;
                }
                ExcelHelper excel = new ExcelHelper(fileName);
                excel.WriteExcelEvent += excel_WriteExcelEvent;
                excel.WriteExcel(warehouseOuts);
                MessageBox.Show("数据导出成功！", "提示");
            }
            catch (Exception exp)
            {
                MessageBox.Show("数据导出失败！", "提示");
                LogHelper.WriteLog(LogType.Error, exp, this.GetType());
            }
        }
        private void excel_WriteExcelEvent(object sender, ExcelHelper.WriteExcelEventArgs e)
        {
            IList<WarehouseOut> warehouseOuts = e.UserData as IList<WarehouseOut>;
            Workbook workBook = e.ExceWorkbook;
            foreach (var warehouseOut in warehouseOuts)
            {
                Worksheet worksheet = (Worksheet)workBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                worksheet.Name = "出库单" + workBook.Worksheets.Count;
                worksheet.Cells[1, 1] = "维修材料出库单";
                worksheet.Cells[2, 6] = "日期";
                worksheet.Cells[2, 7] = warehouseOut.BillDate.ToShortDateString();

                worksheet.Cells[3, 1] = "序号";
                worksheet.Cells[3, 2] = "物品名称";
                worksheet.Cells[3, 3] = "牌子";
                worksheet.Cells[3, 4] = "规格型号";
                worksheet.Cells[3, 5] = "数量";
                worksheet.Cells[3, 6] = "单位";
                worksheet.Cells[3, 7] = "备注";

                int rowIndex = 4;
                foreach (var item in warehouseOut.Items)
                {
                    worksheet.Cells[rowIndex, 1] = rowIndex - 3;
                    worksheet.Cells[rowIndex, 2] = (item.ProductName);
                    worksheet.Cells[rowIndex, 3] = (item.Brand);
                    worksheet.Cells[rowIndex, 4] = (item.Specification);
                    worksheet.Cells[rowIndex, 5] = (item.Quantity);
                    worksheet.Cells[rowIndex, 6] = (item.Unit);
                    worksheet.Cells[rowIndex, 7] = (item.Memo);
                    rowIndex++;
                }
                //var rowCount = rowIndex - 4 + 18;
                for (; rowIndex < 22; rowIndex++)
                {
                    worksheet.Cells[rowIndex, 1] = (rowIndex - 3);
                    worksheet.Cells[rowIndex, 2] = "";
                    worksheet.Cells[rowIndex, 3] = "";
                    worksheet.Cells[rowIndex, 4] = "";
                    worksheet.Cells[rowIndex, 5] = "";
                    worksheet.Cells[rowIndex, 6] = "";
                    worksheet.Cells[rowIndex, 7] = "";
                }
                worksheet.Cells[rowIndex + 1, 1] = (string.Format("领料人:{0,-13}  发料人:{1,-13}  复核:{2,-13}  审批:{3,-13}", warehouseOut.RequestedBy, warehouseOut.SendBy, warehouseOut.ReviewedBy, warehouseOut.AuditBy));

                ((Range)worksheet.Cells[1, 1]).ColumnWidth = 5;
                ((Range)worksheet.Cells[1, 2]).ColumnWidth = 20;
                ((Range)worksheet.Cells[1, 3]).ColumnWidth = 13;
                ((Range)worksheet.Cells[1, 4]).ColumnWidth = 13;
                ((Range)worksheet.Cells[1, 5]).ColumnWidth = 8;
                ((Range)worksheet.Cells[1, 6]).ColumnWidth = 8;
                ((Range)worksheet.Cells[1, 7]).ColumnWidth = 14;

                Range rangeTitle = worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[1, 7]);
                rangeTitle.Merge();
                rangeTitle.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                rangeTitle.VerticalAlignment = XlVAlign.xlVAlignCenter;
                rangeTitle.Font.Bold = true;
                rangeTitle.Font.Size = 24;
                rangeTitle.RowHeight = 35;

                Range rangeDate = worksheet.get_Range(worksheet.Cells[2, 1], worksheet.Cells[2, 7]);
                rangeDate.RowHeight = 25;

                Range rangeHeader = worksheet.get_Range(worksheet.Cells[3, 1], worksheet.Cells[3, 7]);
                rangeHeader.Font.Bold = true;
                rangeHeader.Font.Size = 12;
                rangeHeader.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                rangeHeader.VerticalAlignment = XlVAlign.xlVAlignCenter;
                rangeHeader.RowHeight = 27;

                Range rangeData = worksheet.get_Range(worksheet.Cells[3, 1], worksheet.Cells[rowIndex - 1, 7]);
                rangeData.Borders.get_Item(XlBordersIndex.xlInsideHorizontal).LineStyle = XlLineStyle.xlContinuous;
                rangeData.Borders.get_Item(XlBordersIndex.xlInsideVertical).LineStyle = XlLineStyle.xlContinuous;
                rangeData.Borders.get_Item(XlBordersIndex.xlEdgeLeft).LineStyle = XlLineStyle.xlContinuous;
                rangeData.Borders.get_Item(XlBordersIndex.xlEdgeRight).LineStyle = XlLineStyle.xlContinuous;
                rangeData.Borders.get_Item(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous;
                rangeData.Borders.get_Item(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlContinuous;
                rangeData.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                rangeData.VerticalAlignment = XlVAlign.xlVAlignCenter;
                rangeData.RowHeight = 33;

                worksheet.get_Range(worksheet.Cells[rowIndex, 1], worksheet.Cells[rowIndex, 7]).RowHeight = 24;

                Range rangeFooter = worksheet.get_Range(worksheet.Cells[rowIndex + 1, 1], worksheet.Cells[rowIndex + 1, 7]);
                rangeFooter.RowHeight = 24;
                rangeFooter.Merge();

                worksheet.PageSetup.TopMargin = e.ExcelApp.CentimetersToPoints(1.5);
                worksheet.PageSetup.BottomMargin = e.ExcelApp.CentimetersToPoints(1.5);
                worksheet.PageSetup.LeftMargin = e.ExcelApp.CentimetersToPoints(1.4);
                worksheet.PageSetup.RightMargin = e.ExcelApp.CentimetersToPoints(1.4);

                worksheet.PageSetup.HeaderMargin = e.ExcelApp.CentimetersToPoints(1);
                worksheet.PageSetup.FooterMargin = e.ExcelApp.CentimetersToPoints(1);
            }
        }
    }
}
