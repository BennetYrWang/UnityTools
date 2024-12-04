using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BennetYiranWang.Module
{
    public interface IModuleAsyncLoadHandler
    {
        // Infinite Loop Initialization not resolved
        // Default IModule Interface Implementation
        // MultiThread support to be done
        
        List<IModuleAsyncLoadHandler> Dependencies { get; protected set; }
        Task InitializationTask { get; protected set; }
        bool Initialized { get; protected set; }
        
        public sealed async Task InitializeAsync()
        {
            if (Initialized) return; // Initialized
            if (InitializationTask != null) // Initializing
            {
                await InitializationTask;
                return;
            }
            
            InitializationTask = InternalInitializeAsync();
            await InitializationTask;
            InitializationTask = null;
        }


        private async Task InternalInitializeAsync()
        {
            await InitializeDependenciesAsync(); // Initiate dependencies
            await InitializeAsync(); // Initialize this module
            
            Initialized = true;
        }

        private async Task InitializeDependenciesAsync()
        {
            if (Dependencies == null || Dependencies.Count == 0)
                return;
            
            await Task.WhenAll(Dependencies.Select(dependency => dependency.InitializeAsync()));
        }

        /// <summary>
        /// The Initialization function
        /// </summary>
        /// <returns></returns>
        protected Task OnInitialize();
    }
}