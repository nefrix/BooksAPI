using System.Collections.Generic;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Smd.InterviewAssignment.WebApi.Data;
using Smd.InterviewAssignment.WebApi.Models;

namespace Smd.InterviewAssignment.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookRepo _bookRepo;

        public BooksController(ILogger<BooksController> logger, IBookRepo bookRepo)
        {
            _logger = logger;
            _bookRepo = bookRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            _logger.LogInformation("--> Hit GetBooks");

            return Ok(_bookRepo.GetAllBooks());
        }

        [HttpGet]
        [Route("{id}", Name = "GetBookByID")]
        public ActionResult<Book> GetBookByID(int id)
        {
            _logger.LogInformation("--> Hit GetBookByID: {id}", id);

            Book book = _bookRepo.GetBookById(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            // the method takes in whole Book object - in production I'd create BookCreateDTO without Id field

            _logger.LogInformation("--> Hit CreateBook - title: {title}, author: {author}", book.Title, book.Author);

            if (_bookRepo.BookExists(book.Author, book.Title))
                return BadRequest("Book with provided author and title already exists!");

            _bookRepo.AddBook(book);

            _logger.LogInformation("----> Book created with id: {id}", book.Id);

            return CreatedAtRoute(nameof(GetBookByID), new { book.Id }, book);
        }

        [HttpPut]
        public ActionResult<Book> UpdateBook(Book book)
        {
            // in this case only full update is possible - requiring all required fields (author, title)
            // in production I'd create a class BookUpdateDTO which wouldn't require all the fields

            _logger.LogInformation("--> Hit UpdateBook - id: {id}, title: {title}, author: {author}", book.Id, book.Title, book.Author);

            if (!_bookRepo.BookExists(book.Id))
                return BadRequest("Book with provided ID doesn't exist!");

            _bookRepo.UpdateBook(book);

            _logger.LogInformation("----> Book updated");

            return CreatedAtRoute(nameof(GetBookByID), new { book.Id }, book);
        }

        [HttpGet]
        [Route("mail")]
        public void Mail(string recipient)
        {
            var emailClient = new SmtpClient("host");
            emailClient.Send("noreply@dba.dk", recipient, "New books today",
                "Here is a list of new books: TODO");
        }
    }
}