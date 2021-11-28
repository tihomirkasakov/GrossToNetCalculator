namespace GrossToNetCalculator.Model.Entities
{
    public class Currency : BaseEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool IsActive { get; set; }
    }
}
