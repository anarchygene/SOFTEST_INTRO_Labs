using Reqnroll;
using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class UsingCalculatorBasicReliabilitySteps
{
    private readonly CalculatorContext _calculatorContext;
    private readonly ReliabilityContext _reliabilityContext;

    public UsingCalculatorBasicReliabilitySteps(
        CalculatorContext calculatorContext,
        ReliabilityContext reliabilityContext)
    {
        _calculatorContext = calculatorContext;
        _reliabilityContext = reliabilityContext;
    }

    [Given("the initial failure intensity is {double}, expected total failures is {double}, and execution time is {double}")]
    public void GivenTheBasicMusaParameters(
        double initialFailureIntensity,
        double expectedTotalFailures,
        double executionTime)
    {
        _reliabilityContext.InitialFailureIntensity =
            initialFailureIntensity;

        _reliabilityContext.ExpectedTotalFailures =
            expectedTotalFailures;

        _reliabilityContext.ExecutionTime = executionTime;
    }

    [When("I calculate the current failure intensity")]
    public void WhenICalculateTheCurrentFailureIntensity()
    {
        Execute(() =>
            _calculatorContext.Calculator.CalculateCurrentFailureIntensity(
                _reliabilityContext.InitialFailureIntensity,
                _reliabilityContext.ExpectedTotalFailures,
                _reliabilityContext.ExecutionTime));
    }

    [When("I calculate the expected cumulative failures")]
    public void WhenICalculateTheExpectedCumulativeFailures()
    {
        Execute(() =>
            _calculatorContext.Calculator.CalculateExpectedCumulativeFailures(
                _reliabilityContext.InitialFailureIntensity,
                _reliabilityContext.ExpectedTotalFailures,
                _reliabilityContext.ExecutionTime));
    }

    [Then("the Basic Musa calculation should be rejected")]
    public void ThenTheBasicMusaCalculationShouldBeRejected()
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