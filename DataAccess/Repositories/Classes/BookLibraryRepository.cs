using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Classes
{
    public class BookLibraryRepository(IConnectionMultiplexer _connection) : IBookLibraryRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();
        private readonly string keyList = "books:all";

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            var ids = await _database.ListRangeAsync(keyList);
            var books = new List<Book>();

            foreach (var id in ids)
            {
                var json = await _database.StringGetAsync($"book:{id}");
                if (!json.IsNullOrEmpty)
                    books.Add(JsonSerializer.Deserialize<Book>(json!)!);
            }

            return books;
        }

        public async Task<IEnumerable<Book>> FilterBooksAsync(bool? isBorrowed, DateTime? borrowDate, DateTime? returnDate)
        {
            var allBooks = await GetAllBooksAsync();

            return allBooks
                .Where(b =>
                    (!isBorrowed.HasValue || b.IsBorrowed == isBorrowed) &&
                    (!borrowDate.HasValue || b.BorrowedDate?.Date == borrowDate.Value.Date) &&
                    (!returnDate.HasValue || b.ReturnedDate?.Date == returnDate.Value.Date)
                )
                .ToList();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            var json = await _database.StringGetAsync($"book:{id}");
            return string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<Book>(json!);
        }

        public async Task AddBookAsync(Book book)
        {
            var json = JsonSerializer.Serialize(book);
            await _database.StringSetAsync($"book:{book.Id}", json);

            // Workaround: manually check if the ID exists in the Redis list
            var bookIds = await _database.ListRangeAsync("books:all");
            bool alreadyExists = bookIds.Any(x => x.ToString() == book.Id.ToString());

            if (!alreadyExists)
            {
                await _database.ListRightPushAsync("books:all", book.Id);
            }
        }

        public async Task<bool> BorrowBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if (book == null || book.IsBorrowed) return false;

            book.BorrowedDate = DateTime.UtcNow;
            book.ReturnedDate = null;

            await SaveBookAsync(book);
            return true;
        }

        public async Task<bool> ReturnBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if (book == null || !book.IsBorrowed) return false;

            book.ReturnedDate = DateTime.UtcNow;

            await SaveBookAsync(book);
            return true;
        }

        private async Task SaveBookAsync(Book book)
        {
            var json = JsonSerializer.Serialize(book);
            await _database.StringSetAsync($"book:{book.Id}", json);

            // Manual check to avoid Redis LPOS
            var allIds = await _database.ListRangeAsync("books:all");
            if (!allIds.Any(x => x.ToString() == book.Id.ToString()))
            {
                await _database.ListRightPushAsync("books:all", book.Id);
            }
        }
    }
}   
