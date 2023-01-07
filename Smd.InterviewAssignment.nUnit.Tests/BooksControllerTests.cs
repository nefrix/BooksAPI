using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using Smd.InterviewAssignment.WebApi.Controllers;
using Smd.InterviewAssignment.WebApi.Data;
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
    }
}