using Example.Data;
using Example.Data.Providers;
using Example.Services;
using Example.Services.Cryptography;
using Example.Services.Interfaces;
using Example.Web.Core.Services;
using Microsoft.Practices.Unity;
using System.Configuration;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Unity.WebApi;


namespace Example.Web.Interfaces
{
	public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>();
            //this should be per request
           
            container.RegisterType<IContactUsService, ContactUsService>();
           



            container.RegisterType<IDataProvider, SqlDataProvider>(
                new InjectionConstructor(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));

            container.RegisterType<IPrincipal>(new TransientLifetimeManager(),
                     new InjectionFactory(con => HttpContext.Current.User));

            container.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());

			GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));


        }
    }
}
