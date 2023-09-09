using InterviewV7.Models;
using InterviewV7.Services;
using Microsoft.Extensions.DependencyInjection;
using static InterviewV7.Tools.StringTools;

namespace InterviewV7
{
    public static class Program
    {
        private static ServiceProvider? _serviceProvider;

        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the path to the data source.");
                return;
            }

            string dataSourcePath = args[0];
            RegisterServices();

            var stateService = _serviceProvider!.GetService<IStateService>();
            var states = stateService.GetStates(dataSourcePath);

            var output = string.Join(Environment.NewLine,
               states.Select(state =>
                   $"{ToSentenceCase(state.StateType)} at {state.TimeStamp}" +
                   (state is Faulted faulted ? $" | Alarm code: {faulted.AlarmCode}" : string.Empty)));

            Console.WriteLine(output);

            DisposeServices();
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IStateService, StateService>();
            _serviceProvider = collection.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            _serviceProvider?.Dispose();
        }
    }
}