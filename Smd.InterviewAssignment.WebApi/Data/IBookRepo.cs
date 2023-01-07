using Smd.InterviewAssignment.WebApi.Models;
using System.Collections;
using System.Collections.Generic;

namespace Smd.InterviewAssignment.WebApi.Data
{
    public interface IBookRepo
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        bool BookExists(int id);
        bool BookExists(string author, string title);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
        void MarkBookAsRead(int id);
    }
}
