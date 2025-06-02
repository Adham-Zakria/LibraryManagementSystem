using DataAccess.Contexts;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
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
    }
}
