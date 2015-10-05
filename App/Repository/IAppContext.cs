using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Repository
{
    public interface IAppContext
    {
        IUrlShortEntity UrlShorts { get; }
        
        int SaveChanges();

        void Dispose();
    }
}