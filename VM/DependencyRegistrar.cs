using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;
using Autofac;
using Autofac.Integration.WebApi;
using Ioc.Core;
using Ioc.Core.Infrastructure;
using Ioc.Core.Infrastructure.DependencyManagement;
using VM.BusinessLogic;

namespace VM
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get { return 0; }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //HTTP context and other related stuff
            builder.Register(c => new HttpContextWrapper(HttpContext.Current) as HttpContextBase)
                .As<HttpContextBase>()
                .InstancePerRequest();

            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerRequest();

            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerRequest();

            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerRequest();

            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerRequest();

            //We need one configuration throughout the application
            //builder.RegisterType<Config>().As<IConfig>().SingleInstance();

            //We need one ExceptionHandller for each request
            //builder.RegisterType<ExceptionHandler>().As<IExceptionHandler>().SingleInstance();

            builder.RegisterType<VendingMachine>().As<IVendingMachine>().SingleInstance();


            // securityProvider component is registered for each request in which the session is unique 
            //builder.RegisterType<SecurityProvider>().As<ISecurityProvider>().InstancePerRequest();

            //builder.RegisterType<ClassA>().As<IClassA>().InstancePerDependency();

            
            //builder.Register<Func<string, int, IClassA>>(c => { return new ClassA(); });

            //controllers
            builder.RegisterApiControllers(typeFinder.GetAssemblies().ToArray());


        }
    }
}