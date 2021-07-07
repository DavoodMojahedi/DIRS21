using System.Collections.Generic;
using DMJ.DIRS21.Common.Enums;
using MongoDB.Bson;

namespace DMJ.DIRS21.Model.Documents
{
    public class Dish
    {
       
        public ObjectId _id { get; set; }

        public int Code { get; set; }

        public int RestaurantCode { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        
        public EDishCategory DishCategory { get; set; }
        
        public EDayInWeekAvailability DishAvailabilityInWeek { get; set; }
        
        public ETimeInDayAvailability DishAvailabilityInDay { get; set; }
        
        public bool IsActive { get; set; }

        public string DeactivationReason { get; set; }
        
        public int WaitingDurMinutesToDishReady { get; set; }
        
        public List<DishComment> DishComments { get; set; }
        
        public int Score { get; set; }
    }
}
