﻿using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.DataBaseConfiguration
{
    /// <summary>
    /// session factory interface
    /// </summary>
    public interface INhibernateSessionFactory
    {
        ISession GetSession();
    }
}
