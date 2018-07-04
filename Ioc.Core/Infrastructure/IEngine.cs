using Ioc.Core.Configuration;
using System;
using Ioc.Core.Infrastructure.DependencyManagement;

namespace Ioc.Core.Infrastructure
{

        /// <summary>
        /// Classes implementing this interface can serve as a portal for the 
        /// various services composing the Ioc engine. Edit functionality, modules
        /// and implementations access most Ioc functionality through this 
        /// interface.
        /// </summary>

    public interface IEngine
    {
            ContainerManager ContainerManager { get; }

            /// <summary>
            /// Initialize components in the Project environment.
            /// </summary>
            /// <param name="config">Config</param>
            void Initialize(IocConfig config);

            T Resolve<T>() where T : class;

            object Resolve(Type type);

            T[] ResolveAll<T>();

    }
}
