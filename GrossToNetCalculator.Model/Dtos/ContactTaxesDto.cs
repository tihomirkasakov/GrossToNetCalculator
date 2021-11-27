using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrossToNetCalculator.Model.Dtos
{
    public class ContactTaxesDto
    {
        public decimal GrossIncome { get; set; }
        public decimal CharitySpent { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal SocialTax { get; set; }
        public decimal TotalTax { get; set; }
        public decimal NetIncome { get; set; }
    }
}
