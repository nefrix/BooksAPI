using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using Smd.InterviewAssignment.WebApi.Controllers;
using Smd.InterviewAssignment.WebApi.Data;

namespace Smd.InterviewAssignment.nUnit.Tests
{
    public class BooksControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Get_NotEmpty()
        {
            IBookRepo bookRepo = new BookInMemRepo();
            ILogger<BooksController> logger = new NullLogger<BooksController>();
            var sut = new BooksController(logger, bookRepo);

            var items = sut.Get();

            Assert.That(items, Is.Not.Empty);
        }
    }
}