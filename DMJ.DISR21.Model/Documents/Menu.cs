using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace DMJ.DIRS21.Model.Documents
{
    public class Menu
    {
        public ObjectId _id { get; set; }

        public string Name { get; set; }

        public DateTime OrderDate { get; set; }
        
        public Restaurant Restaurant { get; set; }
        
        public List<Dish> Dishes { get; set; }

    }
}
