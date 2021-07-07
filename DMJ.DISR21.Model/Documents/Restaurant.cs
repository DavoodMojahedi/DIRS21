using System.Collections.Generic;

namespace DMJ.DIRS21.Model.Documents
{
    public class Restaurant
    {
        public int Code { get; set; }
        
        public string Name { get; set; }
        
        public Address Address { get; set; }
        
        public Contact Contact { get; set; }
        
    }
}
