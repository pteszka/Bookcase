using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bookcase.Validators;

namespace Bookcase.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [PhoneNumber]
        public string Phone { get; set; }

        [StringLength(50, MinimumLength = 4)]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Book> Books { get; set; }

    }
}
