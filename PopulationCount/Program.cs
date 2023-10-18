// See https://aka.ms/new-console-template for more information
using Backend;
using PopulationCount;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

Console.WriteLine("Started");

Console.WriteLine("Getting DB Connection...");
IDbManager db = new SqliteDbManager();
DbConnection conn = db.getConnection();

var watch = Stopwatch.StartNew();

// get db data using db connection and raw sql
if (conn != null)
{
    conn.Open();
    using (conn)
    {
        try
        {
            // Create query
            var queryString = $"select CountryName, Sum(c.Population) " +
                $"from City as c inner join State as s on s.StateId = c.StateId inner join Country co on co.CountryId = s.CountryId " +
                $"group by CountryName " +
                $"order by CountryName";

            // Create the command.
            DbCommand command = conn.CreateCommand();
            command.CommandText = queryString;
            command.CommandType = CommandType.Text;


            // Retrieve the data.
            DbDataReader reader = command.ExecuteReader();
            Console.WriteLine($"DB raw:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader[0], 35}: {reader[1], 20:N0}");
            }
            watch.Stop();
            Console.WriteLine($"Elapsed time for raw DB call (ms): {watch.ElapsedMilliseconds}");
        }

        catch (DbException dbex)
        {
            Console.WriteLine($"DbException.GetType: {dbex.GetType()}");
            Console.WriteLine($"DbException.Source: {dbex.Source}");
            Console.WriteLine($"DbException.ErrorCode: {dbex.ErrorCode}");
            Console.WriteLine($"DbException.Message: {dbex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}");
        }
    }
}
else
{
    Console.WriteLine("Failed to get connection");
}

// get data using EF context
watch = Stopwatch.StartNew();
var dbPopulationCount = new Dictionary<string, long>();
using (var context = new CitystatecountryContext())
{
    dbPopulationCount = context.Countries
        .Select(x => Tuple.Create(x.CountryName, x.States.Sum(y => y.Cities.Sum(z => z.Population))))
        .ToDictionary(z => z.Item1, z => z.Item2);
}


Console.WriteLine($"DB EF:");
foreach (var item in dbPopulationCount.OrderBy(o => o.Key))
{

    Console.WriteLine($"{item.Key,35}: {item.Value, 20:N0}");

}
watch.Stop();
Console.WriteLine($"Elapsed time for EF DB access (ms): {watch.ElapsedMilliseconds}");

// get concrete api service data
watch = Stopwatch.StartNew();
var stateService = new ConcreteStatService();
var apiPopulationCount = (await stateService.GetCountryPopulationsAsync())
     .ToDictionary(z => z.Item1, z => z.Item2);

Console.WriteLine($"API:");
foreach (var item in apiPopulationCount.OrderBy(o => o.Key))
{

    Console.WriteLine($"{item.Key, 35}: {item.Value, 20:N0}");

}
watch.Stop();
Console.WriteLine($"Elapsed time for API 'db' access time (ms): {watch.ElapsedMilliseconds}");


// join db and api data using linq
watch = Stopwatch.StartNew();

var joinedPopulationCount = (await stateService.GetCountryPopulationsAsync())
    .Where(x => !dbPopulationCount.ContainsKey(x.Item1))
    .ToDictionary(z => z.Item1, z => z.Item2).Union(dbPopulationCount);

Console.WriteLine($"DB and API Joined (DB precedence):");
foreach (var item in joinedPopulationCount.OrderBy(o => o.Key))
{

    Console.WriteLine($"{item.Key, 35}: {item.Value, 20:N0}");

}
watch.Stop();
Console.WriteLine($"Elapsed time for joining DB and API data (ms): {watch.ElapsedMilliseconds}");