using GrossToNetCalculator.Data.Repositories.Interfaces;
using GrossToNetCalculator.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GrossToNetCalculator.Data.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        public List<Country> GetAll()
        {
            List<Country> countries = new List<Country>()
            {
                new Country()
                {
                    Id=1,
                    Name="Imaginaria",
                    IsActive=true,
                    CurrencyId=1,
                    Currency=new Currency()
                    {
                        Id=1,
                        Name="Imagiaria Dolar",
                        ShortName="IDR",
                        IsActive=true
                    },
                    TaxationRuleId=1,
                    TaxationRule=new TaxationRule()
                    {
                        Id=1,
                        NoTaxationValue=1000,
                        IncomeTaxPercentage=10,
                        SocialContributionPercentage=15,
                        SocialContributionValue=3000,
                        CharitySpentPercentage=10
                    }
                }
            };

            return countries;
        }

        public Country GetCountryByCurrency(string currencyShortName)
        {
            return this.GetAll().FirstOrDefault(c => c.Currency.ShortName.ToLower().Equals(currencyShortName));
        }
    }
}
