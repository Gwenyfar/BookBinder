using Autofac;
using BookBinder.Application;
using BookBinder.Application.Commands;
using BookBinder.Application.Commands.CreateAuthor;
using BookBinder.Application.Commands.DeleteAuthor;
using BookBinder.Application.Commands.UpdateAuthor;
using BookBinder.Application.Queries.FetchAuthors;
using BookBinder.Domain.DTOs;
using BookBinder.Infrastructure.Utilities;
using BookBinder.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookBinder.Controllers
{
    /// <summary>
    /// Manages actions to be performed on authors
    /// </summary>
    [Route("api/authors")]
    [ApiController]
    [TypeFilter(typeof(ApiKeyAttribute))]
    public class AuthorController : BaseController
    {
        /// <summary>
        /// constructor
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
        [ProducesResponseType(typeof(ResponseResult<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddAuthor(CreateAuthorCommand command)
        {
            var response = await Application.ExecuteCommandAsync<CreateAuthorCommand, Guid>(command);
            return FetchResponse(response);
            
        }

        /// <summary>
        /// action to return all authors
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/authors
        /// </remarks>
        /// <returns>all authors</returns>
        [ProducesResponseType(typeof(ResponseResult<IEnumerable<FetchAuthorDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> FetchAuthors()
        {
            var query = new FetchAuthorsQuery();
            var response = await Application.ExecuteQueryAsync<FetchAuthorsQuery, IEnumerable<FetchAuthorDto>>(query);
            return FetchResponse(response);

        }

        /// <summary>
        /// action to update properties of an author
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/authors
        ///     {
        ///         "id": "a9852712-124b-4c3b-8ae2-d43b3d126b2b"
        ///         "firstName": "Iruoma",
        ///         "lastName": "Onyia",
        ///         "email": "iru@gmail.com"
        ///     }
        /// </remarks>
        /// <param name="command">model used to update an author</param>
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorCommand command)
        {
            var response = await Application.ExecuteCommandAsync<UpdateAuthorCommand>(command);
            return FetchResponse(response);

        }

        /// <summary>
        /// action to delete an author
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/authors/id/a9852712-124b-4c3b-8ae2-d43b3d126b2b
        /// </remarks>
        /// <param name="id">id of an existing author</param>
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAuthor(Guid id)
        {
            var command = new DeleteAuthorCommand { Id = id };
            var response = await Application.ExecuteCommandAsync<DeleteAuthorCommand>(command);
            return FetchResponse(response);

        }
    }
}
