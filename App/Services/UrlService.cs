using App.Repository;
using App.Models;
using App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Services
{
    public class UrlService 
    {
        private readonly IAppContext _context;
        private readonly IUrlShortEntity _urlShort;

        public UrlService(IAppContext context)  : base()
        { 
            _context = context;
            _urlShort = _context.UrlShorts;
        }

        public IQueryable<UrlShort> All()
        {
            return _urlShort.All().OrderByDescending(o => o.CreatedDate);
        }

        public UrlShort GetBy(int id)
        {
            return _urlShort.GetBy(id);
        }

        public UrlShort GetByName(string name)
        {
            return _urlShort.GetByName(name);
        }

        public UrlShort GetByShortKey(string shortKey)
        {
            return _urlShort.GetByShortKey(shortKey);
        }
        public UrlShort Create(UrlShortViewModel vModel)
        {
            var urlShort = new UrlShort()
            {
                Name = vModel.Name,
                ShortKey = vModel.ShortKey,
                Type = vModel.Type,
                Status = vModel.Status,
                CreatedBy = vModel.CreatedBy,
                CreatedDate = vModel.CreatedDate,                
            };

            _urlShort.Create(urlShort);
            _context.SaveChanges();
            return urlShort;
        }


        public int Update(UrlShortViewModel vModel)
        {
            var urlShort = _urlShort.GetBy(vModel.Id);
            urlShort.Name = vModel.Name;
            urlShort.ShortKey = vModel.ShortKey;

            _urlShort.Update(urlShort);
            return _context.SaveChanges();

        }

      

        public int Delete(int id)
        {
            var unit = _urlShort.GetBy(id);
            _urlShort.Delete(unit);
            return _context.SaveChanges();
        }
    }
}