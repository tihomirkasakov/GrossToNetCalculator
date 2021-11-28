using GrossToNetCalculator.Data.Repositories.Interfaces;
using GrossToNetCalculator.Model.Dtos;
using GrossToNetCalculator.Model.Entities;
using GrossToNetCalculator.Service.Interfaces;

namespace GrossToNetCalculator.Service
{
    public class CalculatorService : ICalculatorService
    {
        private const string CURRENCY_SHORT_CODE_LOWER_CASE = "idr";
        private readonly ICountryRepository _countryRepository;

        public CalculatorService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public ContactTaxesDto CalculateContactTaxes(ContactDto model)
        {
            Country country = _countryRepository.GetCountryByCurrency(CURRENCY_SHORT_CODE_LOWER_CASE);
            if (country==null)
            {
                return null;
            }
            (decimal charitySpend, decimal incomeTax, decimal socialTax) taxes = CalculateTaxes(model, country);
            ContactTaxesDto contactTaxes = new ContactTaxesDto()
            {
                GrossIncome = model.TaxPayer.GrossIncome,
                CharitySpent = model.CharitySpent,
                IncomeTax = taxes.incomeTax,
                SocialTax = taxes.socialTax,
                TotalTax = SumTaxes(taxes),
                NetIncome = CalculateNetIncome(model, taxes),
            };

            return contactTaxes;
        }

        private (decimal charitySpent, decimal incomeTax, decimal socialTax) CalculateTaxes(ContactDto model, Country country)
        {
            if (model.TaxPayer.GrossIncome <= country.TaxationRule.NoTaxationValue)
            {
                return (default, default, default);
            }
            else
            {
                decimal allowedCharitySpent = CalculateTaxAfterTaxPercentage(model.TaxPayer?.GrossIncome, country.TaxationRule.CharitySpentPercentage);
                decimal charitySpent = model.CharitySpent > allowedCharitySpent ? allowedCharitySpent : model.CharitySpent;
                decimal incomeTax = CalculateIncomeTax(model, country, charitySpent);
                decimal socialTax = CalculateSocialTax(model, country, charitySpent);
                return (charitySpent, incomeTax, socialTax);
            }
        }

        private decimal CalculateIncomeTax(ContactDto model, Country country, decimal charitySpent)
        {
            decimal taxableGross = CalculateTaxableGross(model.TaxPayer.GrossIncome, country.TaxationRule.NoTaxationValue, charitySpent);
            return CalculateTaxAfterTaxPercentage(taxableGross, country.TaxationRule.IncomeTaxPercentage);
        }

        private decimal CalculateSocialTax(ContactDto model, Country country, decimal charitySpent)
        {
            decimal taxableGross = model.TaxPayer.GrossIncome - charitySpent;
            decimal higherSocialContribution = taxableGross > country.TaxationRule.SocialContributionValue ? country.TaxationRule.SocialContributionValue : taxableGross;
            return CalculateTaxAfterTaxPercentage(CalculateTaxableGross(higherSocialContribution, country.TaxationRule.NoTaxationValue, default), country.TaxationRule.SocialContributionPercentage);
        }

        private decimal SumTaxes((decimal charitySpend, decimal incomeTax, decimal socialTax) taxes)
        {
            return taxes.incomeTax + taxes.socialTax;
        }

        private decimal CalculateNetIncome(ContactDto model, (decimal charitySpend, decimal incomeTax, decimal socialTax) taxes)
        {
            return model.TaxPayer.GrossIncome - SumTaxes(taxes);
        }

        private decimal CalculateTaxAfterTaxPercentage(decimal? value, double? taxPercentage)
        {
            return (value.Value * (decimal)taxPercentage.Value) / 100m;
        }

        private decimal CalculateTaxableGross(decimal grossIncome, decimal noTaxationValue, decimal charitySpent)
        {
            return grossIncome - noTaxationValue - charitySpent;
        }
    }
}
