using BookBinder.Infrastructure.Models;

namespace BookBinder.Application.Models
{
    public class Book : BaseEntity
    {
        public virtual string Title { get; set; }
        public virtual string ISBN { get; set; }
        public virtual string Genre { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual DateTime PublishedDate { get; set; }
        public virtual IList<Author> Authors { get; set; } = new List<Author>();
        public virtual IList<User> Users { get; set; } = new List<User>();

    }
}
