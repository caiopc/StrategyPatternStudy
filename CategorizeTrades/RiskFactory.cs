using System;

namespace CategorizeTrades.Risk
{
    static class RiskFactory
    {
        public static IRiskCategory Create(string key)
        {
            switch (key)
            {
                case "LowRiskCategory":
                    return new LowRiskCategory();
                case "MediumRiskCategory":
                    return new MediumRiskCategory();
                case "HighRiskCategory":
                    return new HighRiskCategory();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
