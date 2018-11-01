using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Warehouse.Models;

namespace Warehouse
{
    public partial class FrmWarehouseInList : FormBase
    {
        Entities entities;
        string frmWarehouseInName = "FrmWarehouseIn";
        public FrmWarehouseInList()
        {
            InitializeComponent();
            entities = new Entities();
        }
        private void frmWarehouseInList_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        private void FrmWarehouseInList_Resize(object sender, System.EventArgs e)
        {
            FormResize();
        }
        public override void FormResize()
        {
            FormResize(gridWarehouse, gridWarehouse);
        }
        private void frmWarehouseInList_Activated(object sender, EventArgs e)
        {
            DataBind();
        }
        private void DataBind()
        {
            gridWarehouse.AutoGenerateColumns = false;
            var datas = entities.WarehouseIn.OrderByDescending(item => item.BillDate).ToList();
            gridWarehouse.DataSource = datas;
        }
        private void gridWarehouse_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(gridWarehouse.Rows[e.RowIndex].Cells[0].Value);
            if (e.ColumnIndex == gridWarehouse.Columns.Count - 1)
            {
                if (MessageBox.Show("确定要删除此数据？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var items = entities.WarehouseInItem.Where(item => item.WarehouseIn_Id == id).ToList();

                    try
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            foreach (var item in items)
                            {
                                var product = entities.Product.Find(item.Product_Id);

                                product.StockQuantity = product.StockQuantity - item.Quantity;
                                entities.Save<Product>(product);
                            }
                            entities.Delete<WarehouseIn>(id);
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
            else if (e.ColumnIndex == gridWarehouse.Columns.Count - 2)
            {
                var existFrm = GetForm(frmWarehouseInName);
                if (existFrm != null)
                {
                    existFrm.Close();
                }
                FrmWarehouseIn frm = new FrmWarehouseIn();
                frm.MdiParent = this.MdiParent;
                frm.WarehouseInId = id;
                frm.Show();
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            var existFrm = GetForm(frmWarehouseInName);
            if (existFrm != null)
            {
                existFrm.Close();
            }
            FrmWarehouseIn frm = new FrmWarehouseIn();
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
                string fileName = folderSelect.SelectedPath + "\\In" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
                var fromDt = dtFrom.Value.ClearTime();
                var toDt = dtTo.Value.ClearTime();
                var warehouseIns = entities.WarehouseIn.Where(wi => wi.BillDate >= fromDt && toDt >= wi.BillDate).ToList();
                if (warehouseIns.Count > 0)
                {
                    foreach (var warehouseIn in warehouseIns)
                    {
                        warehouseIn.Items = entities.WarehouseInItem.Where(d => d.WarehouseIn_Id == warehouseIn.Id).ToList();
                        foreach (var item in warehouseIn.Items)
                        {
                            var product = entities.Product.Find(item.Product_Id);
                            item.ProductName = product.ProductName;
                            item.Brand = product.Brand;
                            item.Unit = product.Unit;
                            item.Specification = product.Specification;
                        }
                    }
                }
                if (warehouseIns.Count <= 0)
                {
                    MessageBox.Show("没有相关数据！", "提示");
                    return;
                }
                ExcelHelper excel = new ExcelHelper(fileName);
                excel.WriteExcelEvent += excel_WriteExcelEvent;
                excel.WriteExcel(warehouseIns);
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
            IList<WarehouseIn> warehouseIns = e.UserData as IList<WarehouseIn>;
            Workbook workBook = e.ExceWorkbook;
            foreach (var warehouseIn in warehouseIns)
            {
                var worksheet = (Worksheet)workBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                worksheet.Activate();
                worksheet.Name = "签收表" + workBook.Worksheets.Count;

                worksheet.Cells[1, 1] = ("维修材料签收表");
                worksheet.Cells[2, 6] = ("日期");
                worksheet.Cells[2, 7] = (warehouseIn.BillDate.ToShortDateString());

                worksheet.Cells[3, 1] = ("序号");
                worksheet.Cells[3, 2] = ("物品名称");
                worksheet.Cells[3, 3] = ("牌子");
                worksheet.Cells[3, 4] = ("规格型号");
                worksheet.Cells[3, 5] = ("数量");
                worksheet.Cells[3, 6] = ("单位");
                worksheet.Cells[3, 7] = ("备注");

                int rowIndex = 4;
                foreach (var item in warehouseIn.Items)
                {
                    worksheet.Cells[rowIndex, 1] = (rowIndex - 3);
                    worksheet.Cells[rowIndex, 2] = (item.ProductName);
                    worksheet.Cells[rowIndex, 3] = (item.Brand);
                    worksheet.Cells[rowIndex, 4] = (item.Specification);
                    worksheet.Cells[rowIndex, 5] = (item.Quantity);
                    worksheet.Cells[rowIndex, 6] = (item.Unit);
                    worksheet.Cells[rowIndex, 7] = (item.Memo);
                    rowIndex++;
                }
                //var rowCount = rowIndex - 4 + 18;
                for (; rowIndex < 22;  rowIndex++)
                {
                    worksheet.Cells[rowIndex, 1] = (rowIndex - 3);
                    worksheet.Cells[rowIndex, 2] = "";
                    worksheet.Cells[rowIndex, 3] = "";
                    worksheet.Cells[rowIndex, 4] = "";
                    worksheet.Cells[rowIndex, 5] = "";
                    worksheet.Cells[rowIndex, 6] = "";
                    worksheet.Cells[rowIndex, 7] = "";
                }
                worksheet.Cells[rowIndex + 1, 1] =
                    (string.Format("送货单位:{0,-17}  收货人:{1,-10}  复核:{2,-10}  审批:{3,-10}", warehouseIn.Vender, warehouseIn.ReceivedBy, warehouseIn.ReviewedBy,warehouseIn.AuditBy));


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
        //private void SetBorder(Worksheet worksheet, int fromRow, int toRow, int fromCol, int toCol)
        //{
        //    for (var i = fromRow; i < toRow + 1; i++)
        //    {
        //        for (var j = fromCol; j < toCol + 1; j++)
        //        {
        //            Range range = worksheet.get_Range(worksheet.Cells[i, j]);
        //            range.Borders.get_Item(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous;
        //            range.Borders.get_Item(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlContinuous;
        //            range.Borders.get_Item(XlBordersIndex.xlEdgeLeft).LineStyle = XlLineStyle.xlContinuous;
        //            range.Borders.get_Item(XlBordersIndex.xlEdgeRight).LineStyle = XlLineStyle.xlContinuous;
        //        }
        //    }
        //}
    }
    //public class KeyMyExcelProcess
    //{
    //    [DllImport("User32.dll", CharSet = CharSet.Auto)]
    //    public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
    //    public static void Kill(Microsoft.Office.Interop.Excel.Application excel)
    //    {
    //        try
    //        {
    //            IntPtr t = new IntPtr(excel.Hwnd);   //得到这个句柄，具体作用是得到这块内存入口
    //            int k = 0;
    //            GetWindowThreadProcessId(t, out k);   //得到本进程唯一标志k
    //            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);   //得到对进程k的引用
    //            p.Kill();     //关闭进程k
    //        }
    //        catch (System.Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //}
}
