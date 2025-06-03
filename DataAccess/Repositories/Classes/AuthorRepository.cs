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
    public class AuthorRepository(LibraryDbContext libraryDbContext) 
        : GenericRepository<Author>(libraryDbContext), IAuthorRepository
    {
        private readonly LibraryDbContext _libraryDbContext = libraryDbContext;

        public override async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _libraryDbContext.Authors.Include(a => a.Books).ToListAsync();
        }
    }
}
