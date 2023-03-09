using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategorizeTrades.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public RiskLevel Level { get; set; }

        public bool IsActive { get; set; }
    }
}
