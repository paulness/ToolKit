using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GmailExtract
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(File.OpenRead("C:\\HMI\\mongo issue.mbox"), Encoding.UTF8);

            StringBuilder csv = new StringBuilder();
            bool ignoreLine = true;
            StringBuilder sb = new StringBuilder();
            int count = 0;
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();



                if (Regex.IsMatch(line, "From [0-9]*@xxx"))
                {
                    string output = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(sb.ToString()));

                    string emailPattern =
                        @"&quot;(?<email>[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)&quot;";
                    string activityIdPattern = @"&quot;ActivityId&quot; \: (?<activityId>[0-9]*)";

                    string activityId = Regex.Match(output, activityIdPattern, RegexOptions.IgnoreCase).Groups["activityId"].Value;
                    string email = Regex.Match(output, emailPattern, RegexOptions.IgnoreCase).Groups["email"].Value;

                    if (output.Contains("SaveSubscriberSelfAssessmentDo"))
                    {
                        count++;
                        csv.AppendLine(email + "," + activityId);
                    }
                    else
                    {
                        
                    }
                    
                    sb.Clear();
                    ignoreLine = true;
                }

                if (!ignoreLine)
                {
                    sb.AppendLine(line);
                }

                if (line.Contains("<https://groups.google.com/a/haymarket.com/group/cmserrors/subscribe>"))
                {
                    ignoreLine = false;
                }



            }


            //Console.Write(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(read)));
            File.WriteAllText("C:\\HMI\\mongo issue csv.csv", csv.ToString());

        }
    }
}
