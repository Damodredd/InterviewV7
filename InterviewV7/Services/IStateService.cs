using InterviewV7.Models;

namespace InterviewV7.Services
{
    public interface IStateService
    {
        IEnumerable<State> GetStates(string path);
    }
}