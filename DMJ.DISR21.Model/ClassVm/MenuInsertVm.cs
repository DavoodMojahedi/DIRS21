using System;
using System.Collections.Generic;

namespace DMJ.DIRS21.Model.ClassVm
{
    public class MenuInsertVm
    {
        public string Name { get; set; }
        
        public DateTime OrderDate { get; set; }

        public int RestaurantCode { get; set; }
        
        public List<int> DishCodes { get; set; }
    }
}
