using InterviewV7.Models;
using InterviewV7.Services;
using Microsoft.Extensions.DependencyInjection;
using static InterviewV7.Tools.StateMetrics;
using static InterviewV7.Tools.StringTools;

namespace InterviewV7
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the path to the data source next time.");
                return;
            }

            string dataSourcePath = args[0];

            var startup = new Startup();

            var stateService = startup.ServiceProvider.GetService<IStateService>();
            var states = stateService.GetStates(dataSourcePath);

            var stateProcessorService = startup.ServiceProvider.GetService<IStateProcessorService>();
            var processedStates = stateProcessorService.ProcessStates(states);

            #region test file output

            //test output the OG file
            Console.WriteLine("\nData as read from file:");
            var output = string.Join(Environment.NewLine,
               states.Select(state =>
                   $"{ToSentenceCase(state.StateType)} at {state.TimeStamp}" +
                   (state is Faulted faulted ? $" | Alarm code: {faulted.AlarmCode}" : string.Empty)));

            Console.WriteLine(output);

            Console.WriteLine("End of data as read from file...");

            #endregion test file output

            #region output performance data

            Console.WriteLine("\nData as processed:");
            var output2 = string.Join(Environment.NewLine,
               processedStates.Select(state =>
                   $"{ToSentenceCase(state.StateType)} at {state.TimeStamp} with Duration {state.Duration}" +
                   (state is Faulted faulted ? $" | Alarm code: {faulted.AlarmCode}" : string.Empty)));

            Console.WriteLine(output2);
            Console.WriteLine("End of data as processed...");

            #endregion output performance data

            #region calculate performance data

            var totalRunningTime = CalculateTotalTime<Running>(processedStates);
            var totalFaultedTime = CalculateTotalTime<Faulted>(processedStates);

            var runningPercentage = Math.Round((totalRunningTime / (totalRunningTime + totalFaultedTime)) * 100, 2);
            var faultedPercentage = 100 - runningPercentage;

            var topAlarmCodes = TopAlarmCodesByDuration(processedStates);

            Console.WriteLine($"\nTotal Running Time: {totalRunningTime}s | Percentage of Total Time (% Availability): {runningPercentage}%");
            Console.WriteLine($"Total Running Time: {totalFaultedTime}s | Percentage of Total Time (% Downtime): {faultedPercentage}%");
            Console.WriteLine($"\nTop 5 alarms by total duration:");
            var alarmCodeOutput = string.Join(Environment.NewLine,
                topAlarmCodes.Select(group => $"Alarm Code: {group.AlarmCode} - Duration: {group.Duration} seconds"));

            Console.WriteLine(alarmCodeOutput);

            #endregion calculate performance data

            startup.ServiceProvider.Dispose();
        }
    }
}