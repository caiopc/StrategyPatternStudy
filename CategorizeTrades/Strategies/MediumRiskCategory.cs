
using CategorizeTrades.Models;

namespace CategorizeTrades
{
    class MediumRiskCategory : IRiskCategory
    {
        public RiskLevel Level { get { return RiskLevel.Medium; } }

        public bool IsActive { get { return true; } }
        public string GetKey()
        {
            return "MediumRiskCategory";
        }

        public bool IsMatch(ITrade trade)
        {
            return (trade.Value > 1000000 && trade.ClientSector.Equals("Public"));
        }
    }
}
