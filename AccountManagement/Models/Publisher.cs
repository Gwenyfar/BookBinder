using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Models
{
    public class Publisher : BaseEntity
    {
        public string Company { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
