namespace UserManagement.Api.AutofacModules
{
    using Autofac;
    using System;

    public class ApplicationModule : Module
    {
        public ApplicationModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoriesModule());
            builder.RegisterModule<ServicesModule>();
        }
    }
}
