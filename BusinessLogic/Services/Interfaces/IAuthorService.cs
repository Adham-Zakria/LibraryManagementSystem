using BusinessLogic.DTOs.AuthorDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<AuthorDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateAuthorDto dto);
        Task<bool> UpdateAsync(UpdateAuthorDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
