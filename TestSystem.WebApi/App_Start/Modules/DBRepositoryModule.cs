using Autofac;
using System.Web.Configuration;
using TestSystem.Persistent.Repository;
using TestSystem.Persistent.Repository.Interface;

namespace TestSystem.WebApi.App_Start.Modules
{
    public class DBRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var connectionString = WebConfigurationManager.ConnectionStrings["MSSQLContext"].ConnectionString;

            builder.Register(c => new AccountRepository(connectionString)).As<IAccountRepository>();
            builder.Register(c => new AccountLevelRepository(connectionString)).As<IAccountLevelRepository>();
            builder.Register(c => new SignRepository(connectionString)).As<ISignRepository>();
        }
    }
}