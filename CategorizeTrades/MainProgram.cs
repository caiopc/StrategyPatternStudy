using CategorizeTrades.Dbo;
using CategorizeTrades.Models;
using CategorizeTrades.Risk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CategorizeTrades
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            DbInit();

            var activeCategories = DbContext.GetCategories().Where(p => p.IsActive);

            var categoryStrategies = activeCategories
                .Select(p => RiskFactory.Create(p.Key));

            var trade1 = new Trade { Value = 2000000, ClientSector = "Private" };
            var trade2 = new Trade { Value = 400000, ClientSector = "Public" };
            var trade3 = new Trade { Value = 500000, ClientSector = "Public" };
            var trade4 = new Trade { Value = 3000000, ClientSector = "Public" };

            List<ITrade> portfolio = new List<ITrade> { trade1, trade2, trade3, trade4 };
            List<string> tradeCategories = new List<string>();

            ShowTrades(portfolio);

            foreach (var trade in portfolio)
            {
                var tradeFitsInRule = categoryStrategies.FirstOrDefault(p => p.IsMatch(trade));

                if (tradeFitsInRule != null)
                    tradeCategories.Add(tradeFitsInRule.Level.ToString());
            }

            ShowResults(tradeCategories);


            var chosenOption = GetUserCommand();
        }

        private static void ShowTrades(IEnumerable<ITrade> trades)
        {
            // Output console
            Console.Title = "Categories of Trades";
            Console.WriteLine("Input:\n");
            foreach (ITrade trade in trades)
            {
                Console.WriteLine("{0} = {{ Value = {1}, ClientSector = \"{2}\" }}", (Trade)trade, trade.Value, trade.ClientSector);
            }
        }

        private static void ShowResults(List<string> trades)
        {
            // Output console
            Console.Title = "Trades Categorization";
            Console.WriteLine("Input:\n");
            StringBuilder builder = new StringBuilder();
            builder.Append("\nresults = {{ ");

            foreach (string trade in trades)
            {
                builder.Append("\"" + trade + "Risk\"");
            }
            builder.Append("}} ");
            Console.WriteLine(builder.ToString());
        }

        //private static void ShowResults(IEnumerable<string> results)
        //{
        //    // Output console
        //    Console.Title = "Categories of Trades";
        //    Console.WriteLine("Input:\n");
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append("\nportfolio = {{ ");

        //    foreach (string trade in results)
        //    {
        //        builder.Append(trade);
        //    }
        //    builder.Append("}} ");
        //}

        private static string GetUserCommand()
        {
            return Console.ReadLine();
        }

        private static void DbInit()
        {
            DbContext.CreateSQLiteDataBase();
            DbContext.CreateSQliteTables();

            DbContext.Add(new Category() { Id = 1, Key = "LowRiskCategory", Level = RiskLevel.Low, IsActive = true });
            DbContext.Add(new Category() { Id = 2, Key = "MediumRiskCategory", Level = RiskLevel.Medium, IsActive = true });
            DbContext.Add(new Category() { Id = 3, Key = "HighRiskCategory", Level = RiskLevel.High, IsActive = true });
            DbContext.Add(new Category() { Id = 3, Key = "OrdinaryRiskCategory", Level = RiskLevel.High, IsActive = false });
        }
    }
}
