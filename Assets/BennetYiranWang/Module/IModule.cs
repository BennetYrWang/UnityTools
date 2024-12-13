using System;
using System.Collections.Generic;

namespace BennetYiranWang.Module
{
    public interface IModule
    {
        private static HashSet<IModule> ModulesInitializing = new();
        bool Initialized { get; }
        IEnumerable<IModule> Dependencies { get; }

        public sealed void InitializeAsDependency(List<IModule> loadingModules)
        {
            if (loadingModules.Contains(this))
                throw new InvalidOperationException($"Circular dependency initialization detected. " +
                                                    $"{string.Join("->",loadingModules.ConvertAll(module => module.GetType().Name))}");
            Initialize();
        }

        public sealed bool Initialize()
        {
            if (Initialized) return true;
            
            LoadDependencies(new List<IModule>());
            return OnInitialize();
        }
        protected bool OnInitialize();

        private void LoadDependencies(List<IModule> loadingModules)
        {
            if (Dependencies == null)
                return;
            foreach (IModule dependency in Dependencies)
                dependency?.InitializeAsDependency(loadingModules);
        }
    }
}