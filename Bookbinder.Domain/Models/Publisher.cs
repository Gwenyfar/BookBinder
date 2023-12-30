using Bookbinder.Domain.Models;

namespace BookBinder.Domain.Models
{
    public class Publisher : ApplicationUser
    {
        public virtual string Company { get; set; }
        public virtual string Address { get; set; }
    }
}
