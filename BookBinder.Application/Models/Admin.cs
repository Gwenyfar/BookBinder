using BookBinder.Infrastructure.Models;

namespace BookBinder.Application.Models
{
    public class Admin : BaseEntity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
    }
}
