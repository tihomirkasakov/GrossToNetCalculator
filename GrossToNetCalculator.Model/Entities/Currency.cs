using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrossToNetCalculator.Model.Entities
{
    public class Currency : BaseEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool IsActive { get; set; }
    }
}
