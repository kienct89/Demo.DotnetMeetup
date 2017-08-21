namespace Calculator.Messages.Events
{
    public class ResultReceivedEvent
    {
        public ResultReceivedEvent(double? result)
        {
            Result = result;
        }

        public double? Result { get; }
    }
}