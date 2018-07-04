using Ioc.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ioc.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// Configures the inversion of control container with services used by the project.
    /// </summary>
    public class ContainerConfigurer
    {
        public virtual void Configure(IEngine engine, ContainerManager containerManager, IocConfig configuration)
        {
            //other dependencies
            containerManager.AddComponentInstance<IocConfig>(configuration?? new IocConfig(), "Ioc.configuration");
            
            containerManager.AddComponentInstance<IEngine>(engine, "Ioc.engine");
            
            containerManager.AddComponentInstance<ContainerConfigurer>(this, "Ioc.containerConfigurer");

            //type finder
            containerManager.AddComponent<ITypeFinder, WebAppTypeFinder>("Ioc.typeFinder");

            //register dependencies provided by other assemblies
            var typeFinder = containerManager.Resolve<ITypeFinder>();

            containerManager.UpdateContainer(x =>
            {
                var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
                var drInstances = new List<IDependencyRegistrar>();
                foreach (var drType in drTypes)
                    drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
                //sort
                drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();

                // calling register method
                foreach (var dependencyRegistrar in drInstances)
                    dependencyRegistrar.Register(x, typeFinder);
            });
        }


    }
}
