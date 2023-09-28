using Autofac;
using BookBinder.Application.Data.Repositories;
using BookBinder.Application.Services.AuthorFeatures;
using BookBinder.Application.Services.PublisherFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBinder.Controllers
{
    [Route("api/publisher")]
    [ApiController]
    public class PublisherController : BaseController
    {
        private readonly PublisherService _publisherService;
        public PublisherController(PublisherService publisherService, IContainer container) : base(container)
        {
            _publisherService = publisherService;
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(PublisherDto publisherDto)
        {
            try
            {
                using var scope = Container.BeginLifetimeScope();
                _publisherService.Repository = scope.Resolve<PublisherRepository>();
                var publisher = await _publisherService.AddPublisher(publisherDto);
                return Ok(publisher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> FetchPublishers()
        {
            try
            {
                using var scope = Container.BeginLifetimeScope();
                _publisherService.Repository = scope.Resolve<PublisherRepository>();
                var publishers = await _publisherService.FetchPublishers();
                return Ok(publishers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdatePublisher(FetchPublisherDto publisherDto)
        {
            try
            {
                using var scope = Container.BeginLifetimeScope();
                _publisherService.Repository = scope.Resolve<PublisherRepository>();
                await _publisherService.UpdatePublisher(publisherDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovePublisher(Guid id)
        {
            try
            {
                using var scope = Container.BeginLifetimeScope();
                _publisherService.Repository = scope.Resolve<PublisherRepository>();
                await _publisherService.RemovePublisher(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
