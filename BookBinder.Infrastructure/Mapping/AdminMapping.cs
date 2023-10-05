using BookBinder.Domain.Models;
using BookBinder.Infrastructure.Mapping;

namespace BookBinder.Application.Data.Mapping
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
