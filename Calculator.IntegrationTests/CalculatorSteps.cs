using Add.Program.Handlers;
using Autofac.Extras.Moq;
using Calculator.Messages.Events;
using Calculator.Program.Handlers;
using Calculator.Program.Services;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Transport.InMem;
using TechTalk.SpecFlow;

namespace Calculator.IntegrationTests
{
    [Binding]
    public class CalculatorSteps
    {
        private CalculatorService _calculatorService;
        private IBus _calculatorBus;
        private IBus _addBus;

        protected static IFixture Fixture;
        protected static AutoMock Mock;

        private static IBus CreateCalculatorBus()
        {
            var bus = Configure
                .With(GetCalculatorAdapter())
                .Logging(l => l.None())
                .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "calculator"))
                .Subscriptions(s => s.StoreInMemory())
                .Start();

            return bus;
        }

        private static IBus CreateAddBus()
        {
            var bus = Configure
                .With(GetAddAdapter())
                .Logging(l => l.None())
                .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "add"))
                .Subscriptions(s => s.StoreInMemory())
                .Start();

            return bus;
        }

        private static IContainerAdapter GetCalculatorAdapter()
        {
            var activator =  new BuiltinHandlerActivator();

            activator.Register(() => new ResultReceivedEventHandler());

            return activator;
        }

        private static IContainerAdapter GetAddAdapter()
        {
            var activator = new BuiltinHandlerActivator();

            activator.Register(() => new AddTwoNumbersHandler(activator.Bus));

            return activator;
        }

        [BeforeScenario]
        public void SetUpBeforeScenario()
        {
            _addBus = CreateAddBus();

            _calculatorBus = CreateCalculatorBus();

            _calculatorService = new CalculatorService(_calculatorBus);
        }

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int first)
        {
            ScenarioContext.Current.Set(first, "first");
        }

        [Given(@"I also have entered (.*) into the calculator")]
        public void GivenIAlsoHaveEnteredIntoTheCalculator(int second)
        {
            ScenarioContext.Current.Set(second, "second");
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            _calculatorService.Add(ScenarioContext.Current.Get<int>("first"), ScenarioContext.Current.Get<int>("second"));
        }

        [Then(@"(.*) event should be dispatched to the event bus")]
        public void ThenEventShouldBeDispatchedToTheEventBus(int eventCount)
        {
            // Fail here
            Assert.Fail("Fail because of I'm bad at writing test codes");
        }
    }
}
