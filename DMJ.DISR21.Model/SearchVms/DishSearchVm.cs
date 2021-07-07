using System;
using System.Collections.Generic;
using System.Text;
using DMJ.DIRS21.Common.Enums;

namespace DMJ.DIRS21.Model.SearchVms
{
    public class DishSearchVm
    {
        public int? RestaurantCode { get; set; }
        
        public bool? ActivityStatus { get; set; }
        
        public List<int> DishCodes { get; set; }
        
        public ETimeInDayAvailability? TimeInDayAvailability { get; set; }

        public EDayInWeekAvailability? DayInWeekAvailability { get; set; }
    }
}
