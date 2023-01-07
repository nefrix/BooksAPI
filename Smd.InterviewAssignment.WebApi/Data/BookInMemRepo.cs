using Smd.InterviewAssignment.WebApi.Models;
using System.Collections.Generic;

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
            throw new System.NotImplementedException();
        }

        public Book GetBookByTitle(string title)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            throw new System.NotImplementedException();
        }

        public void AddBook(Book book)
        {
            throw new System.NotImplementedException();
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
    }
}
