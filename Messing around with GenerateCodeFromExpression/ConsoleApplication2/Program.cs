using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        public enum RightColumnType : int
        {
            AddAsset = 0,
            MostArticles = 1,

        }
        static void Main(string[] args)
        {
            List<string> names = new List<string> {"paul", "ness"};
            names.Insert(3, "ll");

            string s= Enum.GetName(typeof (RightColumnType), 41);

            int publicationId = 34;
            if (publicationId != 10 || publicationId != 34)
            {
                //MYCME-1502 - prevent exit survey on all medical sites except Mims/MyCME
                return;
            }


            var r1 = decimal.Divide(0, 5);
            var r = ToLiteral(File.ReadAllText("C:/HMI/dasd.txt"));
        }
        private static string ToLiteral(string input)
        {
            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
                    return writer.ToString();
                }
            }
        }
    }
}
