using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bookcase.Validators;

namespace Bookcase.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [Name]
        public string Name { get; set; }

        [StringLength(250)]
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; }

        public ICollection<Book> Books { get; set; }

    }
}
