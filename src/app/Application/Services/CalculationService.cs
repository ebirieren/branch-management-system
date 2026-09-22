using Application.Interfaces.Services;

namespace Application.Services;

public class CalculationService : ICalculationService
{
    public decimal calculatePriceAfterTax(decimal price, int taxRate)
    {
        var taxCost = price / 100;
        taxCost = taxCost * taxRate;

        return taxCost += price;
    }
}