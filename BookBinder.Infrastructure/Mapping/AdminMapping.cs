using BookBinder.Domain.Models;
using BookBinder.Infrastructure.Mapping;

namespace BookBinder.Application.Data.Mapping
{
    public class AdminMapping : BaseEntityMapping<Admin>
    {
        public AdminMapping()
        {
            Table("Admins");
            Map(u => u.UserName).Not.Nullable();
            Map(u => u.PhoneNumber).Not.Nullable();
            Map(u => u.Email).Not.Nullable();
            Map(u => u.PasswordHash).Not.Nullable();
            HasMany(u => u.Books);
            HasMany(u => u.Publishers);
            HasMany(u => u.Users);
            HasMany(u => u.Authors);
        }
    }
}
