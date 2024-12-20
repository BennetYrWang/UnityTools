using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BennetYiranWang.Module
{
    /// <summary>
    /// This class the the base of all modules
    /// </summary>
    public abstract class AbstractModule
    {
        public bool Initialized { get; private set; }

        public static readonly object GlobalDependencyLock = new object();
        private readonly object _moduleLock = new object();

        public async Task Initialize()
        {
            if (Initialized)
                return;

            await InitializeDependency();
            await OnInitialize();
            Initialized = true;
        }

        public async Task InitializeAsDependency(List<AbstractModule> modulesInitializing)
        {
            lock (GlobalDependencyLock)
            {
                if (modulesInitializing.Contains(this))
                {
                    throw new InvalidOperationException($"Circular dependency initialization detected. " +
                                                        $"{string.Join("->",modulesInitializing.ConvertAll(module => module.GetType().Name))}");
                }

                modulesInitializing.Add(this);
            }

            await Initialize();
        }

        protected abstract Task OnInitialize();
        public abstract Task InitializeDependency();
    }
}