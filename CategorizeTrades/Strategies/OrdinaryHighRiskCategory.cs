
using CategorizeTrades.Models;

namespace CategorizeTrades
{
    public class OrdinaryHighRiskCategory : IRiskCategory
    {
        public RiskLevel Level { get { return RiskLevel.None; } }

        public bool IsActive { get { return false; } }

        public string GetKey()
        {
            return "OrdinaryHighRiskCategory";
        }

        public bool IsMatch(ITrade trade)
        {
            return (trade.Value > 1000000 && trade.ClientSector.Equals("Private"));
        }
    }
}
