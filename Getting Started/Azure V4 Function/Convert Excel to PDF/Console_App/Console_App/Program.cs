﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Console_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileStream excelStream = new FileStream("../../Sample.xlsx", FileMode.Open, FileAccess.Read);
            excelStream.Position = 0;

            //Saves the Excel document in memory stream.
            MemoryStream inputStream = new MemoryStream();
            excelStream.CopyTo(inputStream);
            inputStream.Position = 0;

            try
            {
                Console.WriteLine("Please enter your Azure Functions URL :");
                string functionURL = Console.ReadLine();

                //Create HttpWebRequest with hosted azure function URL
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(functionURL);

                //Set request method as POST
                req.Method = "POST";

                //Get the request stream to strore the Excel document stream
                Stream stream = req.GetRequestStream();

                //Write the Excel document stream into request stream
                stream.Write(inputStream.ToArray(), 0, inputStream.ToArray().Length);

                //Gets the responce from the Azure Function request
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                //Create file stream to save the output PDF file
                FileStream outStream = File.Create("Sample.pdf");

                //Copy the responce stream into file stream
                res.GetResponseStream().CopyTo(outStream);

                //Dispose the input stream
                inputStream.Dispose();

                //Dispose the file stream
                outStream.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
