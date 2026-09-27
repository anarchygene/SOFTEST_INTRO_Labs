namespace SOFTEST_INTRO_Calculator;

public class Calculator
{
    private static bool ViolatesBounds(int n) => n < 0 || n > 20;

    private static int RequireInteger(double value, string parameterName)
    {
        if (!double.IsFinite(value) || value != Math.Truncate(value) || value < int.MinValue || value > int.MaxValue)
            throw new ArgumentException($"{parameterName} must be a whole number that is within the range of an integer.", parameterName);

        return checked((int)value);
    }

    public double Add(double a, double b)
    {
        if (!LooksBinaryCoded(a) || !LooksBinaryCoded(b))
            return a + b;

        string combined =
            a.ToString("F0", System.Globalization.CultureInfo.InvariantCulture) +
            b.ToString("F0", System.Globalization.CultureInfo.InvariantCulture);

        double result = 0;

        foreach (char digit in combined)
            result = result * 2 + (digit - '0');

        return result;
    }

    private static bool LooksBinaryCoded(double value)
    {
        if (!double.IsFinite(value) || value < 0 || value != Math.Truncate(value))
            return false;

        string digits = value.ToString(
            "F0",
            System.Globalization.CultureInfo.InvariantCulture);

        foreach (char digit in digits)
            if (digit != '0' && digit != '1')
                return false;

        return true;
    }
    public double Subtract(double a, double b) => a - b;
    public double Multiply(double a, double b) => a * b;
    // Starter version: complete the zero-divisor rule in section 5.
    public double Divide(double a, double b)
    {
        if (b == 0)
            throw new ArgumentException("Cannot divide by zero.");
        return a / b;
    }
    public long Factorial(int n)
    {
        if (ViolatesBounds(n))
            throw new ArgumentOutOfRangeException(nameof(n), "Factorial is not defined for negative numbers or numbers greater than 20.");
        long result = 1;
        for (int i = 2; i <= n; i++)
            result *= i;
        return result;
    }
    public double TriangleArea(double height, double width)
    {
        if (height < 0)
            throw new ArgumentOutOfRangeException(nameof(height), "Height must be non-negative.");
        if (width < 0)
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be non-negative.");
        return height * width / 2;
    }
    public double CircleArea(double radius)
    {
        if (radius < 0)
            throw new ArgumentOutOfRangeException(nameof(radius), "Radius must be non-negative.");
        return Math.PI * radius * radius;
    }
    public long UnknownFunctionA(int n, int r)
    {
        ValidateUnknownFunctionArguments(n, r);
        return Factorial(n) / Factorial(n - r);
    }
    public long UnknownFunctionB(int n, int r)
    {
        ValidateUnknownFunctionArguments(n, r);
        return Factorial(n) / (Factorial(r) * Factorial(n - r));
    }

    private static void ValidateUnknownFunctionArguments(int n, int r)
    {
        if (ViolatesBounds(n))
            throw new ArgumentOutOfRangeException(nameof(n), "n must be between 0 and 20.");

        if (ViolatesBounds(r))
            throw new ArgumentOutOfRangeException(nameof(r), "r must be between 0 and 20.");

        if (r > n)
            throw new ArgumentOutOfRangeException(nameof(r), "r cannot be greater than n.");
    }

    public double CalculateMtbf(double operatingTime, int failureCount)
    {
        if (!double.IsFinite(operatingTime) || operatingTime <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(operatingTime),
                "Operating time must be a positive finite number.");

        if (failureCount <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(failureCount),
                "Failure count must be positive.");

        return operatingTime / failureCount;
    }

    public double CalculateAvailability(double mtbf, double mttr)
    {
        ValidateNonNegativeFinite(mtbf, nameof(mtbf));
        ValidateNonNegativeFinite(mttr, nameof(mttr));

        double denominator = mtbf + mttr;

        if (denominator <= 0 || !double.IsFinite(denominator))
            throw new ArgumentOutOfRangeException(
                nameof(mtbf),
                "MTBF and MTTR must produce a positive finite denominator.");

        return mtbf / denominator;
    }

    public double CalculateCurrentFailureIntensity(
        double initialFailureIntensity,
        double expectedTotalFailures,
        double executionTime)
    {
        ValidateBasicMusaInputs(
            initialFailureIntensity,
            expectedTotalFailures,
            executionTime);

        return initialFailureIntensity * Math.Exp(
            -initialFailureIntensity * executionTime /
            expectedTotalFailures);
    }

    public double CalculateExpectedCumulativeFailures(
        double initialFailureIntensity,
        double expectedTotalFailures,
        double executionTime)
    {
        ValidateBasicMusaInputs(
            initialFailureIntensity,
            expectedTotalFailures,
            executionTime);

        return expectedTotalFailures * (
            1 - Math.Exp(
                -initialFailureIntensity * executionTime /
                expectedTotalFailures));
    }

    private static void ValidateNonNegativeFinite(
        double value,
        string parameterName)
    {
        if (!double.IsFinite(value) || value < 0)
            throw new ArgumentOutOfRangeException(parameterName,
                "The value must be finite and non-negative.");
    }

    private static void ValidateBasicMusaInputs(
        double initialFailureIntensity,
        double expectedTotalFailures,
        double executionTime)
    {
        if (!double.IsFinite(initialFailureIntensity) || initialFailureIntensity <= 0)
            throw new ArgumentOutOfRangeException(nameof(initialFailureIntensity),
                "Initial failure intensity must be positive and finite.");

        if (!double.IsFinite(expectedTotalFailures) || expectedTotalFailures <= 0)
            throw new ArgumentOutOfRangeException(nameof(expectedTotalFailures),
                "Expected total failures must be positive and finite.");

        if (!double.IsFinite(executionTime) || executionTime < 0)
            throw new ArgumentOutOfRangeException(nameof(executionTime),
                "Execution time must be finite and non-negative.");
    }
    public double DoOperation(double a, double b, string op)
    {
        return op switch
        {
            "a" => Add(a, b),
            "s" => Subtract(a, b),
            "m" => Multiply(a, b),
            "d" => Divide(a, b),
            "f" => Factorial(RequireInteger(a, nameof(a))), // Assuming 'a' is the number for factorial
            "t" => TriangleArea(a, b), // Assuming 'a' is height and 'b' is width
            "c" => CircleArea(a), // Assuming 'a' is the radius
            "ua" => UnknownFunctionA(RequireInteger(a, nameof(a)), RequireInteger(b, nameof(b))), // Assuming 'a' is n and 'b' is r
            "ub" => UnknownFunctionB(RequireInteger(a, nameof(a)), RequireInteger(b, nameof(b))), // Assuming 'a' is n and 'b' is r
            _ => throw new ArgumentException("Unknown operation.")
        };
    }
}