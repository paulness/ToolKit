using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
namespace ReadExcelBuildings
{
    class Program
    {
        static void Main(string[] args)
        {
            var csvHelper = new CsvWriter(new StreamWriter(@"C:\HMI\Rent Stablized Apartments\poutput.csv"));
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\HMI\Rent Stablized Apartments\Copy of 2015ManhattanBldgs.xlsx");

            try
            {
                foreach (Excel._Worksheet xlWorksheet in xlWorkbook.Sheets)
                {
                    Excel.Range xlRange = xlWorksheet.UsedRange;

                    for (int i = 2; i <= xlRange.Rows.Count; i++)
                    {
                        var zip = xlRange.Cells[i, 1];
                        var blg = xlRange.Cells[i, 2];
                        var street = xlRange.Cells[i, 3];
                        var ave = xlRange.Cells[i, 4];

                        if (zip != null && zip.Value2 != null)
                            csvHelper.WriteField(zip.Value2.ToString());
                        if (blg != null && blg.Value2 != null)
                            csvHelper.WriteField(blg.Value2.ToString());
                        if (street != null && street.Value2 != null)
                            csvHelper.WriteField(street.Value2.ToString());
                        if (ave != null && ave.Value2 != null)
                            csvHelper.WriteField(ave.Value2.ToString());

                        csvHelper.NextRecord();
                    }
                }
            }
            finally
            {
                xlWorkbook.Close();
                xlApp.Quit();
                csvHelper.Dispose();
            }
            
        }
    }
}
