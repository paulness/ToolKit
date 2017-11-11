using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CsvHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace RentStablizedMurrayHill
{
    public class RentalDetails
    {
        public string OriginalAddress { get; set; }

        public string ChosenAddress { get; set; }

        public string Url { get; set; }

        public string Owner { get; set; }

        public string AveragePricingInfo { get; set; }

        public string ActiveListingsUrl { get; set; }

        public string PastSalesUrl { get; set; }

        public string PastRentalUrl { get; set; }

        public int UnitsAvaliableRightNow { get; set; }
    }

    class Program
    {
        private static IWebDriver driver;

        static void Main(string[] args)
        {
            var addresses = GetAddresses();

            int skip = 200;
            int valueToProcessAtATime = 100;

            List<string> addressesToProcess = null;
            while ((addressesToProcess = addresses.Skip(skip).Take(valueToProcessAtATime).ToList()).Count > 0)
            {
                driver = new ChromeDriver();
                var fullAddressInfo = addressesToProcess.Select(GetDetailsForBuilding).ToList();
                SaveToCsv(fullAddressInfo, skip);
                skip += valueToProcessAtATime;
                driver.Close();
                driver.Quit();
            }

            
        }

        static List<string> GetAddresses()
        {
            List<string> results = new List<string>();
            using (CsvReader csvHelper = new CsvReader(
                new StreamReader(File.OpenRead(@"C:\HMI\Rent Stablized Apartments\all midtown east.csv"))))
            {
                while (csvHelper.Read())
                {
                    var blg = csvHelper.GetField(1);
                    var multipleBlg = blg.Split(new[] { " TO " }, StringSplitOptions.RemoveEmptyEntries);

                    if (multipleBlg.Length > 1)
                    {
                        for (int i = Convert.ToInt32(multipleBlg[0]); i <= Convert.ToInt32(multipleBlg[1]); i++)
                        {
                            string address = $"{i} {csvHelper.GetField(2)} {csvHelper.GetField(3)} Manhattan";
                            results.Add(address);
                        }
                    }
                    else
                    {
                        string address = $"{csvHelper.GetField(1)} {csvHelper.GetField(2)} {csvHelper.GetField(3)} Manhattan";
                        results.Add(address);
                    }
                }
            }
            return results;
        }

        static void SaveToCsv(List<RentalDetails> rentalDetails, int skip)
        {
            using (var sw = new StreamWriter(File.Create($@"C:\HMI\Rent Stablized Apartments\all midtown east {skip}.csv")))
            {
                sw.AutoFlush = true;
                //write all the rows to csv
                using (CsvWriter csvWriter = new CsvWriter(sw))
                {
                    csvWriter.WriteHeader<RentalDetails>();
                    foreach (var rental in rentalDetails)
                    {
                        csvWriter.WriteRecord(rental);
                    }
                }
            }
        }

        static RentalDetails GetDetailsForBuilding(string address)
        {
            try
            {
                RentalDetails rentalDetails = new RentalDetails()
                {
                    OriginalAddress = address
                };

                driver.Url = @"http://streeteasy.com/search?utf8=%E2%9C%93&search=" + address;
                WaitForPageLoad(driver);

                var captchaIsPresent = driver.FindElements(By.Id("dCF_input_complete")).Count == 1;
                if (captchaIsPresent)
                {
                    Debugger.Break();
                    Console.WriteLine("Captcha is present. Debugging break point now.");
                }

                var searchResults = driver.FindElements(By.CssSelector(".search-results .line-result a"));
                if (searchResults.Count > 0)
                {
                    string clickedOn = searchResults[0].Text;
                    rentalDetails.ChosenAddress = clickedOn;

                    driver.Url = searchResults[0].GetAttribute("href");
                    WaitForPageLoad(driver);
                }

                rentalDetails.Url = driver.Url;

                var detailTableRows = driver.FindElements(By.CssSelector(".clean_table.legible tr"));
                var averagePrice = detailTableRows.FirstOrDefault(e => e.Text.Contains("avg price"));
                var ownerElem = detailTableRows.FirstOrDefault(e => e.Text.StartsWith("Owned"));

                if (ownerElem != null)
                    rentalDetails.Owner = ownerElem.Text;

                if (averagePrice != null)
                    rentalDetails.AveragePricingInfo = averagePrice.Text;

                var additionalContentTabContainers = driver.FindElements(By.ClassName("tabset-content"));

                foreach (var a in additionalContentTabContainers)
                {
                    string url = a.GetAttribute("se:url");

                    if (url == null)
                        continue;

                    if (url.Contains("&show_sales=true"))
                        rentalDetails.PastSalesUrl = url;

                    if (url.Contains("&show_rentals=true"))
                        rentalDetails.PastRentalUrl = url;
                }

                ////click on past sales
                //driver.FindElement(By.XPath(@"//*[@id=""building-tabs""]/div[2]")).Click();

                //ensure they arent renting anything
                var currentRentals = driver.FindElements(By.CssSelector(@".nice_table.building-pages tbody tr"));
                rentalDetails.UnitsAvaliableRightNow = currentRentals.Count;

                ////*[@id="past_transactions_table"]
                //var activeRentals = driver.FindElements(By.CssSelector(@".nice_table.building-pages tbody tr"));
                //if (activeRentals.Count > 0)
                //{
                //    foreach (var rental in activeRentals)
                //    {
                //        var price = rental.FindElements(By.CssSelector(".price"));
                //        var linkToRental = rental.FindElements(By.CssSelector(".address a"));
                //        if (linkToRental.Count > 0)
                //        {
                //            rentalDetails.Units.Add(new UnitAvail()
                //            {
                //                Price = Convert.ToDecimal(price.Last().Text.Replace("$", "").Replace(",", "").Trim()),
                //                AvaliableUnitUrl = linkToRental[0].GetAttribute("href"),
                //                AvaliableUnitDetails = rental.Text
                //            });
                //        }
                //    }
                //}

                Console.WriteLine("Done " + address);

                return rentalDetails;
            }
            catch
            {
                driver.Close();
                driver.Quit();
                driver = new ChromeDriver();
                return GetDetailsForBuilding(address);
            }
        }

        static void WaitForPageLoad(IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 5);
        }
    }
}
