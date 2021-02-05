using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookcase.Validators
{
    public class AnnoDominiAttribute : ValidationAttribute
    {
        public string GetErrorMessage() =>
            $"Incorrect date of birth. Please enter the correct date of birth between AD 100 and today";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _birthday = (DateTime)value;
            if (_birthday > DateTime.Now || _birthday < new DateTime(100,1,1))
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }

    }
}

