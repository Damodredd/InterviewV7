namespace InterviewV7.Models
{
    public abstract class State
    {
        public DateTime TimeStamp { get; set; }
        public abstract string StateType { get; }
    }
}