using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string dateTimePattern = new CultureInfo("en-GB", false).DateTimeFormat.RFC1123Pattern;

            var t = DateTime.Now.ToString(dateTimePattern);
            var s = DateTime.ParseExact("Fri, 18 Mar 2016 01:52:35 GMT", dateTimePattern, CultureInfo.CurrentCulture, DateTimeStyles.None);
        }
    }
}
