namespace SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

public sealed class ReliabilityContext
{
    public double Mtbf { get; set; }
    public double Mttr { get; set; }

    public double InitialFailureIntensity { get; set; }
    public double ExpectedTotalFailures { get; set; }
    public double ExecutionTime { get; set; }
}