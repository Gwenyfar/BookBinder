using BookBinder.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookbinder.Domain.Models
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity
    {
        public virtual IList<Book> Books { get; set; } =
        new List<Book>();
        public virtual AccountType Role { get; set; }

    }
}
