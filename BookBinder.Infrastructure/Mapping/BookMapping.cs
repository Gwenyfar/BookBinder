using BookBinder.Domain.Models;

namespace BookBinder.Infrastructure.Mapping
{
    public class BookMapping : BaseEntityMapping<Book>
    {
        public BookMapping()
        {
            Table("Books");
            Map(b => b.Title).Not.Nullable();
            Map(b => b.ISBN).Not.Nullable();
            Map(b => b.Genre).Not.Nullable();
            References(b => b.Publisher);
            References(b => b.Admin);
            Map(b => b.PublishedDate).Not.Nullable();
            HasManyToMany(b => b.Authors)
                .Table("AuthorToBook")
                .ParentKeyColumn("Book_id")
                .ChildKeyColumn("Author_id");
            HasManyToMany(b => b.Users)
                .Table("UserToBook")
                .ParentKeyColumn("Book_id")
                .ChildKeyColumn("User_id");
        }
    }
}
