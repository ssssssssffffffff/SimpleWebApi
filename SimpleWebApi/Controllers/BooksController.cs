using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Data;
using SimpleWebApi.Entities;

using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookContext context;

        public BooksController(BookContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>>List()
        {
            var books = context.Books;
            return books;
        }

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = context.Books.FirstOrDefault(b => b.BookId.Equals(id));
            return book;
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = book.BookId }, book);

        }

        [HttpPut]
        public IActionResult Update(int id, Book book)
        {
            context.Entry(book).State = EntityState.Modified;
            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book= context.Books.FirstOrDefault(b => b.BookId.Equals(id));
            context.Books.Remove(book);
            context.SaveChanges();
            return NoContent();
        }
    }
}
