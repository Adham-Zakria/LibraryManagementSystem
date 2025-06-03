using BusinessLogic.DTOs.BookDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IBookLibraryService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<IEnumerable<BookDto>> FilterBooksAsync(bool? isBorrowed, DateTime? borrowDate, DateTime? returnDate);
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<bool> BorrowBookAsync(int id);
        Task<bool> ReturnBookAsync(int id);
    }
}
