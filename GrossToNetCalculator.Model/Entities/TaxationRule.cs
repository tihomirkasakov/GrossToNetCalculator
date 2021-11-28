namespace GrossToNetCalculator.Model.Entities
{
    public class TaxationRule : BaseEntity
    {
        public decimal NoTaxationValue { get; set; }
        public double IncomeTaxPercentage { get; set; }
        public double SocialContributionPercentage { get; set; }
        public decimal SocialContributionValue { get; set; }
        public double CharitySpentPercentage { get; set; }
    }
}
