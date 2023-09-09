using InterviewV7.Models;

namespace InterviewV7.Services
{
    public class StateService : IStateService
    {
        public IEnumerable<State> GetStates(string path)
        {
            // Logic to read and parse the CSV goes here.
            var lines = File.ReadLines(path).Skip(1);  // Skip the header.
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length < 2) continue;

                if (parts[0] == "running")
                {
                    yield return new Running { TimeStamp = DateTime.Parse(parts[1]) };
                }
                else if (parts[0] == "faulted" && parts.Length == 3)
                {
                    yield return new Faulted { TimeStamp = DateTime.Parse(parts[1]), AlarmCode = int.Parse(parts[2]) };
                }
            }
        }
    }
}