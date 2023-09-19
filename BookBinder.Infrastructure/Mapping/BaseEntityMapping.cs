using BookBinder.Infrastructure.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
