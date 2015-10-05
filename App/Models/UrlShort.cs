using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class UrlShort : BaseModel
    {
        public string Name { get; set; }
        [MaxLength(6)]
        public string ShortKey { get; set; }

    }
}