using GrossToNetCalculator.Data.Repositories.Interfaces;
using GrossToNetCalculator.Model.Dtos;
using GrossToNetCalculator.Model.Entities;
using GrossToNetCalculator.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GrossToNetCalculator.Tests
{
    public class CalculatorServiceTests
    {
        private const string CURRENCY_SHORT_CODE_LOWER_CASE = "idr";

        [Fact]
        public async Task Should_Return_Same_GrossIncome_As_Posted_Model()
        {
            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(r => r.GetCountryByCurrency(CURRENCY_SHORT_CODE_LOWER_CASE))
                .Returns(GetTestData(CURRENCY_SHORT_CODE_LOWER_CASE));

            var service = new CalculatorService(mockRepo.Object);

            var contact = new ContactDto()
            {
                TaxPayer = new TaxPayerDto()
                {
                    FullName = "Ivanka Petrova",
                    DateOfBirth = new DateTime(year: 1999, month: 9, day: 29).ToString(),
                    GrossIncome = 1234.123235325M
                },
                SSN = "123456"
            };

            var contactTaxes = service.CalculateContactTaxes(contact);

            Assert.Equal(1234.123235325M, contactTaxes.GrossIncome);
        }

        [Fact]
        public async Task Should_Return_Zero_Taxes_For_Gross_Income_Lower_Or_Equal_To_Specified_Country_Taxation_Rule()
        {
            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(r => r.GetCountryByCurrency(CURRENCY_SHORT_CODE_LOWER_CASE))
                .Returns(GetTestData(CURRENCY_SHORT_CODE_LOWER_CASE));

            var service = new CalculatorService(mockRepo.Object);

            var contact = new ContactDto()
            {
                TaxPayer = new TaxPayerDto()
                {
                    FullName = "Ivanka Petrova",
                    DateOfBirth = new DateTime(year: 1999, month: 9, day: 29).ToString(),
                    GrossIncome = 1000
                },
                SSN = "123456"
            };

            var contactTaxes = service.CalculateContactTaxes(contact);

            Assert.Equal(0, contactTaxes.IncomeTax);
            Assert.Equal(0, contactTaxes.SocialTax);
            Assert.Equal(0, contactTaxes.TotalTax);
        }

        [Fact]
        public async Task Should_Not_Return_Zero_Taxes_For_Gross_Income_Lower_Or_Equal_To_Specified_Country_Taxation_Rule()
        {
            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(r => r.GetCountryByCurrency(CURRENCY_SHORT_CODE_LOWER_CASE))
                .Returns(GetTestData(CURRENCY_SHORT_CODE_LOWER_CASE));

            var service = new CalculatorService(mockRepo.Object);

            var contact = new ContactDto()
            {
                TaxPayer = new TaxPayerDto()
                {
                    FullName = "Ivanka Petrova",
                    DateOfBirth = new DateTime(year: 1999, month: 9, day: 29).ToString(),
                    GrossIncome = 1000.0000000001M
                },
                SSN = "123456"
            };

            var contactTaxes = service.CalculateContactTaxes(contact);

            Assert.NotEqual(0, contactTaxes.IncomeTax);
            Assert.NotEqual(0, contactTaxes.SocialTax);
            Assert.NotEqual(0, contactTaxes.TotalTax);
        }

        [Fact]
        public async Task Test_Case_With_Gross_Income_Lower_Or_Equal_To_Specified_Country_Taxation_Rule()
        {
            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(r => r.GetCountryByCurrency(CURRENCY_SHORT_CODE_LOWER_CASE))
                .Returns(GetTestData(CURRENCY_SHORT_CODE_LOWER_CASE));

            var service = new CalculatorService(mockRepo.Object);

            var contact = new ContactDto()
            {
                TaxPayer = new TaxPayerDto()
                {
                    FullName = "Ivanka Petrova",
                    DateOfBirth = new DateTime(year: 1999, month: 9, day: 29).ToString(),
                    GrossIncome = 980
                },
                SSN = "123456"
            };

            var contactTaxes = service.CalculateContactTaxes(contact);

            Assert.Equal(0, contactTaxes.CharitySpent);
            Assert.Equal(980, contactTaxes.GrossIncome);
            Assert.Equal(0, contactTaxes.IncomeTax);
            Assert.Equal(980, contactTaxes.NetIncome);
            Assert.Equal(0, contactTaxes.SocialTax);
            Assert.Equal(0, contactTaxes.TotalTax);
        }

        [Fact]
        public async Task Test_Case_With_Gross_Income_Higher_Then_Specified_Country_Taxation_Rule()
        {
            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(r => r.GetCountryByCurrency(CURRENCY_SHORT_CODE_LOWER_CASE))
                .Returns(GetTestData(CURRENCY_SHORT_CODE_LOWER_CASE));

            var service = new CalculatorService(mockRepo.Object);

            var contact = new ContactDto()
            {
                TaxPayer = new TaxPayerDto()
                {
                    FullName = "Ivanka Petrova",
                    DateOfBirth = new DateTime(year: 1999, month: 9, day: 29).ToString(),
                    GrossIncome = 3400
                },
                SSN = "123456"
            };

            var contactTaxes = service.CalculateContactTaxes(contact);

            Assert.Equal(0, contactTaxes.CharitySpent);
            Assert.Equal(3400, contactTaxes.GrossIncome);
            Assert.Equal(240, contactTaxes.IncomeTax);
            Assert.Equal(2860, contactTaxes.NetIncome);
            Assert.Equal(300, contactTaxes.SocialTax);
            Assert.Equal(540, contactTaxes.TotalTax);
        }

        [Fact]
        public async Task Test_Case_With_Gross_Income_Higher_And_Charity_Spent_Lower_Then_Specified_Country_Taxation_Rule()
        {
            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(r => r.GetCountryByCurrency(CURRENCY_SHORT_CODE_LOWER_CASE))
                .Returns(GetTestData(CURRENCY_SHORT_CODE_LOWER_CASE));

            var service = new CalculatorService(mockRepo.Object);

            var contact = new ContactDto()
            {
                TaxPayer = new TaxPayerDto()
                {
                    FullName = "Ivanka Petrova",
                    DateOfBirth = new DateTime(year: 1999, month: 9, day: 29).ToString(),
                    GrossIncome = 2500
                },
                SSN = "123456",
                CharitySpent=150
            };

            var contactTaxes = service.CalculateContactTaxes(contact);

            Assert.Equal(150, contactTaxes.CharitySpent);
            Assert.Equal(2500, contactTaxes.GrossIncome);
            Assert.Equal(135, contactTaxes.IncomeTax);
            Assert.Equal(2162.5M, contactTaxes.NetIncome);
            Assert.Equal(202.5M, contactTaxes.SocialTax);
            Assert.Equal(337.5M, contactTaxes.TotalTax);
        }

        [Fact]
        public async Task Test_Case_With_Gross_Income_Higher_And_Charity_Spent_Higher_Then_Specified_Country_Taxation_Rule()
        {
            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(r => r.GetCountryByCurrency(CURRENCY_SHORT_CODE_LOWER_CASE))
                .Returns(GetTestData(CURRENCY_SHORT_CODE_LOWER_CASE));

            var service = new CalculatorService(mockRepo.Object);

            var contact = new ContactDto()
            {
                TaxPayer = new TaxPayerDto()
                {
                    FullName = "Ivanka Petrova",
                    DateOfBirth = new DateTime(year: 1999, month: 9, day: 29).ToString(),
                    GrossIncome = 3600
                },
                SSN = "123456",
                CharitySpent = 520
            };

            var contactTaxes = service.CalculateContactTaxes(contact);

            Assert.Equal(520, contactTaxes.CharitySpent);
            Assert.Equal(3600, contactTaxes.GrossIncome);
            Assert.Equal(224, contactTaxes.IncomeTax);
            Assert.Equal(3076, contactTaxes.NetIncome);
            Assert.Equal(300, contactTaxes.SocialTax);
            Assert.Equal(524, contactTaxes.TotalTax);
        }

        [Fact]
        public async Task Test_Getting_Country_By_Non_Existing_Currency_Short_Name()
        {
            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(r => r.GetCountryByCurrency("non existing currency"))
                .Returns(GetTestData("non existing currency"));

            var service = new CalculatorService(mockRepo.Object);

            var contact = new ContactDto()
            {
                TaxPayer = new TaxPayerDto()
                {
                    FullName = "Ivanka Petrova",
                    DateOfBirth = new DateTime(year: 1999, month: 9, day: 29).ToString(),
                    GrossIncome = 3600
                },
                SSN = "123456",
                CharitySpent = 520
            };

            var contactTaxes = service.CalculateContactTaxes(contact);

            Assert.Null(contactTaxes);
        }


        private Country GetTestData(string currencyShortName)
        {
            return this.GetAll().FirstOrDefault(c => c.Currency.ShortName.ToLower().Equals(currencyShortName));
        }

        private List<Country> GetAll()
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

    }
}
