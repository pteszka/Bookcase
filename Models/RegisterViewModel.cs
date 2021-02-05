using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookcase.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Please enter a value in the range between 3 and 20 characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Please enter a value in the range between 6 and 20 characters", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
