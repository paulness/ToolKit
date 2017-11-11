using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            int publicationId = 13;
            string theme = "";

            if (!string.IsNullOrEmpty(theme) & SecondOp() &&
                publicationId != 12 && publicationId != 13)
            {
                
            }
        }

        static bool SecondOp()
        {
            Console.WriteLine("I got called");
            return true;
        }
    }
}
