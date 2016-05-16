namespace MugStore.Web.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;
    using MugStore.Data;
    using System.Data.Entity;
    using MugStore.Services.Data;
    using MugStore.Data.Common;

    public class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.Register(x => new ApplicationDbContext())
                .As<DbContext>()
                .InstancePerRequest();

            builder.RegisterType<OrdersService>()
                .As<IOrdersService>();

            builder.RegisterType<ImagesService>()
                .As<IImagesService>();

            builder.RegisterType<CitiesService>()
                .As<ICitiesService>();

            builder.RegisterGeneric(typeof(DbRepository<>))
               .As(typeof(IDbRepository<>))
               .InstancePerRequest();
        }
    }
}