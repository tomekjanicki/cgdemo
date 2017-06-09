namespace Demo.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Reflection;
    using System.Web.Http;
    using Common.Handlers;
    using Common.Handlers.Interfaces;
    using Common.Mappings;
    using Common.Shared.TemplateMethods.Commands.Interfaces;
    using Common.Shared.TemplateMethods.Queries.Interfaces;
    using Common.Tools;
    using Common.Tools.Interfaces;
    using Infrastructure;
    using Logic.CQ.Customer;
    using Logic.CQ.Customer.Delete.Interfaces;
    using Logic.CQ.Customer.Get;
    using Logic.Database;
    using Logic.Database.Interfaces;
    using Logic.Facades.Apis;
    using Logic.Mappings;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using SimpleInjector.Lifestyles;
    using Command = Logic.CQ.Customer.Update.Command;
    using Repository = Logic.CQ.Customer.Delete.Repository;

    public static class RegisterContainer
    {
        public static void Execute(HttpConfiguration configuration)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.RegisterWebApiControllers(configuration);

            RegisterSingletons(container);

            RegisterScoped(container);

            container.Verify();

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void RegisterScoped(Container container)
        {
            var lifeStyle = Lifestyle.Scoped;
            container.Register<IRepository, Repository>(lifeStyle);
            container.Register<IUpdateRepository<Command>, Logic.CQ.Customer.Update.Repository>(lifeStyle);
            container.Register<Logic.CQ.Customer.Insert.Interfaces.IRepository, Logic.CQ.Customer.Insert.Repository>(lifeStyle);
            container.Register<IGetRepository<Customer>, Logic.CQ.Customer.Get.Repository>(lifeStyle);
            var concreteTypes = GetConcreteTypes();
            concreteTypes.ForEach(type => container.Register(type, type, lifeStyle));
            var assemblies = GetAssemblies();
            container.Register(typeof(IRequestHandler<,>), assemblies, lifeStyle);
            container.Register(typeof(IVoidRequestHandler<>), assemblies, lifeStyle);
        }

        private static void RegisterSingletons(Container container)
        {
            container.RegisterSingleton<IMediator, Mediator>();
            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton<IAssemblyVersionProvider, AssemblyVersionProvider>();
            container.RegisterSingleton<IDbConnectionProvider, DbConnectionProvider>();
            container.RegisterSingleton<IEntryAssemblyProvider, EntryAssemblyProvider>();
            container.RegisterSingleton(() => Helper.GetMapper(AutoMapperConfiguration.Configure));
        }

        private static ImmutableList<Type> GetConcreteTypes()
        {
            return new List<Type>
            {
                typeof(CustomersFilterPagedFacade),
                typeof(CustomersGetFacade),
                typeof(VersionGetFacade),
                typeof(CustomersDeleteFacade),
                typeof(CustomersPutFacade),
                typeof(CustomersPostFacade),
                typeof(SharedQueries)
            }.ToImmutableList();
        }

        private static ImmutableList<Assembly> GetAssemblies()
        {
            return new List<Assembly> { typeof(AutoMapperConfiguration).GetTypeInfo().Assembly }.ToImmutableList();
        }
    }
}