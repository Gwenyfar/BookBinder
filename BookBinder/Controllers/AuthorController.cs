using Autofac;
using BookBinder.Application;
using BookBinder.Application.Commands;
using BookBinder.Application.Commands.CreateAuthor;
using BookBinder.Application.Commands.DeleteAuthor;
using BookBinder.Application.Commands.UpdateAuthor;
using BookBinder.Application.Queries.FetchAuthors;
using BookBinder.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookBinder.Controllers
{
    /// <summary>
    /// Manages actions to be performed on authors
    /// </summary>
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : BaseController
    {
        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="application">contains application callers</param>
        public AuthorController(IApplication application):base(application)
        {
           
        }

        /// <summary>
        /// action to add an author
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/authors
        ///     {
        ///         "firstName": "Iruoma",
        ///         "lastName": "Onyia",
        ///         "email": "iru@gmail.com"
        ///     }
        /// </remarks>
        /// <param name="command">new author information</param>
        /// <returns>new author's id</returns>
        [HttpPost]
        public async Task<IActionResult> AddAuthor(CreateAuthorCommand command)
        {
            var response = await Application.ExecuteCommandAsync<CreateAuthorCommand, Guid>(command);
            return FetchResponse(response);
            
        }

        [HttpGet]
        public async Task<IActionResult> FetchAuthors()
        {
            var query = new FetchAuthorsQuery();
            var response = await Application.ExecuteQueryAsync<FetchAuthorsQuery, IEnumerable<FetchAuthorDto>>(query);
            return FetchResponse(response);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorCommand command)
        {
            var response = await Application.ExecuteCommandAsync<UpdateAuthorCommand>(command);
            return FetchResponse(response);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAuthor(Guid id)
        {
            var command = new DeleteAuthorCommand { Id = id };
            var response = await Application.ExecuteCommandAsync<DeleteAuthorCommand>(command);
            return FetchResponse(response);

        }
    }
}
