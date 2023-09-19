using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure
{
    public class ApplicationSettings
    {
        public ILoggerFactory LoggerFactory { get; set; }
        public string ConnectionString { get; set; }
    }
}
