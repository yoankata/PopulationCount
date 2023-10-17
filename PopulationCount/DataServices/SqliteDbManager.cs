using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Data.SQLite;

namespace Backend
{
    public class SqliteDbManager : IDbManager
    {
        public DbConnection getConnection()
        {
            try
            {
                var connection = new SqliteConnection("Data Source=citystatecountry.db;");
                connection.Open();

                return connection;
            }
            catch(SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
