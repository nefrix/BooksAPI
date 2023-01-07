using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using Smd.InterviewAssignment.WebApi.Controllers;
using Smd.InterviewAssignment.WebApi.Data;
using Smd.InterviewAssignment.WebApi.Models;
using System.Net;

namespace Smd.InterviewAssignment.nUnit.Tests
{
    public class BooksControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetBooks_ShouldReturnOkResult()
        {
            BooksController sut = new BooksController(new NullLogger<BooksController>(), new BookInMemRepo());

            var result = sut.GetBooks();

            Assert.That(result.Result, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public void GetBooks_ResultShouldNotBeEmpty()
        {
            BooksController sut = new BooksController(new NullLogger<BooksController>(), new BookInMemRepo());

            var result = sut.GetBooks().Result as OkObjectResult;

            Assert.That(result.Value, Is.Not.Empty);
        }

        [Test]
        [TestCase(1)]
        public void GetBookById_ShouldReturnOkResult(int id)
        {
            BooksController sut = new BooksController(new NullLogger<BooksController>(), new BookInMemRepo());

            var result = sut.GetBookByID(id);

            Assert.That(result.Result, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        [TestCase(1)]
        public void GetBookById_ShouldReturnProperBook(int id)
        {
            BooksController sut = new BooksController(new NullLogger<BooksController>(), new BookInMemRepo());

            var result = sut.GetBookByID(id).Result as OkObjectResult;
            var book = result.Value;

            Assert.That(book, Is.TypeOf(typeof(Book)));
            Assert.That((book as Book).Id, Is.EqualTo(id));
        }

        [Test]
        public void CreateBook_ShouldReturnCreatedResult()
        {
            BooksController sut = new BooksController(new NullLogger<BooksController>(), new BookInMemRepo());

            Book book = new Book() { Author = "George R.R. Martin", Title = "Game of Thrones" };

            var result = sut.CreateBook(book);

            Assert.That(result.Result, Is.TypeOf(typeof(CreatedAtRouteResult)));
        }

        [Test]
        public void UpdateBook_ShouldReturnCreatedResult()
        {
            BooksController sut = new BooksController(new NullLogger<BooksController>(), new BookInMemRepo());

            Book book = new Book() { Id = 3, Author = "Francis Scott Fitzgerald", Title = "The Great Gatsby" };

            var result = sut.UpdateBook(book);

            Assert.That(result.Result, Is.TypeOf(typeof(CreatedAtRouteResult)));
        }

        [Test]
        [TestCase(1)]
        public void DeleteBook_ShouldReturnOkResult(int id)
        {
            BooksController sut = new BooksController(new NullLogger<BooksController>(), new BookInMemRepo());
  
            var result = sut.DeleteBook(id);

            Assert.That(result.Result, Is.TypeOf(typeof(OkResult)));
        }
    }
}