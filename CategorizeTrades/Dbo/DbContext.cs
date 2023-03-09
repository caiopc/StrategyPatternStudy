using CategorizeTrades.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace CategorizeTrades.Dbo
{
    public class DbContext
    {
        private static SQLiteConnection sqliteConnection;
        public DbContext()
        { }
        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=c:\\dados\\TradesDatabase.sqlite; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }
        public static void CreateSQLiteDataBase()
        {
            try
            {
                SQLiteConnection.CreateFile(@"c:\dados\TradesDatabase.sqlite");
            }
            catch
            {
                throw;
            }
        }
        public static void CreateSQliteTables()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Categories(Id int, Key Varchar(50), Level VarChar(50), IsActive Bit)";
                    cmd.ExecuteNonQuery();
                    //cmd.CommandText = "CREATE TABLE IF NOT EXISTS Trades(id int, Nome Varchar(50), email VarChar(80))";
                    //cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IEnumerable<Category> GetCategories()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Categories";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);

                    return GetCollectioFromDataTable(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Category GetCategory(int id)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Categories Where Id=" + id;
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return GetCollectioFromDataTable(dt).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Add(Category category)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Categories(Id, Key, Level, IsActive ) values (@id, @key, @level, @isActive)";
                    cmd.Parameters.AddWithValue("@Id", category.Id);
                    cmd.Parameters.AddWithValue("@Key", category.Key);
                    cmd.Parameters.AddWithValue("@Level", category.Level);
                    cmd.Parameters.AddWithValue("@IsActive", category.IsActive);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Update(Category category)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (category.Id != null)
                    {
                        cmd.CommandText = "UPDATE Categories SET Key=@Key, Level=@Level, IsActive=@IsActive WHERE Id=@Id";
                        cmd.Parameters.AddWithValue("@Key", category.Key);
                        cmd.Parameters.AddWithValue("@Level", category.Level);
                        cmd.Parameters.AddWithValue("@IsActive", category.IsActive);
                        cmd.ExecuteNonQuery();
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Delete(int Id)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = "DELETE FROM Categories Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static IEnumerable<Category> GetCollectioFromDataTable(DataTable dt)
        {
            var convertedList = (from rw in dt.AsEnumerable()
                                 select new Category()
                                 {
                                     Id = Convert.ToInt32(rw["Id"]),
                                     Key = Convert.ToString(rw["Key"]),
                                     Level = (RiskLevel)Enum.Parse(typeof(RiskLevel), Convert.ToString(rw["Level"])),
                                     IsActive = Convert.ToBoolean(rw["IsActive"]),
                                 }).ToList();

            return convertedList;
        }
    }
}
