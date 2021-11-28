using System.ComponentModel.DataAnnotations;

namespace GrossToNetCalculator.Model.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
