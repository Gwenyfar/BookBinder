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
            try
            {
                using var scope = Container.BeginLifetimeScope();
                _authorService.Repository = scope.Resolve<AuthorRepository>();
                var author = await _authorService.AddAuthor(authorDto);
                return Ok(author);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> FetchAuthors()
        {
            try
            {
                using var scope = Container.BeginLifetimeScope();
                _authorService.Repository = scope.Resolve<AuthorRepository>();
                var authors = await _authorService.FetchAuthors();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthor(FetchAuthorDto authorDto)
        {
            try
            {
                using var scope = Container.BeginLifetimeScope();
                _authorService.Repository = scope.Resolve<AuthorRepository>();
                await _authorService.UpdateAuthor(authorDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAuthor(Guid id)
        {
            try
            {
                using var scope = Container.BeginLifetimeScope();
                _authorService.Repository = scope.Resolve<AuthorRepository>();
                await _authorService.RemoveAuthor(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
