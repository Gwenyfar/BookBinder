using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Domain.DTOs
{
    public class FetchPublisherDto : PublisherDto 
    {
        public Guid Id { get; set; }
    }
}
