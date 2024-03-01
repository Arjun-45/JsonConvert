using System;
using System.Data;
using System.IO.Packaging;
using System.Text.Json;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using JsonConvert.Dto;
using Newtonsoft.Json;
using OfficeOpenXml;


class Program
{
    static void Main(string[] args)
    {
        string filePath = @"C:\Users\Arjun S\source\repos\JsonConvert\template\Report ingestion template New.xlsx";

        ExcelReader excelReader = new ExcelReader();
        List<ImportFormDto> importForms = excelReader.ReadExcel(filePath);

        string json = System.Text.Json.JsonSerializer.Serialize(importForms, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);
        //foreach (var importForm in importForms)
        //{
        //    Console.WriteLine($"FormIdentifier: {importForm.FormIdentifier}, VesselCode: {importForm.VesselCode}");
        //}
    }


    public class ExcelReader
    {
        public List<ImportFormDto> ReadExcel(string filePath)
        {
            List<ImportFormDto> importForms = new List<ImportFormDto>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);

                var rows = worksheet.RowsUsed().Skip(1);
                int rowIndex = 1;
                ImportFormDto importForm = new();
                List<ImportRobDto> importRobDtos= new();
                bool firstRow = true;
                var dt = new DataTable();
                var ds = new DataSet();
                var cellLastAddress = worksheet.LastCellUsed().Address.ColumnNumber;
                string[] fuelTypes = { "HSFO" ,"VLSFO","ULSFO"};
                var rowMainH = worksheet.RowsUsed().Skip(1).First();
                var rowSubH = worksheet.RowsUsed().Skip(2).First();

                foreach (IXLRow row in worksheet.Rows().Skip(3))
                {
                    //Use the first row to add columns to DataTable.
                    importForm = new ImportFormDto();
                    importRobDtos = new List<ImportRobDto>();
                    ImportRobDto importRobDto = new ImportRobDto();

                    foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                    {

                        var headColumnName = rowMainH.Cell(cell.WorksheetColumn().ColumnNumber()).Value.ToString();
                        var subHeadColumnName = rowSubH.Cell(cell.WorksheetColumn().ColumnNumber()).Value.ToString();
                        if (fuelTypes.Contains(headColumnName))
                        {
                            if (!string.IsNullOrEmpty(importRobDto.fuelType))
                            {
                                importRobDtos.Add(importRobDto);
                                importRobDto = new ImportRobDto();
                            }
                            var fuel = headColumnName;
                            importRobDto.fuelType = fuel;
                            var col = subHeadColumnName;
                            var val = cell.Value.ToString();
                            var type = importRobDto.GetType();
                            var propertyInfo = type.GetProperty(col);
                            if (propertyInfo != null)
                            {
                                propertyInfo.SetValue(importRobDto, val);
                            }
                        }
                        else if (!string.IsNullOrEmpty(importRobDto.fuelType))
                        {
                            var col = subHeadColumnName;
                            var val = cell.Value.ToString();
                            var type = importRobDto.GetType();
                            var propertyInfo = type.GetProperty(col);
                            if (propertyInfo != null)
                            {
                                propertyInfo.SetValue(importRobDto, val);
                            }
                        }
                        else
                        {
                            var col = headColumnName;
                            var val = cell.Value.ToString();
                            var type = importForm.GetType();
                            var propertyInfo = type.GetProperty(col);
                            if (propertyInfo != null)
                            {
                                propertyInfo.SetValue(importForm, val);
                            }
                        }

                        
                    }
                    importRobDtos.Add(importRobDto);
                    importForm.Robs = importRobDtos;
                    importForms.Add(importForm);
                }

            }
            return importForms;
        }
    }
}
