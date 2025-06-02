using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public enum Genre
    {
        Unknown,
        Adventure,
        Mystery,
        Thriller,
        Romance,
        SciFi,
        Fantasy,
        Biography,
        History,
        SelfHelp,
        Children,
        YoungAdult,
        Poetry,
        Drama,
        NonFiction
    }
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public string? Description { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public DateTime? BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public bool IsBorrowed => BorrowedDate.HasValue && ReturnedDate == null;

    }
}
