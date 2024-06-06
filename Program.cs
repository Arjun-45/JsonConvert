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
        string filePath = @"C:\Users\SMM-LP149\source\repos\XLI\JsonConvert\template\testdata1.xlsx";

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
                List<ImportRobDto> importRobDtos = new();

                ImportRobDto robForEventDtos = new();
                List<EventRobsRowDto> eventRobsRowDtos = new();
                bool firstRow = true;
                var dt = new DataTable();
                var ds = new DataSet();
                var cellLastAddress = worksheet.LastCellUsed().Address.ColumnNumber;
                string[] fuelTypes = { "HSFO", "VLSFO", "ULSFO", "LSMGO", "LSIFO" };
                var rowMainH = worksheet.RowsUsed().Skip(1).First();
                var rowSubH = worksheet.RowsUsed().Skip(2).First();
                string remarks = "Events";
                var reportDateTimeColumn = worksheet.CellsUsed(c => c.Value.ToString() == "ReportDateTime").FirstOrDefault().Address.ColumnNumber;

                var reportRows = worksheet.RowsUsed().Skip(3).ToList();

                for (int i = 0; i < reportRows.Count; i++)
                {
                    List<ImportRobDto> eventRobDtos = new();

                    var row = reportRows[i];
                    var reportDateCell = row.Cell(reportDateTimeColumn);
                    if (string.IsNullOrEmpty(reportDateCell.GetString()))
                    {
                        var nextRowWithDate = reportRows.Skip(i + 1)
                            .FirstOrDefault(r => !string.IsNullOrEmpty(r.Cell(reportDateTimeColumn).GetString()));
                        if (nextRowWithDate != null)
                        {
                            i = reportRows.IndexOf(nextRowWithDate);
                            row = nextRowWithDate;
                        }
                        else
                        {
                            continue; // Skip this iteration if no more rows with a date are found.
                        }
                    }

                    importForm = new ImportFormDto();
                    importRobDtos = new List<ImportRobDto>();
                    importForm.EventRobsRowDtos = new List<EventRobsRowDto>();

                    ImportRobDto importRobDto = new ImportRobDto();
                    var remarksCell = worksheet.CellsUsed(c => c.Value.ToString() == remarks).FirstOrDefault().Address.ColumnNumber;

                    foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, remarksCell - 1))
                    {
                        var headColumnName = rowMainH.Cell(cell.WorksheetColumn().ColumnNumber()).Value.ToString();
                        var subHeadColumnName = rowSubH.Cell(cell.WorksheetColumn().ColumnNumber()).Value.ToString();
                        importRobDto = GetRobs(importForm, importRobDtos, fuelTypes, importRobDto, cell, headColumnName, subHeadColumnName);
                    }

                    importRobDtos.Add(importRobDto);
                    importForm.Robs = importRobDtos;

                    // Map event details
                    ImportRobDto eventRobDto = new ImportRobDto();
                    if (remarksCell <= cellLastAddress)
                    {
                        EventRobsRowDto currentEventRobsRowDto = new EventRobsRowDto { Robs = new List<ImportRobDto>() };
                        foreach (IXLCell cell in row.Cells(remarksCell, cellLastAddress))
                        {
                            var headColumnName = rowMainH.Cell(cell.WorksheetColumn().ColumnNumber()).Value.ToString();
                            var subHeadColumnName = rowSubH.Cell(cell.WorksheetColumn().ColumnNumber()).Value.ToString();
                            currentEventRobsRowDto = GetEventRobs(currentEventRobsRowDto, cell, headColumnName, subHeadColumnName);
                            eventRobDto = GetRobs(importForm, eventRobDtos, fuelTypes, eventRobDto, cell, headColumnName, subHeadColumnName);
                        }
                        eventRobDtos.Add(eventRobDto);
                        currentEventRobsRowDto.Robs = eventRobDtos;
                        importForm.EventRobsRowDtos.Add(currentEventRobsRowDto);

                    }

                    // Map subsequent event rows


                    var eventRows = reportRows.Skip(i + 1).TakeWhile(r => string.IsNullOrEmpty(r.Cell(reportDateTimeColumn).GetString())).ToList();
                    foreach (var eventRow in eventRows)
                    {
                        ImportRobDto eventRobLegDto = new ImportRobDto();
                        List<ImportRobDto> eventRobLegDtos = new();
                        EventRobsRowDto eventRobsRowDto = new EventRobsRowDto { Robs = new List<ImportRobDto>() };
                        foreach (IXLCell cell in eventRow.Cells(remarksCell, cellLastAddress))
                        {
                            var headColumnName = rowMainH.Cell(cell.WorksheetColumn().ColumnNumber()).Value.ToString();
                            var subHeadColumnName = rowSubH.Cell(cell.WorksheetColumn().ColumnNumber()).Value.ToString();
                            eventRobsRowDto = GetEventRobs(eventRobsRowDto, cell, headColumnName, subHeadColumnName);
                            eventRobLegDto = GetRobs(importForm, eventRobLegDtos, fuelTypes, eventRobLegDto, cell, headColumnName, subHeadColumnName);
                        }
                        eventRobLegDtos.Add(eventRobLegDto);
                        eventRobsRowDto.Robs = eventRobLegDtos;
                        importForm.EventRobsRowDtos.Add(eventRobsRowDto);

                    }
                    importForms.Add(importForm);
                }
            }
            return importForms;
        }

        private EventRobsRowDto GetEventRobs(EventRobsRowDto eventRobsRowDto, IXLCell cell, string headColumnName, string subHeadColumnName)
        {
            var type = eventRobsRowDto.GetType();
            var propertyInfo = type.GetProperty(subHeadColumnName);
            var val = cell.Value.ToString();
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(eventRobsRowDto, val);
            }

            return eventRobsRowDto;
        }


        private static ImportRobDto GetRobs(ImportFormDto importForm, List<ImportRobDto> importRobDtos, string[] fuelTypes, ImportRobDto importRobDto, IXLCell cell, string headColumnName, string subHeadColumnName)
        {
            if (fuelTypes.Contains(headColumnName))
            {
                if (!string.IsNullOrEmpty(importRobDto.FuelType))
                {
                    importRobDtos.Add(importRobDto);
                    importRobDto = new ImportRobDto();
                }
                var fuel = headColumnName;
                importRobDto.FuelType = fuel;
                var col = subHeadColumnName;
                var val = cell.Value.ToString();
                var type = importRobDto.GetType();
                var propertyInfo = type.GetProperty(col);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(importRobDto, val);
                }
            }
            else if (!string.IsNullOrEmpty(importRobDto.FuelType))
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

            return importRobDto;
        }

        private static void GetEventDetails(List<ImportFormDto> importForms, IXLWorksheet worksheet, ref ImportFormDto importForm, List<ImportRobDto> importRobDtos, ref ImportRobDto robForEventDtos, ref List<EventRobsRowDto> eventRobsRowDtos, string[] fuelTypes, IXLRow rowMainH, IXLRow rowSubH, string remarks)
        {
            foreach (IXLRow row in worksheet.Rows().Skip(3))
            {
                //Use the first row to add columns to DataTable.
                importForm = new ImportFormDto();
                eventRobsRowDtos = new List<EventRobsRowDto>();
                EventRobsRowDto eventRobsRowDto = new EventRobsRowDto();
                robForEventDtos = new ImportRobDto();


                var remarksCell = worksheet.CellsUsed(c => c.Value.ToString() == remarks).FirstOrDefault().Address.ColumnNumber;
                var test = row.LastCellUsed().Address.ColumnNumber;

                foreach (IXLCell cell in row.Cells(remarksCell, row.LastCellUsed().Address.ColumnNumber))
                {

                    var headColumnName = rowMainH.Cell(cell.WorksheetColumn().ColumnNumber()).Value.ToString();
                    var subHeadColumnName = rowSubH.Cell(cell.WorksheetColumn().ColumnNumber()).Value.ToString();
                    robForEventDtos = GetRobs(importForm, importRobDtos, fuelTypes, robForEventDtos, cell, headColumnName, subHeadColumnName);

                }
                eventRobsRowDtos.Add(eventRobsRowDto);
                importForm.EventRobsRowDtos = eventRobsRowDtos;
                importForm.Robs = importRobDtos;
                importForms.Add(importForm);
            }
        }


    }

}
