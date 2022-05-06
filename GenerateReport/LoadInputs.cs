using System;
using System.Data;
using Microsoft.Data.Analysis;

namespace ReportingAgent{
    class ProccessData{
    public static DataTable ConvertCSVtoDataTable(string strFilePath)
    {
        DataTable dtInput = new DataTable();
        using (StreamReader sr = new StreamReader(strFilePath))
        {
            string[] headers = sr.ReadLine().Split(';');
            foreach (string header in headers)
            {
                dtInput.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(';');
                DataRow dr = dtInput.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dtInput.Rows.Add(dr);
            }

        }
        // Csv Columns fields are all strings by default, we have to change them to the correct data types
        DataTable dtOutput = dtInput.Clone();
        dtOutput.Columns[0].DataType = typeof(Int32);
        dtOutput.Columns[1].DataType = typeof(Int32);
        dtOutput.Columns[2].DataType = typeof(String);
        dtOutput.Columns[3].DataType = typeof(Int32);
        foreach (DataRow row in dtInput.Rows) 
        {
            int i = 0;

            Convert.ToInt32(dtInput.Rows[0][i]);
            Convert.ToInt32(dtInput.Rows[1][i]);
            Convert.ToInt32(dtInput.Rows[3][i]);
            dtOutput.ImportRow(row);
            i++;
        }
        return dtOutput;
    }
    }
}