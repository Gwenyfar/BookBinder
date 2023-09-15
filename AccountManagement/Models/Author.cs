using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Models
{
    public class Author : User
    {
        public IList<Book> Books => new List<Book>();
    }
}
