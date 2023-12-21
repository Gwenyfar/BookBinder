using Bookbinder.Domain.Models;

namespace BookBinder.Domain.Models
{
    public class Author : ApplicationUser
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }
}
