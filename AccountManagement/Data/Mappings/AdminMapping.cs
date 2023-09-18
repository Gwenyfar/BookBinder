using BookBinder.Application.Models;
using BookBinder.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Data.Mappings
{
    public class AdminMapping : BaseEntityMapping<Admin>
    {
        public AdminMapping()
        {
            Table("Admins");
            Map(u => u.FirstName).Not.Nullable();
            Map(u => u.LastName).Not.Nullable();
            Map(u => u.Email).Not.Nullable();
        }
    }
}
