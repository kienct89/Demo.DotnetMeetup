using System;
using Add.Program.Handlers;
using Calculator.Messages.Commands;
using Calculator.Messages.Events;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;
// ReSharper disable AccessToDisposedClosure

namespace Add.Program
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var activator = new BuiltinHandlerActivator())
            {
                activator.Register(() => new AddTwoNumbersHandler(activator.Bus));

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

                Console.WriteLine("===== ADD PROGRAM =====");
                Console.WriteLine("Press ENTER to quit");
                Console.ReadLine();
                Console.WriteLine("Quitting...");
            }

        }
    }
}
