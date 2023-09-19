using BookBinder.Application.Models;
using BookBinder.Infrastructure.Mapping;

namespace BookBinder.Application.Data.Mappings
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
            Map(u => u.PhoneNumber).Not.Nullable();
            HasManyToMany(u => u.Books)
                .Table("UserToBook")
                .ParentKeyColumn("User_id")
                .ChildKeyColumn("Book_id");
        }
    }
}
