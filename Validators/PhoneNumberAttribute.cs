using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bookcase.Validators
{
    public class PhoneNumberAttribute : ValidationAttribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            Regex regex = new Regex(@"^[0-9]{9}$");
            if (!regex.IsMatch((string)context.Model))
            {
                return new List<ModelValidationResult> {
                    new ModelValidationResult("", "Phone number must contains only 9 numbers")
                };
            }
            return Enumerable.Empty<ModelValidationResult>();

        }
    }
}
