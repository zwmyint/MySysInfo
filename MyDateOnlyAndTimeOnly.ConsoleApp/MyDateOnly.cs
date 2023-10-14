using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDateOnlyAndTimeOnly.ConsoleApp
{
    public class MyDateOnly
    {
        public static void DateOnlyExample()
        {
            // <SnippetDateOnly>
            DateOnly dateOnly = new DateOnly(2020, 12, 25);
            Console.WriteLine($"DateOnly Sample :  {dateOnly}");
            // </SnippetDateOnly>
        }

        public static void DateOnlyFromDateTimeExample()
        {
            // <SnippetDateOnlyFromDateTime>

            var dateOnly = DateOnly.FromDateTime(DateTime.Now);
            Console.WriteLine($"DateOnly From DateTime :  {dateOnly}");
            // </SnippetDateOnlyFromDateTime>
        }

        public static void AddDaysExample()
        {
            // <SnippetAddDays>
            DateOnly dateOnly = new DateOnly(2020, 12, 25);
            dateOnly = dateOnly.AddDays(1);
            Console.WriteLine($"DateOnly.AddDays :  {dateOnly}");
            // </SnippetAddDays>
        }
        public static void AddMonthsExample()
        {
            // <SnippetAddMonths>
            DateOnly dateOnly = new DateOnly(2020, 12, 25);
            dateOnly = dateOnly.AddMonths(1);
            Console.WriteLine($"DateOnly.AddMonths :  {dateOnly}");
            // </SnippetAddMonths>
        }

        public static void AddYearsExample()
        {
            // <SnippetAddYears>
            DateOnly dateOnly = new DateOnly(2020, 12, 25);
            dateOnly = dateOnly.AddYears(1);
            Console.WriteLine($"DateOnly.AddYears :  {dateOnly}");
            // </SnippetAddYears>
        }
    }
}
