using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace GrossToNetCalculator.Model.Dtos
{
    public class TaxPayerDto
    {
        private const int POSITIVE_NUMBER = 0;
        private const int NAME_COUNT = 2;
        private const string FULL_NAME_PATTERN = @"^[a-zA-Z ]+$";

        [Required]
        public string FullName { get; set; }

        public string DateOfBirth { get; set; }

        [Required]
        public decimal GrossIncome { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (FullName.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList().Count < NAME_COUNT)
        //    {
        //        yield return new ValidationResult(@$"'Full Name' should contains at least {NAME_COUNT} names");
        //    }
        //    if (!Regex.IsMatch(FullName, FULL_NAME_PATTERN))
        //    {
        //        yield return new ValidationResult("'Full Name' should contains only letters or spaces");
        //    }

        //    if (!DateTime.TryParse(DateOfBirth, CultureInfo.InvariantCulture, DateTimeStyles.None, out _) &&
        //        !string.IsNullOrWhiteSpace(DateOfBirth))
        //    {
        //        yield return new ValidationResult(@"Please enter valid 'Date of Birth'");
        //    }

        //    if (GrossIncome < POSITIVE_NUMBER)
        //    {
        //        yield return new ValidationResult(@"'Gross Income' should be a positive number");
        //    }
        //}
    }
}
