using GrossToNetCalculator.Model.Dtos;

namespace GrossToNetCalculator.Service.Interfaces
{
    public interface ICalculatorService
    {
        ContactTaxesDto CalculateContactTaxes(ContactDto model);
    }
}
