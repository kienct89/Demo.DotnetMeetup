using Calculator.Messages.Commands;
using Rebus.Bus;

namespace Calculator.Program.Services
{
    public class CalculatorService
    {
        private readonly IBus _bus;

        public CalculatorService(IBus bus)
        {
            _bus = bus;
        }

        public void Add(int first, int second)
        {
            _bus.Publish(new AddTwoNumbersCommand(first, second)).Wait();
        }
    }
}
