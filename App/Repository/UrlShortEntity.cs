using App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace App.Repository
{
    public class UrlShortEntity : Data.EfRepository<UrlShort>, IUrlShortEntity
    {

        public UrlShortEntity(DbContext context, bool shareContext) : base(context, shareContext) { }

        public IQueryable<UrlShort> GetAll()
        {
            return All();
        }

        public UrlShort GetBy(int id)
        {
            return Find(u => u.Id == id);
        }

        public UrlShort GetByName(string name)
        {
            return Find(u => u.Name.Equals(name));
        }

        public UrlShort GetByShortKey(string key)
        {
            return Find(u => u.ShortKey.Equals(key));
        }
    }
}