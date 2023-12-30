using Bookbinder.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace BookBinder.Domain.Models
{
    public class User : ApplicationUser
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        
    }
}
