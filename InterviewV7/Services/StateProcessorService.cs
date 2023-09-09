using InterviewV7.Models;

namespace InterviewV7.Services
{
    public class StateProcessorService : IStateProcessorService
    {
        public IEnumerable<State> ProcessStates(IEnumerable<State> rawStates)
        {
            var stateList = rawStates.ToList();

            return stateList
                        .Zip(stateList.Skip(1), (current, next) =>
                        {
                            current.Duration = next.TimeStamp - current.TimeStamp;
                            return current;
                        })
                        .ToList();
        }
    }
}