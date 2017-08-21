using System;
using Add.Program.Handlers;
using Calculator.Messages.Commands;
using Calculator.Messages.Events;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;

namespace Add.Program
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var activator = new BuiltinHandlerActivator())
            {
                activator.Register(() => new AddTwoNumbersHandler(activator.Bus));

                //                activator.Handle<AddTwoNumbersCommand>(async msg =>
                //                {
                //                    await activator.Bus.Publish(new ResultReceivedEvent(msg.First + msg.Second));
                //                });

                Configure.With(activator)
                    .Logging(l => l.ColoredConsole())
                    .Transport(t =>
                    {
                        t.UseMsmq("calculator-subscriber");
                    })
                    .Routing(r => r.TypeBased().MapAssemblyOf<AddTwoNumbersCommand>("calculator-publisher"))
                    .Subscriptions(s => s.StoreInMemory())
                    .Start();

                activator.Bus.Subscribe<AddTwoNumbersCommand>().Wait();

                Console.WriteLine("This is Subscriber 1");
                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
                Console.WriteLine("Quitting...");
            }

        }
    }
}
