using DataAccess.Contexts;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IAuthorRepository> _authorRepository;
        private readonly Lazy<IBookRepository> _bookRepository;

        private readonly LibraryDbContext _libraryDbContext;

        public UnitOfWork(LibraryDbContext libraryDbContext )
        {
            _libraryDbContext = libraryDbContext;
            _authorRepository = new Lazy<IAuthorRepository>(() => new AuthorRepository(_libraryDbContext));
            _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(_libraryDbContext));
        }

        public IAuthorRepository AuthorRepository => _authorRepository.Value;
        public IBookRepository BookRepository => _bookRepository.Value;

        public int SaveChanges()
        {
            return _libraryDbContext.SaveChanges();
        }
    }
}
