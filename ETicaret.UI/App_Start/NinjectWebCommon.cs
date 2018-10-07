[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ETicaret.UI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ETicaret.UI.App_Start.NinjectWebCommon), "Stop")]

namespace ETicaret.UI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using ETicaret.DAL.IoC.Ninject;
    using ETicaret.DAL.Abstract;
    using ETicaret.DAL.Concrete.EntityFramework.Entities;
    using System.Data.Entity;
    using DAL.Concrete.EntityFramework.Context;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load<DALModule>();

            kernel.Bind<IBasketProductDAL>().To<EFBasketProductDAL>();
            kernel.Bind<ICategoryDAL>().To<EFCategoryDAL>();
            kernel.Bind<ICampaignDAL>().To<EFCampaignDAL>();
            kernel.Bind<ICityDAL>().To<EFCityDAL>();
            kernel.Bind<IDistrictDAL>().To<EFDistrictDAL>();
            kernel.Bind<IMakeDAL>().To<EFMakeDAL>();
            kernel.Bind<IOrderDAL>().To<EFOrderDAL>();
            kernel.Bind<IPaymentTypeDAL>().To<EFPaymentTypeDAL>();
            kernel.Bind<IProductCommentDAL>().To<EFProductCommentDAL>();
            kernel.Bind<IProductDAL>().To<EFProductDAL>();
            kernel.Bind<IProductModelDAL>().To<EFProductModelDAL>();
            kernel.Bind<IProductPictureDAL>().To<EFProductPictureDAL>();
            kernel.Bind<IProductTechnicPropertyDAL>().To<EFProductTechnicPropertyDAL>();
            kernel.Bind<IPropertyDAL>().To<EFPropertyDAL>();
            kernel.Bind<IUserAddressDAL>().To<EFUserAddressDAL>();
            kernel.Bind<IUserCardDAL>().To<EFUserCardDAL>();
            kernel.Bind<IUserDAL>().To<EFUserDAL>();
            kernel.Bind<IUserDetailDAL>().To<EFUserDetailDAL>();
            kernel.Bind<IUserTypeDAL>().To<EFUserTypeDAL>();
            kernel.Bind<IOrderStatusDAL>().To<EFOrderStatusDAL>();
        }
    }
}
