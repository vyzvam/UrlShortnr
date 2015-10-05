using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace App.Repository
{


    public class AppContext : IAppContext
    {
        private readonly DbContext _db;


        public IUrlShortEntity UrlShorts { get; private set; }



        public AppContext(DbContext context = null, IUrlShortEntity urlShorts = null)

        {
            _db = context ?? new AppDatabase();
            UrlShorts = urlShorts ?? new UrlShortEntity(_db, true);

        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            if (_db != null) { _db.Dispose(); }
        }
    }
}