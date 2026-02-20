namespace Manal_Calculator.src.CalculatorService.Server.Services;

public class CalculatorService : ICalculatorService
{
    public double Add(double a, double b)
        => a + b;

    public double Sub(double a, double b)
        => a - b;

    public double Mul(double a, double b)
        => a * b;

    public double Div(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException();

        return a / b;
    }

    public double Sqrt(double a)
    {
        if (a < 0)
            throw new ArgumentException("Negative sqrt");

        return Math.Sqrt(a);
    }
}