using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MovieRecommendationsBackend.Filters
{
    public class ArgumentFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var nullArguments = actionContext.ActionArguments.Where(argument => argument.Value == null);
            if (nullArguments.Any())
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, string.Join(",", nullArguments.Select(argument => argument.Key)));
            }
        }
    }
}