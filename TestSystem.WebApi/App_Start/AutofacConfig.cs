using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using TestSystem.WebApi.App_Start.Modules;

namespace TestSystem.WebApi.App_Start
{
    public class AutofacConfig
    {
        public static void Bootstrapper()
        {
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterWebApiFilterProvider(config);

            // 註冊Repository
            builder.RegisterModule(new DBRepositoryModule());

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}