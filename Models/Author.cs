using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bookcase.Validators;

namespace Bookcase.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [Name]
        public string Name { get; set; }

        [Required]
        [Name]
        public string Surname { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [AnnoDomini]
        public DateTime DateOfBirth { get; set; }

        public ICollection<Book> Books { get; set; }

    }
}
