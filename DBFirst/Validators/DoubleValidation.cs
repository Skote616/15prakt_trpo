using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DBFirst.Validators
{
    public class DoubleValidation: ValidationRule
    {
        public bool AllowIntermediate { get; set; } = true;
        public override ValidationResult Validate (object? value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString( ).Trim( );

            input = input.Replace('.', ',');

            if (!Double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
            {
                return new ValidationResult(false, "Должно быть десятичное число");
            }
            else
                if(Convert.ToDouble(input)<0)
                {
                  return new ValidationResult(false, "Число не может быть отрицательным");
                }
                else
                    return ValidationResult.ValidResult;
            
        }
    }
}
