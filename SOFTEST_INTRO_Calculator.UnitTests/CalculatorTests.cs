using SOFTEST_INTRO_Calculator;
using NUnit.Framework;
namespace SOFTEST_INTRO_Calculator.UnitTests;

public class CalculatorTests
{
    private Calculator _calculator = null!;
    [SetUp]
    public void SetUp()
    {
        _calculator = new Calculator();
    }
    [Test]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        // Arrange: the calculator is created in SetUp.
        // Act
        double result = _calculator.Add(10, 20);
        // Assert
        Assert.That(result, Is.EqualTo(30));
    }

    [TestCase(0, 0, 0)]
    [TestCase(0, 5, 5)]
    [TestCase(-3, 8, 5)]
    [TestCase(0.1, 0.2, 0.3)]

    public void Add_RepresentativeInputs_ReturnsSum(
    double a, double b, double expected)
    {
        double result = _calculator.Add(a, b);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(1, 11, 7)]
    [TestCase(10, 11, 11)]
    [TestCase(11, 11, 15)]
    [TestCase(1, 1, 3)]
    [TestCase(0, 0, 0)]
    public void Add_BinaryCodedInputs_ReturnsConcatenatedBinaryValue(
    double a, double b, double expected)
    {
        double result = _calculator.Add(a, b);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Subtract_NormalInputs_ReturnsDifference()
    {
        // Exposes an implementation that adds the operands instead of subtracting.
        double result = _calculator.Subtract(14, 5);

        Assert.That(result, Is.EqualTo(9));
    }

    [Test]
    public void Subtract_ZeroFirstInput_PreservesOperandOrder()
    {
        // Exposes reversed operands, which would return 7 instead of -7.
        double result = _calculator.Subtract(0, 7);

        Assert.That(result, Is.EqualTo(-7));
    }

    [Test]
    public void Subtract_NegativeInputs_HandlesSigns()
    {
        // Exposes implementations that subtract absolute values or otherwise lose signs.
        double result = _calculator.Subtract(-8, -3);

        Assert.That(result, Is.EqualTo(-5));
    }

    [Test]
    public void Multiply_NormalInputs_ReturnsProduct()
    {
        // Exposes an implementation that adds the operands instead of multiplying.
        double result = _calculator.Multiply(6, 4);

        Assert.That(result, Is.EqualTo(24));
    }

    [Test]
    public void Multiply_ZeroInput_ReturnsZero()
    {
        // Exposes an implementation that ignores zero or uses addition in place of multiplication.
        double result = _calculator.Multiply(0, 9);

        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void Multiply_NegativeInput_PreservesProductSign()
    {
        // Exposes an implementation that multiplies absolute values and loses the negative sign.
        double result = _calculator.Multiply(-6, 4);

        Assert.That(result, Is.EqualTo(-24));
    }

    // For this lab series, Divide accepts finite double inputs. A zero numerator is valid when the divisor is
    // nonzero. A zero divisor is rejected with ArgumentException, including 0 / 0. This is the application’s rule;
    // C# double division itself can return infinity or NaN instead of throwing. Handling overflow and non-finite
    // inputs inside the arithmetic methods is outside the core exercise.
    [TestCase(1, 2, 0.5)]
    [TestCase(0, 15, 0)]
    [TestCase(15, -3, -5)]
    public void Divide_RepresentativeInputs_ReturnsQuotient(
    double a, double b, double expected)
    {
        double result = _calculator.Divide(a, b);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(15, 0)]
    [TestCase(0, 0)]
    public void Divide_ZeroDivisor_ThrowsArgumentException(double a, double b)
    {
        Assert.That(() => _calculator.Divide(a, b),
        Throws.TypeOf<ArgumentException>());
    }

    // For one behaviour, write a test, see it fail for the intended reason, implement enough to pass, then
    // refactor without changing behaviour. Repeat. If the method does not yet exist, add its signature and a
    // NotImplementedException stub so the test can run. A compilation error is useful feedback, but it is not
    // the intended assertion failure.
    [Test]
    public void Factorial_Zero_ReturnsOne()
    {
        long result = _calculator.Factorial(0);
        Assert.That(result, Is.EqualTo(1L));
    }

    [TestCase(0, 1L)]
    [TestCase(1, 1L)]
    [TestCase(5, 120L)]
    [TestCase(20, 2432902008176640000L)]
    public void Factorial_ValidInput_ReturnsExpectedResult(
    int n,
    long expected)
    {
        long result = _calculator.Factorial(n);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(-1)]
    [TestCase(21)]
    public void Factorial_InvalidInput_ThrowsArgumentException(int n)
    {
        Assert.That(() => _calculator.Factorial(n),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(3, 4, 6)]
    [TestCase(3, 6, 9)]
    [TestCase(4, 24, 48)]
    public void TriangleArea_RepresentativeInput_ReturnsArea(double height, double width, double expected)
    {
        double result = _calculator.TriangleArea(height, width);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(0, 0)]
    [TestCase(0, 5)]
    [TestCase(5, 0)]
    public void TriangleArea_ZeroInput_ReturnsZero(double height, double width)
    {
        double result = _calculator.TriangleArea(height, width);
        Assert.That(result, Is.EqualTo(0));
    }

    [TestCase(-1, 5)]
    [TestCase(5, -1)]
    public void TriangleArea_NegativeInput_ThrowsArgumentException(double height, double width)
    {
        Assert.That(() => _calculator.TriangleArea(height, width),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void TriangleArea_NegativeWidth_IdentifiesWidthParameter()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => _calculator.TriangleArea(5, -1));

        Assert.That(exception!.ParamName, Is.EqualTo("width"));
    }

    [TestCase(1, Math.PI)]
    [TestCase(0, 0)]
    public void CircleArea_RepresentativeInputs_ReturnsArea(double radius, double expected)
    {
        double result = _calculator.CircleArea(radius);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(-1)]
    public void CircleArea_NegativeInput_ThrowsArgumentException(double radius)
    {
        Assert.That(() => _calculator.CircleArea(radius),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(5, 5, 120L)]
    [TestCase(5, 4, 120L)]
    [TestCase(5, 3, 60L)]
    [TestCase(5, 0, 1L)]
    [TestCase(0, 0, 1L)]
    [TestCase(6, 2, 30L)]
    public void UnknownFunctionA_ValidInputs_ReturnsPermutation(int n, int r, long expected)
    {
        long result = _calculator.UnknownFunctionA(n, r);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(5, 5, 1L)]
    [TestCase(5, 4, 5L)]
    [TestCase(5, 3, 10L)]
    [TestCase(5, 0, 1L)]
    [TestCase(0, 0, 1L)]
    [TestCase(6, 2, 15L)]
    public void UnknownFunctionB_ValidInputs_ReturnsCombination(int n, int r, long expected)
    {
        long result = _calculator.UnknownFunctionB(n, r);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(-4, 5)]
    [TestCase(4, 5)]
    [TestCase(21, 5)]
    [TestCase(5, -1)]
    public void UnknownFunctionA_InvalidInputs_ThrowsArgumentOutOfRangeException(int n, int r)
    {
        Assert.That(() => _calculator.UnknownFunctionA(n, r), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(-4, 5)]
    [TestCase(4, 5)]
    [TestCase(21, 5)]
    [TestCase(5, -1)]
    public void UnknownFunctionB_InvalidInputs_ThrowsArgumentOutOfRangeException(int n, int r)
    {
        Assert.That(() => _calculator.UnknownFunctionB(n, r), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(5.9)]
    [TestCase(-1.5)]
    public void DoOperation_FactorialWithFraction_ThrowsArgumentException(double value)
    {
        Assert.That(() => _calculator.DoOperation(value, 0, "f"), Throws.TypeOf<ArgumentException>());
    }

    [TestCase("ua")]
    [TestCase("ub")]
    public void DoOperation_UnknownFunctionWithFraction_ThrowsArgumentException(string operation)
    {
        Assert.That(() => _calculator.DoOperation(5, 2.5, operation), Throws.TypeOf<ArgumentException>());
    }

    // MBTF Tests
    [TestCase(1000, 5, 200)]
    [TestCase(240, 3, 80)]
    public void CalculateMtbf_ValidInputs_ReturnsObservedAverage(
    double operatingTime,
    int failureCount,
    double expected)
    {
        double result = _calculator.CalculateMtbf(
            operatingTime,
            failureCount);

        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(0, 5)]
    [TestCase(-1, 5)]
    [TestCase(100, 0)]
    [TestCase(100, -1)]
    public void CalculateMtbf_InvalidInputs_ThrowsArgumentOutOfRangeException(
        double operatingTime,
        int failureCount)
    {
        Assert.That(
            () => _calculator.CalculateMtbf(
                operatingTime,
                failureCount),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // Availability Tests
    [TestCase(90, 10, 0.9)]
    [TestCase(10, 0, 1)]
    [TestCase(0, 10, 0)]
    public void CalculateAvailability_ValidInputs_ReturnsRatio(
    double mtbf,
    double mttr,
    double expected)
    {
        double result = _calculator.CalculateAvailability(mtbf, mttr);

        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(-1, 10)]
    [TestCase(10, -1)]
    [TestCase(0, 0)]
    public void CalculateAvailability_InvalidInputs_ThrowsArgumentOutOfRangeException(
        double mtbf,
        double mttr)
    {
        Assert.That(
            () => _calculator.CalculateAvailability(mtbf, mttr),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    // Basic Musa Reliability Tests
    [TestCase(10, 100, 0, 10)]
    [TestCase(10, 100, 10, 3.6787944117144233)]
    public void CalculateCurrentFailureIntensity_ValidInputs_ReturnsExpectedResult(
    double initialFailureIntensity,
    double expectedTotalFailures,
    double executionTime,
    double expected)
    {
        double result = _calculator.CalculateCurrentFailureIntensity(
            initialFailureIntensity,
            expectedTotalFailures,
            executionTime);

        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(10, 100, 0, 0)]
    [TestCase(10, 100, 10, 63.212055882855765)]
    public void CalculateExpectedCumulativeFailures_ValidInputs_ReturnsExpectedResult(
        double initialFailureIntensity,
        double expectedTotalFailures,
        double executionTime,
        double expected)
    {
        double result = _calculator.CalculateExpectedCumulativeFailures(
            initialFailureIntensity,
            expectedTotalFailures,
            executionTime);

        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(0, 100, 1)]
    [TestCase(-1, 100, 1)]
    [TestCase(10, 0, 1)]
    [TestCase(10, -1, 1)]
    [TestCase(10, 100, -1)]
    public void CalculateCurrentFailureIntensity_InvalidInput_ThrowsArgumentOutOfRangeException(
        double initialFailureIntensity,
        double expectedTotalFailures,
        double executionTime)
    {
        Assert.That(
            () => _calculator.CalculateCurrentFailureIntensity(
                initialFailureIntensity,
                expectedTotalFailures,
                executionTime),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(0, 100, 1)]
    [TestCase(-1, 100, 1)]
    [TestCase(10, 0, 1)]
    [TestCase(10, -1, 1)]
    [TestCase(10, 100, -1)]
    public void CalculateExpectedCumulativeFailures_InvalidInput_ThrowsArgumentOutOfRangeException(
        double initialFailureIntensity,
        double expectedTotalFailures,
        double executionTime)
    {
        Assert.That(
            () => _calculator.CalculateExpectedCumulativeFailures(
                initialFailureIntensity,
                expectedTotalFailures,
                executionTime),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }
}