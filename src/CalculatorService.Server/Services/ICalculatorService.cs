namespace Manal_Calculator.src.CalculatorService.Server.Services;

public interface ICalculatorService
{
    double Add(double a, double b);
    double Sub(double a, double b);
    double Mul(double a, double b);
    double Div(double a, double b);
    double Sqrt(double a);
}