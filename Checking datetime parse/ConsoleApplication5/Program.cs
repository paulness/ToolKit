using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Aes_Example
{
    class AesExample
    {
        public static void Main()
        {
            var t = GetDateTime("2017-01-31");
        }

        public static DateTime? GetDateTime(string key, DateTime? defaultValue = null)
        {
            DateTime? returnValue = defaultValue;
            DateTime dateValue;
            try
            {
                if (DateTime.TryParse(key, out dateValue))
                    returnValue = dateValue;
            }
            catch
            { }

            return returnValue;
        }
    }
}