using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class PickupDayViewModel
    {
        public List<Pickup> Pickups { get; set; }

        public string DaySearch { get; set; }
    }
}