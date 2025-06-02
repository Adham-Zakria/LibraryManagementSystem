using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.BookDtos
{
    public class CreateBookDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}
