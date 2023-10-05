﻿using Autofac;
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
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : BaseController
    {
        
        public AuthorController(IApplication application):base(application)
        {
           
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(CreateAuthorCommand command)
        {
            var response = await Application.ExecuteCommandAsync<CreateAuthorCommand, Guid>(command);
            return Ok(response);
            
        }

        [HttpGet]
        public async Task<IActionResult> FetchAuthors()
        {
            var query = new FetchAuthorsQuery();
            var response = await Application.ExecuteQueryAsync<FetchAuthorsQuery, IEnumerable<FetchAuthorDto>>(query);
            return Ok(response);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorCommand command)
        {
            var response = await Application.ExecuteCommandAsync<UpdateAuthorCommand>(command);
            return Ok(response);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAuthor(Guid id)
        {
            var command = new DeleteAuthorCommand { Id = id };
            var response = await Application.ExecuteCommandAsync<DeleteAuthorCommand>(command);
            return Ok(response);

        }
    }
}
