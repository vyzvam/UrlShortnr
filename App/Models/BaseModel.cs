using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class BaseModel
    {
        public int Id { get; set; }

        public int Type { get; set; }

        public int Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public BaseModel(int operateBy = 0, DateTime? eventDate = null, int type = 1, int status = 1)
        {
            CreatedBy = operateBy;
            CreatedDate = eventDate ?? DateTime.Now;
            Type = type;
            Status = status;
        }

        public void SetHandler(int handlerId)
        {
            CreatedBy = handlerId;
        }
    }
}