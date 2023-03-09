
using CategorizeTrades.Models;

namespace CategorizeTrades
{
    interface IRiskCategory
    {
        RiskLevel Level { get; }
        bool IsMatch(ITrade trade);

        string GetKey();
    }
}
