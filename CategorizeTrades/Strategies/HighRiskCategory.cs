
using CategorizeTrades.Models;

namespace CategorizeTrades
{
    public class HighRiskCategory : IRiskCategory
    {
        public RiskLevel Level { get { return RiskLevel.High; } }

        public bool IsActive { get; }

        public string GetKey()
        {
            return "HighRiskCategory";
        }

        public bool IsMatch(ITrade trade)
        {
            return (trade.Value > 1000000 && trade.ClientSector.Equals("Private"));
        }
    }
}
