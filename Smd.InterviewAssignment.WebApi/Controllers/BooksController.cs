using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Smd.InterviewAssignment.WebApi.Data;
using Smd.InterviewAssignment.WebApi.Models;
using Smd.InterviewAssignment.WebApi.Services;

namespace Smd.InterviewAssignment.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookRepo _bookRepo;
        private readonly IEmailService _emailService;

        public BooksController(ILogger<BooksController> logger, IBookRepo bookRepo, IEmailService emailService)
        {
            _logger = logger;
            _bookRepo = bookRepo;
            _emailService = emailService;
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
                return NotFound($"Book with id {id} doesn't exist!");

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

            Book bookToUpdate = _bookRepo.UpdateBook(book);

            _logger.LogInformation("----> Book updated");

            return Ok(bookToUpdate);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<int> DeleteBook(int id)
        { 
            _logger.LogInformation("--> Hit DeleteBook - id: {id}", id);

            if (!_bookRepo.BookExists(id))
                return BadRequest("Book with provided ID doesn't exist!");

            _bookRepo.DeleteBook(id);

            _logger.LogInformation("----> Book deleted");

            return Ok(new { Id = id });
        }

        [HttpPatch]
        [Route("{id}")]
        public ActionResult<int> MarkBookAsRead(int id)
        {
            _logger.LogInformation("--> Hit MarkBookAsRead - id: {id}", id);

            if (!_bookRepo.BookExists(id))
                return BadRequest("Book with provided ID doesn't exist!");

            Book bookToMark = _bookRepo.MarkBookAsRead(id);

            _logger.LogInformation("----> Book marked as read");

            return Ok(bookToMark);
        }

        [HttpPost]
        [Route("mail")]
        public async Task<ActionResult> Mail([FromBody]string recipient)
        {
            // sending email requires providing specs in appsettings.json

            _logger.LogInformation("--> Hit Mail - recipient: {recipient}", recipient);

            if (string.IsNullOrEmpty(recipient))
                return BadRequest("Recipient address not provided!");

            List<Book> unreadBooks = _bookRepo.GetAllBooks().Where(b => b.IsRead == false).ToList();
            if (unreadBooks.Count == 0)
                return Ok("No unread books to be sent to recipient");

            StringBuilder emailBody = new StringBuilder();
            emailBody.AppendLine("Here is a list of new books:");
            emailBody.AppendLine(string.Join(Environment.NewLine, unreadBooks));

            EmailDto email = new EmailDto()
            {
                Sender = "noreply@dba.dk",
                Recipient = recipient,
                Subject = "New books today",
                Body = emailBody.ToString()
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "--> Error occured on sending email");
                return BadRequest("Error occured on sending email: " + ex.Message);
            }

            _logger.LogInformation("----> Email sent");

            return Ok("Sent email to: " + recipient);
        }
    }
}