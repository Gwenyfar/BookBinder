namespace BookBinder.Domain.Models
{
    public class Publisher : BaseEntity
    {
        public virtual string Company { get; set; }
        public virtual string Address { get; set; }
        public virtual string Email { get; set; }
        public virtual IList<Book> Books { get; set; } = new List<Book>();
    }
}
