using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Repository
{
    public interface IUrlShortEntity : Data.IRepository<UrlShort>
    {
        IQueryable<UrlShort> GetAll();
        UrlShort GetBy(int id);
        UrlShort GetByName(string name);
        UrlShort GetByShortKey(string key);
    }
}