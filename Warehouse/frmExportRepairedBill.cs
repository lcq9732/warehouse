using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warehouse.Models;
using Microsoft.Office.Interop.Excel;

namespace Warehouse
{
    public partial class frmExportRepairedBill : FormBase
    {
        Entities entities;
        public frmExportRepairedBill()
        {
            InitializeComponent();
            entities = new Entities();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (folderSelect.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            try
            {
                string fileName = folderSelect.SelectedPath + "\\Bill" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
                var fromDt = dtFrom.Value.ClearTime();
                var toDt = dtTo.Value.ClearTime();
                IList<WarehouseInItem> items = new List<WarehouseInItem>();

                var warehouseIns = entities.WarehouseIn.Where(wi => wi.BillDate >= fromDt && toDt >= wi.BillDate).ToList();
                if (warehouseIns.Count > 0)
                {
                    foreach (var warehouseIn in warehouseIns)
                    {
                        warehouseIn.Items = entities.WarehouseInItem.Where(d => d.WarehouseIn_Id == warehouseIn.Id).ToList();
                        items = items.Union(warehouseIn.Items).ToList();
                    }
                    items = (from t in items
                             group t by t.Product_Id
                                 into data
                                 select new WarehouseInItem()
                                 {
                                     Product_Id = data.Key,
                                     Quantity = data.Sum(d => d.Quantity),
                                     UnitPrice = data.Sum(d => d.UnitPrice * d.Quantity) / data.Sum(d => d.Quantity)
                                 }).ToList();
                    foreach (var item in items)
                    {
                        var product = entities.Product.Find(item.Product_Id);
                        item.ProductName = product.ProductName;
                        item.Brand = product.Brand;
                        item.Unit = product.Unit;
                        item.Specification = product.Specification;
                    }
                }
                if (items.Count <= 0)
                {
                    MessageBox.Show("没有相关数据！", "提示");
                    return;
                }
                ExcelHelper excel = new ExcelHelper(fileName);
                excel.WriteExcelEvent += excel_WriteExcelEvent;
                excel.WriteExcel(items);
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
            IList<WarehouseInItem> items = e.UserData as IList<WarehouseInItem>;
            Workbook workBook = e.ExceWorkbook; ;
            Worksheet worksheet = (Worksheet)workBook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            worksheet.Name = "维修材料清单";
            worksheet.Cells[1, 1] = ("惠东文化广场管理所维修材料清单");
            worksheet.Cells[2, 7] = ("日期");
            worksheet.Cells[2, 8] = (DateTime.Now.ToShortDateString());

            worksheet.Cells[3, 1] = ("序号");
            worksheet.Cells[3, 2] = ("物品名称");
            worksheet.Cells[3, 3] = ("牌子");
            worksheet.Cells[3, 4] = ("规格型号");
            worksheet.Cells[3, 5] = ("数量");
            worksheet.Cells[3, 6] = ("单位");
            worksheet.Cells[3, 7] = ("单价(元)");
            worksheet.Cells[3, 8] = ("总价(元)");

            int rowIndex = 4;
            foreach (var item in items)
            {
                worksheet.Cells[rowIndex, 1] = (rowIndex - 3);
                worksheet.Cells[rowIndex, 2] = (item.ProductName);
                worksheet.Cells[rowIndex, 3] = (item.Brand);
                worksheet.Cells[rowIndex, 4] = (item.Specification);
                worksheet.Cells[rowIndex, 5] = (item.Quantity);
                worksheet.Cells[rowIndex, 6] = (item.Unit);
                worksheet.Cells[rowIndex, 7] = (item.UnitPrice);
                worksheet.Cells[rowIndex, 8] = (string.Format("=E{0}*G{0}", rowIndex));
                rowIndex++;
            }
            //var rowCount = rowIndex - 4 + 27;
            for (; rowIndex < 31; rowIndex++)
            {
                worksheet.Cells[rowIndex, 1] = (rowIndex - 3);
                worksheet.Cells[rowIndex, 2] = "";
                worksheet.Cells[rowIndex, 3] = "";
                worksheet.Cells[rowIndex, 4] = "";
                worksheet.Cells[rowIndex, 5] = "";
                worksheet.Cells[rowIndex, 6] = "";
                worksheet.Cells[rowIndex, 7] = "";
            }
            worksheet.Cells[rowIndex, 6] = ("综合费用");
            worksheet.Cells[rowIndex, 8] = (string.Format("=sum(H2:H{0})", rowIndex - 1));

            ((Range)worksheet.Cells[1, 1]).ColumnWidth = 5;
            ((Range)worksheet.Cells[1, 2]).ColumnWidth = 15;
            ((Range)worksheet.Cells[1, 3]).ColumnWidth = 11;
            ((Range)worksheet.Cells[1, 4]).ColumnWidth = 12;
            ((Range)worksheet.Cells[1, 5]).ColumnWidth = 6.5;
            ((Range)worksheet.Cells[1, 6]).ColumnWidth = 6.5;
            ((Range)worksheet.Cells[1, 7]).ColumnWidth = 11;
            ((Range)worksheet.Cells[1, 8]).ColumnWidth = 12;

            Range rangeTitle = worksheet.get_Range(worksheet.Cells[1, 1], worksheet.Cells[1, 8]);
            rangeTitle.Merge();
            rangeTitle.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeTitle.VerticalAlignment = XlVAlign.xlVAlignCenter;
            rangeTitle.Font.Bold = true;
            rangeTitle.Font.Size = 24;
            rangeTitle.RowHeight = 28.5;

            Range rangeSpace = worksheet.get_Range(worksheet.Cells[2, 1], worksheet.Cells[2, 8]);
            rangeSpace.RowHeight = 25;

            Range rangeHeader = worksheet.get_Range(worksheet.Cells[3, 1], worksheet.Cells[3, 8]);
            rangeHeader.Font.Bold = true;
            rangeHeader.Font.Size = 12;
            rangeHeader.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader.VerticalAlignment = XlVAlign.xlVAlignCenter;
            rangeHeader.RowHeight = 21;

            Range rangeData = worksheet.get_Range(worksheet.Cells[3, 1], worksheet.Cells[rowIndex, 8]);
            rangeData.Borders.get_Item(XlBordersIndex.xlInsideHorizontal).LineStyle = XlLineStyle.xlContinuous;
            rangeData.Borders.get_Item(XlBordersIndex.xlInsideVertical).LineStyle = XlLineStyle.xlContinuous;
            rangeData.Borders.get_Item(XlBordersIndex.xlEdgeLeft).LineStyle = XlLineStyle.xlContinuous;
            rangeData.Borders.get_Item(XlBordersIndex.xlEdgeRight).LineStyle = XlLineStyle.xlContinuous;
            rangeData.Borders.get_Item(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous;
            rangeData.Borders.get_Item(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlContinuous;
            rangeData.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeData.VerticalAlignment = XlVAlign.xlVAlignCenter;
            rangeData.RowHeight = 21;

            Range rangeTotal = worksheet.get_Range(worksheet.Cells[rowIndex, 6], worksheet.Cells[rowIndex, 7]);
            rangeTotal.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeTotal.VerticalAlignment = XlVAlign.xlVAlignCenter;
            rangeTotal.Font.Bold = true;
            rangeTotal.Font.Size = 12;
            rangeTotal.RowHeight = 21;
            rangeTotal.Merge();

            Range rangeUnitPrice = worksheet.get_Range(worksheet.Cells[3, 7], worksheet.Cells[rowIndex - 1, 7]);
            rangeUnitPrice.NumberFormat = "0.00";
            Range rangeAmount = worksheet.get_Range(worksheet.Cells[3, 8], worksheet.Cells[rowIndex, 8]);
            rangeAmount.NumberFormat = "0.00";
            rangeAmount.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeAmount.VerticalAlignment = XlVAlign.xlVAlignCenter;

            worksheet.PageSetup.TopMargin = e.ExcelApp.CentimetersToPoints(2.5);
            worksheet.PageSetup.BottomMargin = e.ExcelApp.CentimetersToPoints(2.5);
            worksheet.PageSetup.LeftMargin = e.ExcelApp.CentimetersToPoints(1.4);
            worksheet.PageSetup.RightMargin = e.ExcelApp.CentimetersToPoints(1.4);
            worksheet.PageSetup.HeaderMargin = e.ExcelApp.CentimetersToPoints(1.3);
            worksheet.PageSetup.FooterMargin = e.ExcelApp.CentimetersToPoints(1.3);
        }
    }
}
