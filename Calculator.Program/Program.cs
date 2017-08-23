using System;
using System.IO;
using Calculator.Messages.Commands;
using Calculator.Messages.Events;
using Calculator.Program.Handlers;
using Calculator.Program.Services;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Logging;
using Rebus.Persistence.FileSystem;
using Rebus.Routing.TypeBased;

namespace Calculator.Program
{
    class Program
    {
        static readonly string JsonFilePath = 
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rebus_subscriptions.json");

        private static CalculatorService _calculatorService;

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

                _calculatorService = new CalculatorService(activator.Bus);

                while (true)
                {
                    Console.WriteLine(@"1) Add 1 + 2
2) Add 2 + 3
q) Quit");

                    var keyChar = char.ToLower(Console.ReadKey(true).KeyChar);

                    switch (keyChar)
                    {
                        case '1':
                            _calculatorService.Add(1, 2);
                            break;

                        case '2':
                            _calculatorService.Add(2, 3);
                            break;

                        case 'q':
                            goto consideredHarmful;

                        default:
                            Console.WriteLine("Invalid key ({0})", keyChar);
                            break;
                    }
                }

                consideredHarmful:
                Console.WriteLine("Quitting!");
            }
        }
    }
}
