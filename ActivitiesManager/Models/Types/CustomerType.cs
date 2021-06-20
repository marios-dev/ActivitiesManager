using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ActivitiesManager.Models
{
    public class CustomerType
    {
        [Display(Name ="Customer Type")]
        public int ID { get; set; }
        public string Description { get; set; }
    }
}