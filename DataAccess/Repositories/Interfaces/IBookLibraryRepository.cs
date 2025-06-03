using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repositories.Interfaces
{
     public interface IBookLibraryRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<IEnumerable<Book>> FilterBooksAsync(bool? isBorrowed, DateTime? borrowDate, DateTime? returnDate);
        Task<Book?> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task<bool> BorrowBookAsync(int id);
        Task<bool> ReturnBookAsync(int id);
    }
}
