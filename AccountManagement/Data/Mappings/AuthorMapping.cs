using BookBinder.Application.Models;
using BookBinder.Infrastructure.Mapping;

namespace BookBinder.Application.Data.Mappings
{
    public class AuthorMapping : BaseEntityMapping<Author>
    {
        public AuthorMapping()
        {
            Table("Authors");
            Map(u => u.FirstName).Not.Nullable();
            Map(u => u.LastName).Not.Nullable();
            Map(u => u.Email).Not.Nullable();
            HasManyToMany(u => u.Books);
        }
    }
}
