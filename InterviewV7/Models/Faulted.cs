namespace InterviewV7.Models
{
    public class Faulted : State
    {
        public int AlarmCode { get; set; }
        public override string StateType => "faulted";
    }
}