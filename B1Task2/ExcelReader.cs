using B1Task2.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace B1Task2
{
    internal class ExcelReader
    {
        public List<FinancialData> ReadExcelFile(string filePath)
        {
            try
            {
                var financialDataList = new List<FinancialData>();

                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var workbook = new HSSFWorkbook(fs);
                    var sheet = workbook.GetSheetAt(0);

                    for (int row = 8; row <= sheet.LastRowNum; row++) // Пропускаем заголовок
                    {
                        var excelRow = sheet.GetRow(row);
                        var data = GetData(excelRow);
                        if (data != null)
                        {
                            financialDataList.Add(data);
                        }
                    }
                }
                return financialDataList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private FinancialData GetData(IRow excelRow)
        {
            try
            {
                var data = new FinancialData();
                if (excelRow == null)
                {
                    return null;
                }
                var firstCell = excelRow.GetCell(0);
                if (firstCell != null && firstCell.CellType == CellType.String && firstCell.StringCellValue.Trim().StartsWith("КЛАСС"))
                {
                    data.AccountNumber = GetFormattedCellValue(excelRow.GetCell(0));
                    return data;
                }

                data.AccountNumber = GetFormattedCellValue(excelRow.GetCell(0));
                data.InitialActiveBalance = GetFormattedCellValue(excelRow.GetCell(1));
                data.InitialPassiveBalance = GetFormattedCellValue(excelRow.GetCell(2));
                data.DebitTurnover = GetFormattedCellValue(excelRow.GetCell(3));
                data.CreditTurnover = GetFormattedCellValue(excelRow.GetCell(4));
                data.FinalActiveBalance = GetFormattedCellValue(excelRow.GetCell(5));
                data.FinalPassiveBalance = GetFormattedCellValue(excelRow.GetCell(6));


                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string GetFormattedCellValue(ICell cell)
        {
            if (cell == null) return string.Empty;
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
                case CellType.String:
                    return cell.StringCellValue;
                default:
                    return string.Empty;
            }
        }
    }
}
