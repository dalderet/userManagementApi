namespace UserManagement.Api.AutofacModules
{
    using Autofac;
    using UserManagement.DataAccessLayer;
    using UserManagement.Model.Api;

    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterIRepository<User>(builder);
        }

        private void RegisterIRepository<T>(ContainerBuilder builder) where T : class
        {
            builder.RegisterType<InMemoryRepository<T>>()
                .As<IRepository<T>>().SingleInstance();
        }
    }
}
