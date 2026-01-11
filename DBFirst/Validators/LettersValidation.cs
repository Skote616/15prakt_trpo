using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DBFirst.Validators
{
    public class LettersValidation:ValidationRule
    {
        public override ValidationResult Validate (object? value, CultureInfo cultureInfo)
        {
            var input = (value ?? "").ToString( ).Trim( );

            foreach (var letter in input)
            {
               if( !char.IsLetter(letter) )
                {
                    return new ValidationResult(false, "В строке должны быть только буквы");
                }
            }


            return ValidationResult.ValidResult;
        }
    }
}
