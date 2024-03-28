

using Microsoft.AspNetCore.Identity;

namespace BookBinder.Domain.Models
{
    public class Admin : IdentityUser<Guid>, IEntity
    {
        public virtual IList<Book> Books { get; set; } =
        new List<Book>();
        public virtual IList<Publisher> Publishers { get; set; } = new List<Publisher>();
        public virtual IList<User> Users { get; set; } = new List<User>();
        public virtual IList<Author> Authors { get; set; } = new List<Author>();
    }
}
