using System.Threading.Tasks;
using Calculator.Messages.Commands;
using Calculator.Messages.Events;
using Rebus.Bus;
using Rebus.Handlers;

namespace Add.Program.Handlers
{
    public class AddTwoNumbersHandler : IHandleMessages<AddTwoNumbersCommand>
    {
        private readonly IBus _bus;

        public AddTwoNumbersHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(AddTwoNumbersCommand message)
        {
            await _bus.Publish(new ResultReceivedEvent(message.First + message.Second));
        }
    }
}