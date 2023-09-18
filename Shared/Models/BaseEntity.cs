using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Models
{
    public class BaseEntity : IEntity
    {
        public virtual Guid Id { get; set; }
    }
}
