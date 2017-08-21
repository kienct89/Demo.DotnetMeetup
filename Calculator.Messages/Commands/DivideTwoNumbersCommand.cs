namespace Calculator.Messages.Commands
{
    public class DivideTwoNumbersCommand
    {
        public DivideTwoNumbersCommand(int first, int second)
        {
            First = first;
            Second = second;
        }

        public int First { get; }
        public int Second { get; }
    }
}