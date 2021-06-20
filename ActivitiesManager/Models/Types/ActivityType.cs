using System.ComponentModel.DataAnnotations;

namespace ActivitiesManager.Models
{
    public class ActivityType
    {
        [Display(Name="Activity Type")]
        public int ID { get; set; }
        public string Description { get; set; }
    }
}