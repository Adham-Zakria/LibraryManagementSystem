using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Author
    {
        public int Id { get; set; }

        [RegularExpression(@"^(\w{2,}\s){3}\w{2,}$", ErrorMessage = "Full name must consist of four parts with at least 2 characters each.")]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string? Website { get; set; }
        public string? Bio { get; set; }
        public List<Book> Books { get; set; } = new();
    }
}
