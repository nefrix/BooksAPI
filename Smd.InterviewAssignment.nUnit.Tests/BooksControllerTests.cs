using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using Smd.InterviewAssignment.WebApi.Controllers;
using Smd.InterviewAssignment.WebApi.Data;
using Smd.InterviewAssignment.WebApi.Models;
using Smd.InterviewAssignment.WebApi.Services;
using System.Net;
using System.Threading.Tasks;

namespace Smd.InterviewAssignment.nUnit.Tests
{
    public class BooksControllerTests
    {
        private ILogger<BooksController> _logger;
        private IEmailService _emailService;

        [SetUp]
        public void Setup()
        {
            _logger = new NullLogger<BooksController>();
            _emailService = A.Fake<IEmailService>();
        }

        [Test]
        public void GetBooks_ShouldReturnOkResult()
        {
            // in production I'd mock the repository - no need to do that for the in-mem book repo 
            BooksController sut = new BooksController(_logger, new BookInMemRepo(), _emailService);

            var result = sut.GetBooks();

            Assert.That(result.Result, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public void GetBooks_ResultShouldNotBeEmpty()
        {
            BooksController sut = new BooksController(_logger, new BookInMemRepo(), _emailService);

            var result = sut.GetBooks().Result as OkObjectResult;

            Assert.That(result.Value, Is.Not.Empty);
        }

        [Test]
        [TestCase(1)]
        public void GetBookById_ShouldReturnOkResult(int id)
        {
            BooksController sut = new BooksController(_logger, new BookInMemRepo(), _emailService);

            var result = sut.GetBookByID(id);

            Assert.That(result.Result, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        [TestCase(1)]
        public void GetBookById_ShouldReturnProperBook(int id)
        {
            BooksController sut = new BooksController(_logger, new BookInMemRepo(), _emailService);

            var result = sut.GetBookByID(id).Result as OkObjectResult;
            var book = result.Value;

            Assert.That(book, Is.TypeOf(typeof(Book)));
            Assert.That((book as Book).Id, Is.EqualTo(id));
        }

        [Test]
        public void CreateBook_ShouldReturnCreatedResult()
        {
            BooksController sut = new BooksController(_logger, new BookInMemRepo(), _emailService);

            Book book = new Book() { Author = "George R.R. Martin", Title = "Game of Thrones" };

            var result = sut.CreateBook(book);

            Assert.That(result.Result, Is.TypeOf(typeof(CreatedAtRouteResult)));
        }

        [Test]
        public void UpdateBook_ShouldReturnOkResult()
        {
            BooksController sut = new BooksController(_logger, new BookInMemRepo(), _emailService);

            Book book = new Book() { Id = 3, Author = "Francis Scott Fitzgerald", Title = "The Great Gatsby" };

            var result = sut.UpdateBook(book);

            Assert.That(result.Result, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        [TestCase(1)]
        public void DeleteBook_ShouldReturnOkResult(int id)
        {
            BooksController sut = new BooksController(_logger, new BookInMemRepo(), _emailService);
  
            var result = sut.DeleteBook(id);

            Assert.That(result.Result, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        [TestCase(1)]
        public void MarkBookAsRead_ShouldReturnOkResult(int id)
        {
            BooksController sut = new BooksController(_logger, new BookInMemRepo(), _emailService);

            var result = sut.MarkBookAsRead(id);

            Assert.That(result.Result, Is.TypeOf(typeof(OkObjectResult)));
        }

        [Test]
        public async Task Mail_ShouldReturnOkResult()
        {
            BooksController sut = new BooksController(_logger, new BookInMemRepo(), _emailService);

            var result = await sut.Mail("test@test.com");

            Assert.That(result, Is.TypeOf(typeof(OkObjectResult)));
        }
    }
}