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
    public class AuthorRepository(LibraryDbContext libraryDbContext) 
        : GenericRepository<Author>(libraryDbContext), IAuthorRepository
    {
        private readonly LibraryDbContext _libraryDbContext = libraryDbContext;
    }
}
