// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using ReportingAgent;
//using ReportAgent.SendMail;

namespace Main
{
    class MainProgram
    {
        public static void Main(string[] args)
        {
            // ExecuteScript.CallScript();

            // Generate Report
            string currentDir = Directory.GetCurrentDirectory();
            string WatchlistFilePath = $"{currentDir}/InputStatistics/Combined_Watchlist_Statistics_New.csv";

            // FixCsvDataTypes.AlterCsvDataFields(WatchlistFilePath);
            //ConvertCSVtoXLSX.Convert();
            var GeneratedDataTable = ProccessData.ConvertCSVtoDataTable(WatchlistFilePath);
            var ProcessedDataTable = ProccessInputs.QueryDataTable(GeneratedDataTable);

            var debugTable = new DebugTable();
            debugTable.Table(ProcessedDataTable);

            // var arrayNames = (from DataColumn x 
            //       in GeneratedDataTable.Columns.Cast<DataColumn>()
            //       select x.ColumnName).ToArray();

            // System.Console.WriteLine(arrayNames);

            // foreach( DataRow dr in GeneratedDataTable.Rows)
            //     {
            //     foreach(var item in dr.ItemArray)
            //         System.Console.WriteLine(item + "");
            //     }
            //      System.Console.WriteLine();
        }
    }
}