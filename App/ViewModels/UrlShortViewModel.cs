using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.ViewModels
{

    public class UrlShortViewModel : Models.BaseModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "Name cannot be less than 3 characters")]
        [MaxLength(150, ErrorMessage = "Name cannot be more than 150 characters")]
        public string Name { get; set; }

        public string ShortKey { get; set; }

        public UrlShortViewModel() : base() { }
    }

}