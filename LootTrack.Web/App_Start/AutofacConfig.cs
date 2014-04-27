using LootTrack.Domain;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace LootTrack.Web
{
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;

    public static class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterWebApiModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiModelBinderProvider();


            builder.RegisterModule(new AutofacWebTypesModule());
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterType<DatabaseContext>().As<IDataContextAsync>().WithParameter("nameOrConnectionString", "DefaultConnection").InstancePerHttpRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWorkAsync, IUnitOfWork>();
            builder.RegisterType<Entity>().As<IObjectState>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest();

            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}