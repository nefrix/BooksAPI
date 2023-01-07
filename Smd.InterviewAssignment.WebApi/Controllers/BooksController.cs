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
        [Route("{id}")]
        public object Get(int id)
        {
            //foreach (var book in Get())
            //{
            //    if (book.Id == id)
            //    {
            //        return book;
            //    }
            //}
        

            return null;
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