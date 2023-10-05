using Microsoft.AspNetCore.Identity;

namespace BookBinder.Domain.Models
{
    public class User : IdentityUser<Guid>, IEntity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual IList<Book> Books { get; set; } =
        new List<Book>();
    }
}
