using BookBinder.Domain.Models;

namespace BookBinder.Infrastructure.Mapping
{
    public class PublisherMapping : BaseEntityMapping<Publisher>
    {
        public PublisherMapping()
        {
            Table("Publishers");
            Map(p => p.Company).Not.Nullable();
            Map(p => p.Address).Not.Nullable();
            Map(p => p.Email).Not.Nullable();
            Map(p => p.UserName).Nullable();
            Map(u => u.PasswordHash).Not.Nullable();
            Map(u => u.PhoneNumber).Not.Nullable();
            References(p => p.Admin);
            HasMany(p => p.Books).Cascade.DeleteOrphan();
        }
    }
}
