using System.ComponentModel.DataAnnotations;

namespace GrossToNetCalculator.Model.Dtos
{
    public class TaxPayerDto
    {
        [Required]
        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
        [Required]
        public decimal GrossIncome { get; set; }
    }
}
