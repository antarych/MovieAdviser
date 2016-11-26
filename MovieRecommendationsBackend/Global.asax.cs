using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using MovieRecommendationsBackend.App_Start;

namespace MovieRecommendationsBackend
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            new Bootstrapper().Setup();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
