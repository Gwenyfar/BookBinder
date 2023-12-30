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
            Map(p => p.PhoneNumber).Nullable();
            Map(p => p.PasswordHash).Not.Nullable();
            Map(p => p.Role).Not.Nullable();
            HasMany(p => p.Books).Cascade.DeleteOrphan();
        }
    }
}
