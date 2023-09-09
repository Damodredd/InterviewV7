using InterviewV7.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewV7
{
    public class Startup
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public Startup()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            var collection = new ServiceCollection();

            // Register your services here
            collection.AddScoped<IStateService, StateService>();
            collection.AddScoped<IStateProcessorService, StateProcessorService>();

            ServiceProvider = collection.BuildServiceProvider();
        }
    }
}