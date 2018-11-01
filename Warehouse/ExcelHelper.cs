using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Warehouse
{
    public class ExcelHelper
    {
        public class WriteExcelEventArgs : EventArgs
        {
            public readonly Workbook ExceWorkbook;
            public readonly Microsoft.Office.Interop.Excel.Application ExcelApp;
            public Object UserData;
            public WriteExcelEventArgs(Workbook workbook, Object userData, Application excelApp)
            {
                ExceWorkbook = workbook;
                UserData = userData;
                ExcelApp = excelApp;
            }
        }
        public delegate void WriteExcelEventHandler(object sender, WriteExcelEventArgs e);
        public event WriteExcelEventHandler WriteExcelEvent;

        private string fileName;
        public ExcelHelper(string fileName)
        {
            this.fileName = fileName;
        }
        public void WriteExcel(Object userData)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbooks workbooks = excel.Workbooks;
            Workbook workBook = workbooks.Add(true);
            try
            {
                excel.DisplayAlerts = false;
                excel.AlertBeforeOverwriting = false;

                WriteExcelEventArgs args = new WriteExcelEventArgs(workBook, userData, excel);
                if (WriteExcelEvent != null)
                {
                    WriteExcelEvent(this, args);
                }
                //((Worksheet)workBook.Worksheets[workBook.Worksheets]).Delete();

                workBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, XlSaveAsAccessMode.xlNoChange,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            finally
            {
                workBook.Close();
                workbooks.Close();
                excel.Quit();
                GC.Collect();
            }
        }
    }
}
