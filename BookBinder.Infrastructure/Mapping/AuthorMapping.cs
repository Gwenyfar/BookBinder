using BookBinder.Application.Models;

namespace BookBinder.Infrastructure.Mapping
{
    public class AuthorMapping : BaseEntityMapping<Author>
    {
        public AuthorMapping()
        {
            Table("Authors");
            Map(u => u.FirstName).Not.Nullable();
            Map(u => u.LastName).Not.Nullable();
            Map(u => u.Email).Not.Nullable();
            HasManyToMany(u => u.Books)
                .Table("AuthorToBook")
                .ParentKeyColumn("Author_id")
                .ChildKeyColumn("Book_id");
        }
    }
}
