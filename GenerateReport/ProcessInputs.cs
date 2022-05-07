using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using LINQtoCSV;
using GemBox.Spreadsheet;

namespace ReportingAgent
{
    public class ProccessInputs
    {
        public static DataTable[] QueryDataTable(DataTable inputDataTable){
        string currentDir = Directory.GetCurrentDirectory();
        string WatchlistFilePath = $"{currentDir}/InputStatistics/Combined_Watchlist_Statistics.csv";

        var outputDataTableScreening = inputDataTable.AsEnumerable()
              .Where(x => x.Field<string>("Type") == "Screening")
              .GroupBy(r => r.Field<int>("Client"))
              .Select(g =>
              {
                  var row = inputDataTable.NewRow();
                  row["Client"] = g.Key;
                  row["Company_Count"] = g.Sum(r => r.Field<int>("Company_Count"));
                  row["Type"] = "Screening";
                  return row;
              }).CopyToDataTable();

        var outputDataTableMonitoring = inputDataTable.AsEnumerable()
              .Where(x => x.Field<string>("Type") == "Monitoring")
              .GroupBy(r => r.Field<int>("Client"))
              .Select(g =>
              {
                  var row = inputDataTable.NewRow();
                  row["Client"] = g.Key;
                  row["Company_Count"] = g.Max(r => r.Field<int>("Company_Count"));
                  row["Type"] = "Monitoring";
                  return row;
              }).CopyToDataTable();
        SaveAsXlsx(outputDataTableScreening,outputDataTableMonitoring);

        var outputDataTables = new DataTable[2] {outputDataTableScreening, outputDataTableMonitoring};
        return outputDataTables;
        }

        private static void SaveAsXlsx(DataTable inputDataTableScreening, DataTable inputDataTableMonitoring){
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            var workbook = new ExcelFile();
            var worksheetScreeing = workbook.Worksheets.Add("Screening");
            var worksheetMonitoring = workbook.Worksheets.Add("Monitoring");
            worksheetScreeing.InsertDataTable(inputDataTableScreening,
            new InsertDataTableOptions()
            {
                ColumnHeaders = true,
                StartRow = 0
            });

            worksheetMonitoring.InsertDataTable(inputDataTableMonitoring,
            new InsertDataTableOptions()
            {
                ColumnHeaders = true,
                StartRow = 0
            });

            string currentDir = Directory.GetCurrentDirectory();
	        string reportPath = $"{currentDir}/Reports/finalReport.xlsx";
            workbook.Save(reportPath);
        }
    }
}
