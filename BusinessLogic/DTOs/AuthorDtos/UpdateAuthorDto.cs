using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.AuthorDtos
{
    public class UpdateAuthorDto : CreateAuthorDto
    {
        [Required]
        public int Id { get; set; }
    }
}
