using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.BookDtos
{
    public class UpdateBookDto : CreateBookDto
    {
        [Required]
        public int Id { get; set; }
    }
}
