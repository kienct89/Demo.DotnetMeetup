namespace Calculator.Messages.Commands
{
    public class SubtractTwoNumbersCommand
    {
        public SubtractTwoNumbersCommand(int first, int second)
        {
            First = first;
            Second = second;
        }

        public int First { get; }
        public int Second { get; }
    }
}