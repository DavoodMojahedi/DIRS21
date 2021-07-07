using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMJ.DIRS21.Model.ClassVm
{
    public class ResultViewModel<TResultModel>
    {
        public ResultViewModel()
        {
            ErrorModel = new List<string>();
        }

        public bool IsSuccess => !ErrorModel.Any();

        public IEnumerable<string> ErrorModel { get; set; }

        public TResultModel ResultModel { get; set; }

    }
}
