using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using SimpleInjector;
using MovieRecomendationSyst;

namespace MovieRecommendationsBackend.App_Start
{
    public class Bootstrapper
    {
        public void Setup()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            SetupDependencies(container);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private void SetupDependencies(Container container)
        {
            container.Register<IMovieInfService, MovieInfService>();
            container.Register<IRegistrationService, RegistrationService>();
            container.Register<UserProfileRepository>(() => new UserProfileRepository(ConfigurationManager.AppSettings["PathToRepository"]));
            container.Register<ISociatisationService, SocialisationService>();
        }
    }
}