using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Util
{
    public class DateHelper
    {
        public static DateTime StringToDate(string date)
        {
            return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public static string DateToString(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }
    }
}
