using System;
using System.Threading.Tasks;
using Calculator.Messages.Events;
using Rebus.Handlers;
#pragma warning disable 1998

namespace Calculator.Program.Handlers
{
    public class ResultReceivedEventHandler : IHandleMessages<ResultReceivedEvent>
    {
        public async Task Handle(ResultReceivedEvent message)
        {
            Console.WriteLine(message.Result);
        }
    }
}