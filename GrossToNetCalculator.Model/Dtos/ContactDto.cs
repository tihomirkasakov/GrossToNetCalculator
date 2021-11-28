using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace GrossToNetCalculator.Model.Dtos
{
    public class ContactDto : CacheSettings, IValidatableObject
    {
        private const int POSITIVE_NUMBER = 0;
        private const int NAME_COUNT = 2;
        private const int SSN_LOWER = 5;
        private const int SSN_HIGHER = 10;
        private const string SSN_PATTERN = @"^\d{5,10}$";
        private const string FULL_NAME_PATTERN = @"^[a-zA-Z ]+$";

        public TaxPayerDto TaxPayer { get; set; }
        [Required]
        public string SSN { get; set; }
        public decimal CharitySpent { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TaxPayer.FullName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList().Count < NAME_COUNT)
            {
                yield return new ValidationResult(@$"'Full Name' should contains at least {NAME_COUNT} names");
            }
            if (!Regex.IsMatch(TaxPayer.FullName, FULL_NAME_PATTERN))
            {
                yield return new ValidationResult("'Full Name' should contains only letters or spaces");
            }

            if (!DateTime.TryParse(TaxPayer.DateOfBirth, CultureInfo.InvariantCulture, DateTimeStyles.None, out _) &&
                !string.IsNullOrWhiteSpace(TaxPayer.DateOfBirth))
            {
                yield return new ValidationResult(@"Please enter valid 'Date of Birth'");
            }

            if (TaxPayer.GrossIncome < POSITIVE_NUMBER)
            {
                yield return new ValidationResult(@"'Gross Income' should be a positive number");
            }

            if (!Regex.IsMatch(SSN, SSN_PATTERN))
            {
                yield return new ValidationResult(@$"'SSN' should be between {SSN_LOWER} and {SSN_HIGHER} digits");
            }

            if (CharitySpent < POSITIVE_NUMBER)
            {
                yield return new ValidationResult(@"'Charity Spent' should be a positive number");
            }
        }
    }
}
