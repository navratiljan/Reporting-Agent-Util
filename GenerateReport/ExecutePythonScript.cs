using System;
using System.Text;
using IronPython.Hosting;

namespace ReportingAgent
{
    public class ExecuteScript
    {
        public static void CallScript(){
            Console.WriteLine("Calling Python script to generate combined watchlist statistics");

            // Create python source engine
            var pythonEngine = Python.CreateEngine();
            string currentDir = Directory.GetCurrentDirectory();
            var PythonScript = $"{currentDir}/analyze_articles.py";
            var source = pythonEngine.CreateScriptSourceFromFile(PythonScript);

            // call with 0 arguments passed
            var argv = new List<string>();
            argv.Add("");
            pythonEngine.GetSysModule().SetVariable("argv", argv);

            // redirect stderr of the script
            var eIO = pythonEngine.Runtime.IO;
            var errors = new MemoryStream();
            eIO.SetErrorOutput(errors, Encoding.Default);

            // redirect stdout of the script 
            var output = new MemoryStream();
            eIO.SetOutput(output, Encoding.Default);

            // Execute python script, exit this program if execution fails
            string str(byte[]x) => Encoding.Default.GetString(x);
            try
            {
            var scope = pythonEngine.CreateScope();
            source.Execute(scope);
            }
            catch(Exception)
            {
            Console.WriteLine("Python script ERRORS:");
            Console.WriteLine(str(errors.ToArray()));

            throw new Exception("Reporting Agent exiting with non-zero status code");
            }
            // Display output if execution succeeded
            Console.WriteLine("Python script OUTPUT:");
            Console.WriteLine(str(output.ToArray()));
        }
    }
}