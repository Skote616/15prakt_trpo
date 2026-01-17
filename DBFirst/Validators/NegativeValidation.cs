using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DBFirst.Validators
{
    class NegativeValidation :ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString().Trim();

            if (int.Parse(input) < 0)
            {
                return new ValidationResult(false, "Число не может быть отрицательным");
            }
            else
                return ValidationResult.ValidResult;
        }
    }
}
