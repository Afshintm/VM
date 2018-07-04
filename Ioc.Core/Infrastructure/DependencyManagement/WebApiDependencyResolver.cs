using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace Ioc.Core.Infrastructure.DependencyManagement
{
    public class WebApiDependencyResolver : IDependencyResolver
    {

        public object GetService(Type serviceType)
        {
            return EngineContext.Current.ContainerManager.ResolveOptional(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(serviceType);
            return (IEnumerable<object>)EngineContext.Current.Resolve(type);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
        public void Dispose()
        {
            if (EngineContext.Current.ContainerManager.Container != null)
                EngineContext.Current.ContainerManager.Container.Dispose();
        }
    }

}
