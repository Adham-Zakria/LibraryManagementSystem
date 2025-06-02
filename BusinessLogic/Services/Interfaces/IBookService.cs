using BusinessLogic.DTOs.BookDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateBookDto dto);
        Task<bool> UpdateAsync(UpdateBookDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
