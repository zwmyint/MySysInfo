using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDateOnlyAndTimeOnly.ConsoleApp
{
    public class MyTimeOnly
    {
        public static void TimeOnlyExample()
        {
            // <SnippetTimeOnly>
            TimeOnly timeOnly = new TimeOnly(12, 30, 45);
            Console.WriteLine($"TimeOnly Sample :  {timeOnly}");
            // </SnippetTimeOnly>
        }

        public static void TimeOnlyFromDateTimeExample()
        {
            // <SnippetTimeOnlyFromDateTime>

            var timeOnly = TimeOnly.FromDateTime(DateTime.Now);
            Console.WriteLine($"TimeOnly From DateTime :  {timeOnly}");
            // </SnippetTimeOnlyFromDateTime>
        }

        public static void AddHoursExample()
        {
            // <SnippetAddHours>
            TimeOnly timeOnly = new TimeOnly(12, 30, 45);
            timeOnly = timeOnly.AddHours(1);
            Console.WriteLine($"TimeOnly.AddHours :  {timeOnly}");
            // </SnippetAddHours>
        }

        public static void AddMinutesExample()
        {
            // <SnippetAddMinutes>
            TimeOnly timeOnly = new TimeOnly(12, 30, 45);
            timeOnly = timeOnly.AddMinutes(1);
            Console.WriteLine($"TimeOnly.AddMinutes :  {timeOnly}");
            // </SnippetAddMinutes>
        }
        public static void AddSecondsExample()
        {
            // <SnippetAddSeconds>
            TimeOnly timeOnly = new TimeOnly(12, 30, 45);
            timeOnly = timeOnly.Add(TimeSpan.FromSeconds(10));
            Console.WriteLine($"TimeOnly.AddSeconds :  {timeOnly}");
            // </SnippetAddSeconds>
        }
    }
}
