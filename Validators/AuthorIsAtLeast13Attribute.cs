using Bookcase.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bookcase.Controllers;
using Bookcase.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookcase.Validators
{

    public class AuthorIsAtLeast13Attribute : ValidationAttribute
    {
        public string GetErrorMessage() =>
            $"Incorrect release date. The Author must be at least 13 years old to write a book";

        // author of the book must be at least 13 years old to write book
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _context = (BookcaseContext)validationContext
                         .GetService(typeof(BookcaseContext));
            var BookModel = (Book)validationContext.ObjectInstance;
            var authorId = BookModel.AuthorID;
            var authorBirthDate = FindAuthorBirthDate(authorId, _context).Result;
            var _pubYear = (int)value;
            var pubDate = new DateTime(_pubYear, authorBirthDate.Month, authorBirthDate.Day);

            if (pubDate < authorBirthDate.AddYears(13))
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }

        private async Task<DateTime> FindAuthorBirthDate(int _id, BookcaseContext _context)
        {
            var result = await _context.Author.FirstOrDefaultAsync(a => a.Id == _id);
            return result.DateOfBirth;
        }
    }
    
}
