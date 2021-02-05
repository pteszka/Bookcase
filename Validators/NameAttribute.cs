using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bookcase.Validators
{
    public class NameAttribute : ValidationAttribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            Regex regex = new Regex(@"^[A-Z\p{Lu}][a-z\p{Ll}]{1,50}$");
            if (!regex.IsMatch((string)context.Model))
            {
                return new List<ModelValidationResult> {
                    new ModelValidationResult("", "Name/Surname should consist of 2 to 50 letters and start with a capital letter")
                };
            }
            return Enumerable.Empty<ModelValidationResult>();

        }
    }
}
