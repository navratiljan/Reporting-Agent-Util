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
	    // Call Python script to generate Combined wathclist statistics
            ExecuteScript.CallScript();

            // Generate Report
            string currentDir = Directory.GetCurrentDirectory();
            string WatchlistFilePath = $"{currentDir}/InputStatistics/Combined_Watchlist_Statistics_New.csv";

            var GeneratedDataTable = ProccessData.ConvertCSVtoDataTable(WatchlistFilePath);
            var ProcessedDataTable = ProccessInputs.QueryDataTable(GeneratedDataTable);
	
	    // Displays DataTable for debugging purposes
            var debugTable = new DebugTable();
            debugTable.Table(ProcessedDataTable);
        }
    }
}
