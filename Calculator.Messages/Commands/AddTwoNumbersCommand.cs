namespace Calculator.Messages.Commands
{
    public class AddTwoNumbersCommand
    {
        public AddTwoNumbersCommand(int first, int second)
        {
            First = first;
            Second = second;
        }

        public int First { get; }
        public int Second { get; }
    }
}