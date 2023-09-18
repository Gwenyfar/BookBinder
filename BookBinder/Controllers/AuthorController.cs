using BookBinder.Application.Services.AuthorFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBinder.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;
        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        public IActionResult AddAuthor(AuthorDto authorDto)
        {
            var author = _authorService.AddAuthor(authorDto);
            return Ok(author);
        }

        [HttpGet]
        public IActionResult FetchAuthors()
        {
            var authors = _authorService.FetchAuthors();
            return Ok(authors);
        }
    }
}
