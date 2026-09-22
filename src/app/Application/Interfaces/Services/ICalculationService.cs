namespace Application.Interfaces.Services;

public interface ICalculationService
{
    public decimal calculatePriceAfterTax(decimal price, int taxRate);
}