using System.Linq;
using Autofac.Extras.Moq;
using Calculator.Messages.Commands;
using Calculator.Program.Services;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Rebus.Testing;
using Rebus.Testing.Events;
using TechTalk.SpecFlow;

namespace Calculator.UnitTests
{
    [Binding]
    public class CalculatorSteps
    {
        private CalculatorService _calculatorService;
        private FakeBus _fakeBus;

        protected static IFixture Fixture;
        protected static AutoMock Mock;

        [BeforeScenario]
        public void SetUpBeforeScenario()
        {
            _fakeBus = new FakeBus();
            _calculatorService = new CalculatorService(_fakeBus);
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
            // Verify
            var addCommand = _fakeBus.Events.OfType<MessagePublished<AddTwoNumbersCommand>>();
            Assert.That(addCommand.Count(), Is.EqualTo(eventCount), "Add command is dispatched to the bus");
        }
    }
}
