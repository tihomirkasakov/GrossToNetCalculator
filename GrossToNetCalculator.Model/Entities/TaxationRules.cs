using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrossToNetCalculator.Model.Entities
{
    public class TaxationRules : BaseEntity
    {
        public decimal NoTaxationValue { get; set; }
        public double IncomeTaxPercentage { get; set; }
        public double SocialContributionPercentage { get; set; }
        public decimal SocialContributionValue { get; set; }
        public double CharitySpentPercentage { get; set; }
    }
}
