using Smd.InterviewAssignment.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smd.InterviewAssignment.WebApi.Data
{
    public class BookInMemRepo : IBookRepo
    {
        private readonly List<Book> _books;

        public BookInMemRepo()
        {
            _books = new List<Book>
            {
                new Book { Id = 1, Title = "Moby Dick", Author = "Herman Melville" },
                new Book { Id = 2, Title = "Ulysses", Author = "James Joyce" },
                new Book { Id = 3, Title = "The Great Gatsby", Author = "Fitz" },
                new Book { Id = 4, Title = "War and Peace", Author = "Leo Tolstoy" }
            };
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public Book GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public bool BookExists(string author, string title)
        {
            return _books.Any(b => string.Equals(b.Title, title, System.StringComparison.OrdinalIgnoreCase) && string.Equals(b.Author, author, System.StringComparison.OrdinalIgnoreCase));
        }

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            book.Id = GetNewId();
            _books.Add(book);
        }

        public void DeleteBook(int id)
        {
            throw new System.NotImplementedException();
        }

        public void MarkBookAsRead(int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            throw new System.NotImplementedException();
        }

        private int GetNewId()
        {
            return _books.Max(b => b.Id) + 1;
        }
    }
}
