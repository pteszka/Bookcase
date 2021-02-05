using Bookcase.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookcase.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Author")]
        public int AuthorID { get; set; }

        [Display(Name = "Genre")]
        public int GenreID { get; set; }

        [Display(Name = "Publisher")]
        public int PublisherID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string Title { get; set; }

        [Display(Name = "Year of publication")]
        [AuthorIsAtLeast13]
        public int PubYear { get; set; }

        [Range(1, 7500, ErrorMessage = "This number is outside the range 1 to 7500")]
        [Display(Name = "Pages")]
        public int numberOfPages { get; set; }

        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public Publisher Publisher { get; set; }
    }
}
