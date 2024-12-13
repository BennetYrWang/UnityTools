using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BennetYiranWang.Module
{
    /// <summary>
    /// This interface provides ThreadSafeModule Methods, but those don't support asynchronous operation
    /// </summary>
    public interface IThreadSafeModule
    {
        protected object InitializeLock { get; }
        protected object DependencyLock { get; }

        public void InitializeThreadSafe(AbstractModule module)
        {
            if (module.Initialized)
                return;

            lock (module)
            {
                if (!module.Initialized)
                    module.Initialize();
            }
        }

        public void InitializeDependencyThreadSafe(AbstractModule module, List<AbstractModule> modulesInitializing)
        {
            lock (module)
            {
                lock (AbstractModule.GlobalDependencyLock)
                {
                    if (modulesInitializing.Contains(module))
                        throw new InvalidOperationException($"Circular dependency initialization detected. " +
                                                            $"{string.Join("->",modulesInitializing.ConvertAll(mod => mod.GetType().Name))}");

                    modulesInitializing.Add(module);
                }

                if (!module.Initialized)
                    module.InitializeDependency();
            }
        }
    }
}