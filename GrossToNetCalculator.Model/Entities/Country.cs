namespace GrossToNetCalculator.Model.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public int TaxationRuleId { get; set; }
        public TaxationRule TaxationRule { get; set; }
    }
}
