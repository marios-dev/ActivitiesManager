using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActivitiesManager.Models.ViewModels
{
    public class ActivityCustomerViewmodel
    {
        public Customer Customer { get; set; }
        public CustomerType CustomerType { get; set; }
        public Activity Activity { get; set; }
        public ActivityType ActivityType { get; set; }
    }
}