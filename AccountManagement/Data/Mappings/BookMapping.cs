using BookBinder.Application.Models;
using BookBinder.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Data.Mappings
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
