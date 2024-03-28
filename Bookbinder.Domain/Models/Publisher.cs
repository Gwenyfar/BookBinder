namespace BookBinder.Domain.Models
{
    public class Publisher : User
    {
        public virtual string Company { get; set; }
        public virtual string Address { get; set; }
    }
}
