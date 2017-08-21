using System;
using System.IO;
using System.Threading.Tasks;
using Calculator.Messages.Commands;
using Calculator.Messages.Events;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Logging;
using Rebus.Persistence.FileSystem;
using Rebus.Routing.TypeBased;

namespace Calculator.Program
{
    class Program
    {
        static readonly string JsonFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rebus_subscriptions.json");

        static void Main()
        {
            using (var activator = new BuiltinHandlerActivator())
            {
                activator.Register(() => new ResultReceivedEventHandler());

                Configure.With(activator)
                    .Logging(l => l.ColoredConsole(LogLevel.Info))
                    .Routing(s => s.TypeBased().MapAssemblyOf<ResultReceivedEvent>("calculator-subscriber"))
                    .Transport(t => t.UseMsmq("calculator-publisher"))
                    .Subscriptions(s => s.UseJsonFile(JsonFilePath))
                    .Start();

                activator.Bus.Subscribe<ResultReceivedEvent>();

                while (true)
                {
                    Console.WriteLine(@"1) Add 1 + 2
q) Quit");

                    var keyChar = char.ToLower(Console.ReadKey(true).KeyChar);

                    switch (keyChar)
                    {
                        case '1':
                            activator.Bus.Publish(new AddTwoNumbersCommand(1, 2)).Wait();
                            break;

                        case 'q':
                            goto consideredHarmful;

                        default:
                            Console.WriteLine("There's no option ({0})", keyChar);
                            break;
                    }
                }

                consideredHarmful:
                Console.WriteLine("Quitting!");
            }
        }
    }

    public class ResultReceivedEventHandler : IHandleMessages<ResultReceivedEvent>
    {
        public async Task Handle(ResultReceivedEvent message)
        {
            Console.WriteLine(message.Result);
        }
    }
}
