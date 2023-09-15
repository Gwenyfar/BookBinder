using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public Publisher Publisher { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<Author> Authors => new List<Author>();

    }
}
