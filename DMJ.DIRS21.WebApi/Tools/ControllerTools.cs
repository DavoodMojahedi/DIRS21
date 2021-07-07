using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMJ.DIRS21.Model.ClassVm;

namespace DMJ.DIRS21.WebApi.Tools
{
    public class ControllerTools
    {
        public static ResultViewModel<TResultModel> CreateSuccessResult<TResultModel>(TResultModel resultModel)
        {
            return new ResultViewModel<TResultModel>
            {
                ResultModel = resultModel,
            };
        }

        public static ResultViewModel<TResultModel> CreateInvalidResult<TResultModel>(List<string> errorList)
        {
            return new ResultViewModel<TResultModel>
            {
                ErrorModel = errorList,
            };
        }
    }
}
