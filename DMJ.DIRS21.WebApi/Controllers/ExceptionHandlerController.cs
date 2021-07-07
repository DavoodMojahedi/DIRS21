using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMJ.DIRS21.Model.ClassVm;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace DMJ.DIRS21.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionHandlerController : ControllerBase
    {

        [ApiExplorerSettings(IgnoreApi = true), HttpGet, HttpPost, HttpDelete, HttpPatch, HttpPut, HttpHead,
         HttpOptions, ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ResultViewModel<bool> Index()
        {
            var message = CreateExceptionMessage(HttpContext);

            var errorIssuer = GetErrorIssuerExceptionMessage(HttpContext);

            //ToDo: the exception message could be logged here

            return CreateExceptionMessage($"error : {message}, errorIssuer : {errorIssuer}");
        }

        private static string CreateExceptionMessage(HttpContext context)
        {
            var exHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

            var error = exHandlerFeature?.Error;

            return error?.Message;
        }

        private static string GetErrorIssuerExceptionMessage(HttpContext context)
        {
            var exHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

            var urlError = GetUrlError(exHandlerFeature);

            return urlError;
        }

        private static ResultViewModel<bool> CreateExceptionMessage(string message)
        {

            return new ResultViewModel<bool>()
            {
                ErrorModel = new List<string>() { message }
            };
        }

        private static string GetUrlError(IExceptionHandlerFeature src)
        {
            return src is null ?
                string.Empty :
                (string)src.GetType().GetProperty("Path")?.GetValue(src, null);
        }

    }
}
