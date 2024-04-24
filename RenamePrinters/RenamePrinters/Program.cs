using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Printing;
using System.Threading;
//using Spire.Pdf;

namespace RenamePrinters
{
    class Program
    {

        public static void Main(string[] args)
        {
            //PrintDocument();
            List<string> listPrinters = GetNumberOfPrintJobs();
            
           // RenameFiles();

            Console.ReadKey();
        }
        //Thread.Sleep(3000) fazer esperar um tempo
        private static void RenameFiles()
        {

            string directoryToRename = @"C:\Users\japso\Downloads\PCL\";
            string directoryTo = @"C:\Windows\System32\spool\PRINTERS\";

            string[] pasta = Directory.GetFiles(directoryToRename);
            int number = 1033;
            foreach (string files in pasta)
            {




                if (files.Contains(".SHD"))
                    File.Delete(files);
                else
                {
                    string rename = directoryToRename + number + ".SPL";
                    number++;
                    System.IO.File.Move(files, rename);
                    var fileName = Path.GetFileName(rename);
                    //string moveTo = directoryTo + fileName;
                    //File.Move(rename, moveTo);


                    /*Thread.Sleep(3000)*/
                    ;
                    if (files.Length == 0)
                    {
                        break;
                    }
                }
            }
        }
        //private static void PrintDocument()
        //{
        //    int count = 1;
        //    try
        //    {
        //        while (count < 3000)
        //        {
        //            PdfDocument doc = new PdfDocument(@"C:\Users\gabriel.oliveira\Downloads\PCL\26.pdf");
        //            doc.PrintSettings.PrinterName = "kyoKPDL_dev";
        //            doc.Print();
        //            Thread.Sleep(100);
        //            count++;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //}

        public static void SendToPrinter(string filePath, string Printer)
        {
            try
            {
                Process proc = new Process();
                proc.Refresh();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.Verb = "printto";
                proc.StartInfo.FileName = ConfigurationManager.AppSettings["ReaderPath"].ToString();
                proc.StartInfo.Arguments = String.Format("/t \"{0}\" \"{1}\"", filePath, Printer);
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (proc.HasExited == false)
                {
                    proc.WaitForExit(20000);
                }
                proc.EnableRaisingEvents = true;
                proc.Close();
            }
            catch (Exception e)
            {
            }
        }

        private static List<string> GetNumberOfPrintJobs()
        {
            LocalPrintServer server = new LocalPrintServer();
            PrintQueueCollection queueCollection = server.GetPrintQueues();
            PrintQueue printQueue = null;

            List<string> listPrinters = new List<string>();

            //string[] printersInstalled = System.Drawing.Printing.PrinterSettings.InstalledPrinters();

            foreach (PrintQueue pq in queueCollection)
            {
                listPrinters.Add(pq.FullName);
                Console.WriteLine(pq.FullName);
               
            }

            return listPrinters;
        }
    }
}
