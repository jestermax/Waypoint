using System.Web.Http;
using Ninject.Syntax;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Presentation.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Presentation.NinjectWebCommon), "Stop")]

namespace Presentation
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

            Bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IBindingRoot kernel)
        {
            kernel.Bind<Domain.Repositories.IApiTokenRepository>().To<Domain.Repositories.ApiTokenRepository>();
            kernel.Bind<Domain.Repositories.IUserRepository>().To<Domain.Repositories.UserRepository>();

            //kernel.Bind<Domain.Authentication.IAuthenticationService>().To<Domain.Authentication.AspNetMembershipAuthenticationService>();
        }
    }
}
