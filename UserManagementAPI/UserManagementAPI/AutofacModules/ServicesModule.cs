namespace UserManagement.Api.AutofacModules
{
    using Autofac;
    using UserManagement.Domain.IManagers.Api;
    using UserManagement.Domain.Managers;

    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManagementService>().As<IUserManagementService>().SingleInstance();
        }
    }
}
