using InterviewV7.Models;

namespace InterviewV7.Services
{
    internal interface IStateProcessorService
    {
        IEnumerable<State> ProcessStates(IEnumerable<State> rawStates);
    }
}