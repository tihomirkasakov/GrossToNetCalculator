using GrossToNetCalculator.Model.Entities;
using System.Collections.Generic;

namespace GrossToNetCalculator.Data.Repositories.Interfaces
{
    public interface ICountryRepository
    {
        List<Country> GetAll();
        Country GetCountryByCurrency(string currencyShortName);
    }
}
