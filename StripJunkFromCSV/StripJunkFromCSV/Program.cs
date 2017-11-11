using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace StripJunkFromCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            //CsvWriter csvWriter = new CsvWriter(new StreamWriter(File.OpenWrite(@"C:\HMI\Visitor_Sub_Ref-Clean.csv")), new CsvConfiguration()
            //{
            //    QuoteAllFields = true,
            //});
            CsvReader csvReader = new CsvReader(new StreamReader(File.OpenRead(@"C:\HMI\Visitor_Sub_Ref.csv")));

            //csvWriter.WriteField("Visitor_ID");
            //csvWriter.WriteField(@"Subscriber ID (evar55)");
            //csvWriter.NextRecord();

            int rowCount = 0;
            while (csvReader.Read())
            {
                var subscriberId = csvReader.GetField<int?>("Subscriber ID (evar55)");
                var visitorId = csvReader.GetField<string>("Visitor_ID");

                if (subscriberId.HasValue && !string.IsNullOrWhiteSpace(visitorId))
                {
                    rowCount++;
                    Console.WriteLine(rowCount);
                    //csvWriter.WriteField(visitorId);
                    //csvWriter.WriteField(subscriberId);
                    //csvWriter.NextRecord();
                }
            }

            
        }
    }
}
