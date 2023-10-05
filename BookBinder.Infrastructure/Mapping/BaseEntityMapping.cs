using BookBinder.Domain.Models;
using FluentNHibernate.Mapping;

namespace BookBinder.Infrastructure.Mapping
{
    public class BaseEntityMapping<T> : ClassMap<T> where T : IEntity
    {
        public BaseEntityMapping() 
        {
            Id(entity => entity.Id).GeneratedBy.GuidComb();
        }
    }
}
