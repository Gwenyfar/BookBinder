using Autofac;
using BookBinder.Application.Data.Repositories;
using BookBinder.Application.Services.AuthorFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBinder.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : BaseController
    {
        private readonly AuthorService _authorService;
        public AuthorController(AuthorService authorService, IContainer container):base(container)
        {
            _authorService = authorService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(AuthorDto authorDto)
        {
            using var scope = Container.BeginLifetimeScope();
            _authorService.Repository = scope.Resolve<AuthorRepository>();
            var author = await _authorService.AddAuthor(authorDto);
            return Ok(author);
        }

        [HttpGet]
        public async Task<IActionResult> FetchAuthors()
        {
            using var scope = Container.BeginLifetimeScope();
            _authorService.Repository = scope.Resolve<AuthorRepository>();
            var authors = await _authorService.FetchAuthors();
            return Ok(authors);
        }
    }
}
