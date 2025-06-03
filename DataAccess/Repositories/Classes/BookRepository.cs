using DataAccess.Contexts;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Classes
{
    public class BookRepository(LibraryDbContext libraryDbContext) 
        : GenericRepository<Book>(libraryDbContext), IBookRepository
    {
        private readonly LibraryDbContext _libraryDbContext = libraryDbContext;

        public override async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _libraryDbContext.Books.Include(b => b.Author).ToListAsync();   // override for Eager loading
        }
    }
}
