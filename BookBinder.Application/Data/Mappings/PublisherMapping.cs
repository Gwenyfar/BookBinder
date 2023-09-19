using BookBinder.Application.Models;
using BookBinder.Infrastructure.Mapping;

namespace BookBinder.Application.Data.Mappings
{
    public class PublisherMapping : BaseEntityMapping<Publisher>
    {
        public PublisherMapping()
        {
            Table("Publishers");
            Map(p => p.Company).Not.Nullable();
            Map(p => p.Address).Not.Nullable();
            Map(p => p.Email).Not.Nullable();
            HasMany(p => p.Books).Cascade.DeleteOrphan();
        }
    }
}
