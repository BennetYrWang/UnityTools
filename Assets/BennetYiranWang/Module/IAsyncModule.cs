using System.Threading.Tasks;

namespace BennetYiranWang.Module
{
    /// <summary>
    /// This interface provides asynchronous methods for the module, those are not thread-safe
    /// </summary>
    public interface IAsyncModule
    {
        public async Task InitializeAsync(AbstractModule module)
        {
            if (!module.Initialized)
                await module.Initialize();
        }

        public async Task InitializeDependencyAsync(AbstractModule module)
        {
            if (module.Initialized)
                return;
            await InitializeDependencyAsyncHandler();
        }

        protected Task InitializeDependencyAsyncHandler();
    }
}