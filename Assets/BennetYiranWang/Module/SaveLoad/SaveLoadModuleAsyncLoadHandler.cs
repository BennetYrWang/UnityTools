using System.Collections.Generic;
using System.Threading.Tasks;

namespace BennetYiranWang.Module.SaveLoad
{
    public class SaveLoadModuleAsyncLoadHandler : IModuleAsyncLoadHandler
    {
        private List<IModuleAsyncLoadHandler> _dependencies;
        private Task _initializationTask;
        private bool _initialized;

        List<IModuleAsyncLoadHandler> IModuleAsyncLoadHandler.Dependencies
        {
            get => _dependencies;
            set => _dependencies = value;
        }

        Task IModuleAsyncLoadHandler.InitializationTask
        {
            get => _initializationTask;
            set => _initializationTask = value;
        }

        bool IModuleAsyncLoadHandler.Initialized
        {
            get => _initialized;
            set => _initialized = value;
        }


        Task IModuleAsyncLoadHandler.OnInitialize()
        {
            throw new System.NotImplementedException();
        }
    }
}