using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;

namespace ValidateDate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] dates = new string[]
                {
                    
                    "13/Jul/1928",
                    "10-Nov-1934",
                    "01/Jan-1951",
                    "25.Dec.1937",
                    "23#09#1973",
                    "1/Feb/2016",
                    
                    "01/Jan-1951",
                    "29/Feb/2024",
                    "1/Jan-1951",
                    "27-Feb-2007",
                    "1/Jan-1951",
                    "1/Mar/2016",
                    "23/october/197"
                    
                   
                };
            var k = new DateValidation();
            foreach (var i in dates)
            {
                string x = "";
                bool ok = false;
                k.IsValid(i,ref ok,ref x);
                if (ok)
                {
                    Console.WriteLine(x);
                }
                else
                {
                    //Console.WriteLine($"{i} = > false");
                }
            }
        }
    }

    public class DateValidation
    {

        //************************************************************
        private bool Xxx(string input)
        {
            bool result =
                Char.IsUpper(input[0])
                &&
                Char.IsLower(input[1])
                &&
                Char.IsLower(input[2])
                ;
            return result;
        }
        //************************************************************
        public void IsValid(string input,ref bool ok,ref string print)
        {
            string[] parts = this.Parts(input);

            char sep1 = input[parts[0].Length];
            char sep2 = input[parts[0].Length + parts[1].Length + 1];

            ok =false;
            print = "";
            if (sep1 == sep2 && Array.IndexOf<char>(this.separator(), sep1) > -1)
            {
                if (parts[0].Length == 2 && parts[2].Length == 4 && parts[1].Length == 3)
                {
                    if (Xxx(parts[1]))
                    {
                        ok = true;
                        print = $"Day: {parts[0]}, Month: {parts[1]}, Year: {parts[2]}";
                    }
                }
            }
            else
            {
                ok = false;
                print = "";
            }



           
        }
        //************************************************************
        private char[] separator()
        {
            return new char[] { '.', '-', '/' };
        }
        //************************************************************
        private string YearPart(string input, string day_part, string month_part)
        {
            int index = day_part.Length + month_part.Length + 2;
            StringBuilder sb = new StringBuilder();
            while (index <= input.Length-1)
            {
                sb.Append(input[index]);
                index++;
            }
            return sb.ToString();
        }


        //************************************************************
        private string ExtractMonthPart(string input, string day_part)
        {
            int index = day_part.Length+1;
            StringBuilder sb = new StringBuilder();
            bool flag = true;
            while (index <= input.Length - 1 && flag)
            {
                flag = Char.IsLetter(input[index]);
                if (flag)
                {
                    sb.Append(input[index]);
                }
                index++;
            }
            return sb.ToString();
        }
        //************************************************************
        private string ExtractDayPart(string input)
        {
            int index = 0;
            bool flag = true;

            StringBuilder sb = new StringBuilder();
            while (flag && index <= input.Length - 1)
            {
                flag = Char.IsDigit(input[index]);
                if (flag)
                {
                    sb.Append(input[index]);
                }

                index++;
            }
            return sb.ToString();
        }
        //************************************************************
        private string[] Parts(string input)
        {
            string[] result = new string[3];

          

            result[0] = this.ExtractDayPart(input);
            result[1] = this.ExtractMonthPart(input, result[0]);
            result[2] = this.YearPart(input, result[0], result[1]);
           

            return result;
        }
        //************************************************************
    }
}
