using BookBinder.Application.Data.Repositories;
using BookBinder.Application.Models;
using BookBinder.Application.Services.AuthorFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Services.PublisherFeatures
{
    public class PublisherService
    {
        public async Task<Guid> AddPublisher(PublisherDto publisherDto)
        {
            var publisher = new Publisher
            {
                Company = publisherDto.Company,
                Address = publisherDto.Company,
                Email = publisherDto.Email
            };
            await Repository.AddAsync(publisher);
            return publisher.Id;
        }

        public async Task UpdatePublisher(FetchPublisherDto publisherDto)
        {
            var publisher = await Repository.FetchByIdAsync(publisherDto.Id);
            publisher.Company = publisherDto.Company;
            publisher.Address = publisherDto.Address;
            publisher.Email = publisherDto.Email;
            await Repository.UpdateAsync(publisher);
        }

        public async Task RemovePublisher(Guid publisherId)
        {
            var author = await Repository.FetchByIdAsync(publisherId);
            await Repository.RemoveAsync(author);
        }

        public async Task<IEnumerable<FetchPublisherDto>> FetchPublishers()
        {
            var authors = await Repository.FetchAllAsync();

            return authors.Select(p => new FetchPublisherDto
            {
                Id = p.Id,
                Company = p.Company,
                Address = p.Address,
                Email = p.Email
            });
        }

        public PublisherRepository Repository { get; set; }
    }
}
