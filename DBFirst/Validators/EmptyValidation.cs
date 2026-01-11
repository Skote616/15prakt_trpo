using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DBFirst.Validators
{
    public class EmptyValidation:ValidationRule
    {
        public override ValidationResult Validate (object? value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString( ).Trim( );

            if (input.IsNullOrEmpty( )) return new ValidationResult(false, "Поле пустое");

            return ValidationResult.ValidResult;
        }
    }
}
