using System.Globalization;
using Reqnroll;
using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class UsingCalculatorAvailabilitySteps
{
    private readonly CalculatorContext _calculatorContext;
    private readonly ReliabilityContext _reliabilityContext;

    public UsingCalculatorAvailabilitySteps(
        CalculatorContext calculatorContext,
        ReliabilityContext reliabilityContext)
    {
        _calculatorContext = calculatorContext;
        _reliabilityContext = reliabilityContext;
    }

    [When("I have entered {double} and {int} into the calculator and press MTBF")]
    public void WhenICalculateMtbf(
        double operatingTime,
        int failureCount)
    {
        Execute(() => _calculatorContext.Calculator.CalculateMtbf(
            operatingTime,
            failureCount));
    }

    [When("I have entered {double} and {double} into the calculator and press Availability")]
    public void WhenICalculateAvailability(double mtbf, double mttr)
    {
        Execute(() => _calculatorContext.Calculator.CalculateAvailability(
            mtbf,
            mttr));
    }

    [Given("the reliability values are")]
    public void GivenTheReliabilityValuesAre(DataTable table)
    {
        var values = table.Rows[0];

        _reliabilityContext.Mtbf = double.Parse(
            values["MTBF"],
            CultureInfo.InvariantCulture);

        _reliabilityContext.Mttr = double.Parse(
            values["MTTR"],
            CultureInfo.InvariantCulture);
    }

    [When("I calculate Availability from these values")]
    public void WhenICalculateAvailabilityFromTheseValues()
    {
        Execute(() => _calculatorContext.Calculator.CalculateAvailability(
            _reliabilityContext.Mtbf,
            _reliabilityContext.Mttr));
    }

    [Then("the reliability calculation should be rejected")]
    public void ThenTheReliabilityCalculationShouldBeRejected()
    {
        Assert.That(
            _calculatorContext.Error,
            Is.TypeOf<ArgumentOutOfRangeException>());
    }

    private void Execute(Func<double> calculation)
    {
        _calculatorContext.Result = null;
        _calculatorContext.Error = null;

        try
        {
            _calculatorContext.Result = calculation();
        }
        catch (ArgumentOutOfRangeException error)
        {
            _calculatorContext.Error = error;
        }
    }
}