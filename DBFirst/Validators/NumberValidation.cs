using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DBFirst.Validators
{
    public class NumberValidation:ValidationRule
    {
        public override ValidationResult Validate (object? value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString( ).Trim( );

            foreach (var letter in input)
            {
                if (!char.IsNumber(letter))
                {
                    return new ValidationResult(false, "В строке должы быть только цифры");
                }
            }
            

            return ValidationResult.ValidResult;
        }
    }
}
