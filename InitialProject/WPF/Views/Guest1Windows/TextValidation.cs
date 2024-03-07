using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.WPF.Views.Guest1Windows
{
    public class TextValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                double r;
                if (double.TryParse(s, out r))
                {
                    return new ValidationResult(false, "Unesite samo slova.");
                }
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occurred.");
            }
        }
    }
}
