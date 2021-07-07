using System.Collections.Generic;
using DMJ.DIRS21.Model.Documents;

namespace DMJ.DIRS21.Model.ClassVm
{
    public class DishCommentVm
    {
        public DishCommentVm()
        {
            Comments = new List<DishComment>();
        }

        public int DishCode { get; set; }
        
        public List<DishComment> Comments { get; set; }
    }
}
