using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WinForms;
using Warehouse.Models;

namespace Warehouse.Report
{
    public enum ReportType
    {
        WarehouseIn,
        WarehouseOut,
        RepairedBill
    }
    public partial class frmReport : FormBase
    {
        private Entities entities;
        public frmReport(ReportType reportType)
        {
            _reportType = reportType;
            InitializeComponent();
            entities = new Entities();
            _fromDateTime = new DateTime(1900, 1, 1);
            _toDateTime = DateTime.Now.ClearTime();
            this.Text = Title;
        }
        private string Title
        {
            get
            {
                switch (_reportType)
                {
                    case ReportType.WarehouseIn:
                        return "维修材料签收单";
                        break;
                    case ReportType.WarehouseOut:
                        return "维修材料出库单";
                        break;
                    case ReportType.RepairedBill:
                        return "文化广场管理所维修材料清单";
                        break;
                }
                return base.Text;
            }
        }

        private DateTime _fromDateTime;
        private DateTime _toDateTime;
        private ReportType _reportType;
        private void LoadReport()
        {
            rptViewer.ProcessingMode = ProcessingMode.Local;

            var pagerSize = new PaperSize()
            {
                Height = 1169,
                Width = 827
            };
            var margins = new Margins()
            {
                Bottom = 50,
                Left = 50,
                Right = 50,
                Top = 50
            };

            PageSettings ps = new PageSettings();
            ps.PaperSize = pagerSize;
            ps.Margins = margins;
            ps.PrinterSettings.DefaultPageSettings.PaperSize = pagerSize;
            ps.PrinterSettings.DefaultPageSettings.Margins = margins;
            rptViewer.SetPageSettings(ps);

            rptViewer.LocalReport.ReportPath = ReportPath;
            rptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LoadSubreport);
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.DataSources.Add(GetDataSource());
            //var reportMargins = rptViewer.LocalReport.GetDefaultPageSettings().Margins;
            //reportMargins.Left = 0;
            //reportMargins.Right = 0;
            //reportMargins.Top = 0;
            //reportMargins.Bottom = 0;
            //rptViewer.LocalReport.GetDefaultPageSettings().PaperSize.Height = pagerSize.Height;
            //rptViewer.LocalReport.GetDefaultPageSettings().PaperSize.Width = pagerSize.Width;
            rptViewer.RefreshReport();
        }
        private string ReportPath
        {
            get
            {
                string reportPath = "";
                switch (_reportType)
                {
                    case ReportType.WarehouseIn:
                        reportPath = "rptIn.rdlc";
                        break;
                    case ReportType.WarehouseOut:
                        reportPath = "rptOut.rdlc";
                        break;
                    case ReportType.RepairedBill:
                        reportPath = "rptRepairedBill.rdlc";
                        break;
                }
                return "Report\\" + reportPath;
            }
        }
        private void LoadSubreport(object sender, SubreportProcessingEventArgs e)
        {
            var id = Convert.ToInt32(e.Parameters["Id"].Values[0]);
            e.DataSources.Clear();
            if (e.DataSourceNames[0] == "DataHeader")
            {
                e.DataSources.Add(GetDataHeaderSource(id));
            }
            else
            {
                e.DataSources.Add(GetDataItemSource(id));
            }
        }
        private ReportDataSource GetDataSource()
        {
            switch (_reportType)
            {
                case ReportType.WarehouseIn:
                    var inData =
                        entities.WarehouseIn.Where(wi => wi.BillDate >= _fromDateTime && _toDateTime >= wi.BillDate)
                            .ToList();
                    return new ReportDataSource("Data", inData);
                    break;
                case ReportType.WarehouseOut:
                    var outData =
                        entities.WarehouseOut.Where(wi => wi.BillDate >= _fromDateTime && _toDateTime >= wi.BillDate)
                            .ToList();
                    return new ReportDataSource("Data", outData);
                    break;
                case ReportType.RepairedBill:
                    var warehouseIn = entities.WarehouseIn.Where(wi => wi.BillDate >= _fromDateTime && _toDateTime >= wi.BillDate).ToList();
                    var temp = from w in warehouseIn
                               from item in entities.WarehouseInItem.Where(t => t.WarehouseIn_Id == w.Id)
                               select item;
                    temp = from t in temp
                           group t by t.Product_Id
                               into data
                               select new WarehouseInItem()
                               {
                                   Product_Id = data.Key,
                                   Quantity = data.Sum(d => d.Quantity),
                                   UnitPrice = data.Sum(d => d.UnitPrice * d.Quantity) / data.Sum(d => d.Quantity)
                               };
                    if (temp != null && temp.Count() > 0)
                    {
                        var items = temp.ToList();
                        foreach (var item in items)
                        {
                            var product = entities.Product.FirstOrDefault(p => p.Id == item.Product_Id);
                            if (product != null)
                            {
                                item.ProductName = product.ProductName;
                                item.Brand = product.Brand;
                                item.Specification = product.Specification;
                                item.Unit = product.Unit;
                            }
                        }
                        while (items.Count < 28)
                        {
                            items.Add(new WarehouseInItem());
                        }
                        return new ReportDataSource("Data", items);
                    }
                    break;
            }
            return null;
        }
        private ReportDataSource GetDataHeaderSource(int id)
        {
            switch (_reportType)
            {
                case ReportType.WarehouseIn:
                    return new ReportDataSource("DataHeader", entities.WarehouseIn.Where(wi => wi.Id == id).ToList());
                    break;
                case ReportType.WarehouseOut:
                    return new ReportDataSource("DataHeader", entities.WarehouseOut.Where(wi => wi.Id == id).ToList());
                    break;
            }
            return null;

        }
        private ReportDataSource GetDataItemSource(int id)
        {
            switch (_reportType)
            {
                case ReportType.WarehouseIn:
                    {
                        var items = entities.WarehouseInItem.Where(wi => wi.WarehouseIn_Id == id).ToList();
                        foreach (var item in items)
                        {
                            var product = entities.Product.FirstOrDefault(p => p.Id == item.Product_Id);
                            if (product != null)
                            {
                                item.ProductName = product.ProductName;
                                item.Brand = product.Brand;
                                item.Specification = product.Specification;
                                item.Unit = product.Unit;
                            }
                        }
                        while (items.Count < 18)
                        {
                            items.Add(new WarehouseInItem());
                        }
                        return new ReportDataSource("DataItem", items);
                    }
                    break;
                case ReportType.WarehouseOut:
                    {
                        var items = entities.WarehouseOutItem.Where(wi => wi.WarehouseOut_Id == id).ToList();
                        foreach (var item in items)
                        {
                            var product = entities.Product.FirstOrDefault(p => p.Id == item.Product_Id);
                            if (product != null)
                            {
                                item.ProductName = product.ProductName;
                                item.Brand = product.Brand;
                                item.Specification = product.Specification;
                                item.Unit = product.Unit;
                            }
                        }
                        while (items.Count < 18)
                        {
                            items.Add(new WarehouseOutItem());
                        }
                        return new ReportDataSource("DataItem", items);
                    }
                    break;
            }
            return null;
        }

        public override void FormResize()
        {
            FormResize(rptViewer, rptViewer);
        }
        private void frmReport_Resize(object sender, System.EventArgs e)
        {
            FormResize();
        }
        private void frmReport_Load(object sender, EventArgs e)
        {
            FormResize(rptViewer, rptViewer);
        }

        private void btnGeneral_Click(object sender, EventArgs e)
        {
            _fromDateTime = dtFrom.Value.ClearTime();
            _toDateTime = dtTo.Value.ClearTime();
            LoadReport();
        }
    }
}
