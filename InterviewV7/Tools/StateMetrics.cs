using InterviewV7.Models;

namespace InterviewV7.Tools
{
    public static class StateMetrics
    {
        public static double CalculateTotalTime<T>(IEnumerable<State> states) where T : State
        {
            return states.OfType<T>().Sum(state => state.Duration.TotalSeconds);
        }

        public static IEnumerable<(int AlarmCode, double Duration)> TopAlarmCodesByDuration(IEnumerable<State> states, int count = 5)
        {
            return states
                .OfType<Faulted>()
                .GroupBy(fault => fault.AlarmCode)
                .Select(group => (AlarmCode: group.Key, Duration: group.Sum(state => state.Duration.TotalSeconds)))
                .OrderByDescending(alarm => alarm.Duration)
                .Take(count);
        }
    }
}