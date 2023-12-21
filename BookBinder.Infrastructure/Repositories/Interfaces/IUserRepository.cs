using Bookbinder.Domain.Models;
using BookBinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository<TUser> : IRespository<TUser> where TUser : ApplicationUser
    {
        Task<TUser> FetchByEmailAsync(string email);
    }
}
