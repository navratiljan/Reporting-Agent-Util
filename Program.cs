// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using ReportingAgent;

namespace Main
{
    class MainProgram
    {
        public static void Main(string[] args)
        {
	    // Call Python script to generate Combined watchlist statistics
        ExecuteScript.CallScript();

        // Generate Report
        string currentDir = Directory.GetCurrentDirectory();
        string WatchlistFilePath = $"{currentDir}/InputStatistics/Combined_Watchlist_Statistics.csv";

        var GeneratedDataTable = ProccessData.ConvertCSVtoDataTable(WatchlistFilePath);
        var ProcessDataTables = ProccessInputs.QueryDataTable(GeneratedDataTable);

	    // Displays DataTable for debugging purposes (visible only in debugger)
        var debugTable = new DebugTable();
        var screeningTable = ProcessDataTables[0];
        var monitoringTable = ProcessDataTables[1];
        debugTable.Table(screeningTable);
        debugTable.Table(monitoringTable);

        // Send mail with the report
        string reportFilePath = $"{currentDir}/InputStatistics/Combined_Watchlist_Statistics.csv";
        MailingAgent.SendMail("dummymail1@example.com", "dummymail2@example.com", reportFilePath);
        }
    }
}
