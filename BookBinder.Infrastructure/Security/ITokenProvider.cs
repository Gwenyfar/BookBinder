using Bookbinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Security
{
    public interface ITokenProvider
    {
        public ClaimsPrincipal CreateClaimsIdentity(ApplicationUser user);
    }
}
