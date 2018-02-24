namespace MugStore.Web.App_Start
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;
    using MugStore.Data;
    using MugStore.Data.Common;
    using MugStore.Services.Common;
    using MugStore.Services.Data;

    public static class AutofacConfig
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

            builder.RegisterType<BulletinsService>()
                .As<IBulletinsService>();

            builder.RegisterType<CategoriesService>()
                .As<ICategoriesService>();

            builder.RegisterType<ProductsService>()
                .As<IProductsService>();

            builder.RegisterType<TagsService>()
                .As<ITagsService>();

            builder.RegisterType<CouriersService>()
                .As<ICouriersService>();

            builder.RegisterType<FeedbacksService>()
                .As<IFeedbacksService>();

            builder.RegisterType<LoggerService>()
                .As<ILoggerService>();

            builder.RegisterType<BlogService>()
                .As<IBlogService>();

            builder.RegisterGeneric(typeof(DbRepository<>))
               .As(typeof(IDbRepository<>))
               .InstancePerRequest();
        }
    }
}