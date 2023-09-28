using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Services.PublisherFeatures
{
    public class FetchPublisherDto : PublisherDto 
    {
        public Guid Id { get; set; }
    }
}
