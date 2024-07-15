using BookStore.API.Contracts;
using BookStore.Core.Abstractions;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookServce _bookServce;
        public BooksController(IBookServce bookServce)
        {
            _bookServce = bookServce;
        }


        [HttpGet]
        public async Task<ActionResult<List<BooksResponse>>> GetBooks()
        {
            var books = await _bookServce.GetAllBooks();
            var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.Description, b.Price));
            return Ok(response);
        }



        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request)
        {
            var (book, error) = Book.Create(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.Price
                );

            if(!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

           var bookId =  await _bookServce.CreateBook(book);
            return Ok(bookId);    
        }


        [HttpPut("{id.guid}")]
        public async Task<ActionResult<Guid>> UpdateBook(Guid id,[FromBody] BooksRequest request)
        {
            var bookId = await _bookServce.UpdateBook(id, request.Title, request.Description, request.Price);


            return Ok(bookId);
        }



    }
}
