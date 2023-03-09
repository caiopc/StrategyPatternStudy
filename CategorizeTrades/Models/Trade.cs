namespace CategorizeTrades.Models
{
    class Trade : ITrade
    {
        private static int tradeID = 1;

        private string name;

        public double Value { get; set; }
        public string ClientSector { get; set; }

        public Trade()
        {
            this.name = "Trade" + tradeID++;
        }
        public override string ToString()
        {
            return this.name;
        }
    }
}
