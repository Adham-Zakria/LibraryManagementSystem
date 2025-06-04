using DataAccess.Contexts;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Seeding
{
    public static class Seeder
    {
        public static async Task SeedAsync(LibraryDbContext _context)
        {
            try
            {
                if (!_context.Set<Author>().Any())
                {
                    var authorsData = await File.ReadAllTextAsync(@"..\DataAccess\Seeding\authors.json");
                    var authors = JsonSerializer.Deserialize<List<Author>>(authorsData);

                    if (authors is not null && authors.Any())
                        _context.Set<Author>().AddRange(authors);

                    await _context.SaveChangesAsync();
                }

                if (!_context.Set<Book>().Any())
                {
                    var booksData = await File.ReadAllTextAsync(@"..\DataAccess\Seeding\books.json");
                    var books = JsonSerializer.Deserialize<List<Book>>(booksData);

                    if (books is not null && books.Any())
                        _context.Set<Book>().AddRange(books);

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding failed: {ex.Message}");
            }
        }
    }
}
