using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Services;
using Services.Interfaces;
using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using FluentValidation;
using FluentValidation.Mvc;
using Aytesoft.Models.Edit.Validation;

namespace Aytesoft
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<BasketService>().As<IBasketService>();
            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<LoginService>().As<ILoginService>();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        private void ValidationConfiguration()
        {
            FluentValidationModelValidatorProvider.Configure(provider =>
            {
                provider.ValidatorFactory = new ValidatorFactory();
            });
        }
    }
}
