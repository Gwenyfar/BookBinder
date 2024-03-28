using BookBinder.Domain.Models;

namespace BookBinder.Infrastructure.Mapping
{
    public class UserMapping : BaseEntityMapping<User>
    {
        public UserMapping()
        {
            Table("Users");
            Map(u => u.FirstName).Not.Nullable();
            Map(u => u.LastName).Not.Nullable();
            Map(u => u.Email).Not.Nullable();
            Map(u => u.UserName).Nullable();
            Map(u => u.PasswordHash).Not.Nullable();
            Map(u => u.PhoneNumber).Nullable();
            References(u => u.Admin);
            HasManyToMany(u => u.Books)
                .Table("UserToBook")
                .ParentKeyColumn("User_id")
                .ChildKeyColumn("Book_id");
        }
    }
}
