
using CategorizeTrades.Models;

namespace CategorizeTrades
{
    public class LowRiskCategory : IRiskCategory
    {
        public RiskLevel Level { get { return RiskLevel.Low; } }

        public bool IsActive { get { return true; } }

        public string GetKey()
        {
            return "LowRiskCategory";
        }

        public bool IsMatch(ITrade trade)
        {
            return (trade.Value < 1000000 && trade.ClientSector.Equals("Public"));
        }
    }
}
