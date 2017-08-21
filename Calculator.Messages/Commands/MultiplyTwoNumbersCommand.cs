namespace Calculator.Messages.Commands
{
    public class MultiplyTwoNumbersCommand
    {
        public MultiplyTwoNumbersCommand(int first, int second)
        {
            First = first;
            Second = second;
        }

        public int First { get; }
        public int Second { get; }
    }
}