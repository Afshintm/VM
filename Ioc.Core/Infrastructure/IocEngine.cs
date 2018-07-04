using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Autofac;
using Ioc.Core.Infrastructure.DependencyManagement;
using Ioc.Core.Configuration;

namespace Ioc.Core.Infrastructure
{
    public class IocEngine : IEngine
    {
        #region Fields
        private ContainerManager _containerManager;
        #endregion

        #region Ctor
        
        #endregion

        #region Ctor

        /// <summary>
		/// Creates an instance of the content engine using default settings and configuration.
		/// </summary>
		public IocEngine() 
            : this(new ContainerConfigurer())
		{
		}

		public IocEngine(ContainerConfigurer configurer)
		{
            var config = ConfigurationManager.GetSection("IocConfig") as IocConfig;
            InitializeContainer(configurer, config);
		}
        
        #endregion

        #region Utilities

        private void RunStartupTasks()
        {
            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            //sort
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

        private void InitializeContainer(ContainerConfigurer configurer, IocConfig config)
        {
            var builder = new ContainerBuilder();

            _containerManager = new ContainerManager(builder.Build());
            configurer.Configure(this, _containerManager, config);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize components and plugins in the project environment.
        /// </summary>
        /// <param name="config">Config</param>
        public void Initialize(IocConfig config)
        {
            //bool databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();
            //if (databaseInstalled)
            //{
            //    //startup tasks
            //    RunStartupTasks();
            //}


            //startup tasks
            if (!config.IgnoreStartupTasks)
            {
                RunStartupTasks();
            }
        }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

        #endregion

        #region Properties

        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        #endregion
    }
}
