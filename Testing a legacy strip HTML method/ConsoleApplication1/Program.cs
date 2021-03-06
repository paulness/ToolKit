﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = File.ReadAllLines(@"C:\DumpingGround\ll.txt").Select(RemoveInvalidCharacters);
            File.WriteAllLines(@"C:\DumpingGround\ll2.txt", f);

        }

                public static string RemoveInvalidCharacters(string str)
        {
            // Replace invalid characters with empty strings.
            return Regex.Replace(StripHTML(str), @"[^\w-]", "");
        }

         public static string StripHTML(string source)
        {
            string result = "";

            if (!String.IsNullOrEmpty(source))
            {
                // Remove HTML Development formatting

                // Replace line breaks with space
                // because browsers inserts space
                // Replace line breaks with space
                // because browsers inserts space
                result = source.Replace("\r", String.Empty);
                // make line breaking consistent
                result = result.Replace("\n", "\r");
                //result = result.Replace("\n", String.Empty);

                // Remove step-formatting
                result = result.Replace("\t", String.Empty);
                // Remove repeating speces becuase browsers ignore them
                result = Regex.Replace(result, @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = Regex.Replace(result, @"<( )*head([^>])*>", "<head>", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"(<( )*(/)( )*head( )*>)", "</head>", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, "(<head>).*(</head>)", String.Empty, RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = Regex.Replace(result, @"<( )*script([^>])*>", "<script>", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"(<( )*(/)( )*script( )*>)", "</script>", RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result, 
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty, 
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"(<script>).*(</script>)", String.Empty, RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = Regex.Replace(result, @"<( )*style([^>])*>", "<style>", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"(<( )*(/)( )*style( )*>)", "</style>", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, "(<style>).*(</style>)", String.Empty, RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                //result = Regex.Replace(result, @"<( )*td([^>])*>", "\t", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*td([^>])*>", "\r", RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = Regex.Replace(result, @"<( )*br( )*>", "\r", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*br( )*/>", "\r", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*li( )*>", "\r\r", RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = Regex.Replace(result, @"<( )*div([^>])*>", "\r\r", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*tr([^>])*>", "\r\r", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*p([^>])*>", "\r\r", RegexOptions.IgnoreCase);

                result = Regex.Replace(result, @"<( )*h1([^>])*>", "\r", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*h2([^>])*>", "\r", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*h3([^>])*>", "\r", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*h4([^>])*>", "\r", RegexOptions.IgnoreCase);

                result = Regex.Replace(result, "(\r) ", "\r", RegexOptions.IgnoreCase);

                // Remove remaining tags like <a> if any, links, images,
                // comments etc - anything thats enclosed inside < >
                result = Regex.Replace(result, @"<[^>]*>", String.Empty, RegexOptions.IgnoreCase);
                result = Regex.Replace(result, "\">", "\r", RegexOptions.IgnoreCase);
                // replace special characters:
                result = Regex.Replace(result, @"&nbsp;", " ", RegexOptions.IgnoreCase);

                result = Regex.Replace(result, @"&bull;", " * ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&lsaquo;", "<", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&rsaquo;", ">", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&trade;", "(tm)", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&frasl;", "/", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<", "<", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @">", ">", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&copy;", "(c)", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&reg;", "(r)", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&(.{2,6});", String.Empty, RegexOptions.IgnoreCase);

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4. 
                // Prepare first to remove any whitespaces inbetween
                // the escaped characters and remove redundant tabs inbetween linebreaks
                result = Regex.Replace(result, "(\r)( )+(\r)", "\r\r", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, "(\t)( )+(\t)", "\t\t", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, "(\t)( )+(\r)", "\t\r", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, "(\r)( )+(\t)", "\r\t", RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = Regex.Replace(result, "(\r)(\t)+(\r)", "\r\r", RegexOptions.IgnoreCase);
                // Remove multible tabs followind a linebreak with just one tab
                result = Regex.Replace(result, "(\r)(\t)+", "\r\t", RegexOptions.IgnoreCase);
                // Initial replacement target string for linebreaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                //removing any line breaks at the start
                result = result.Trim().TrimStart(new char[] { '\r' });
            }

            // Thats it.
            return result;
        }

    }
}
