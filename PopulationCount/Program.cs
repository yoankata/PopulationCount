// See https://aka.ms/new-console-template for more information
using Backend;
using PopulationCount;
using System.Data.Common;

Console.WriteLine("Hello, World!");
Console.WriteLine("Started");
Console.WriteLine("Getting DB Connection...");
var query = $"select CountryName, Sum(c.Population)" +
    $"from City as c inner join State as s on s.StateId = c.StateId inner join Country co on co.CountryId = s.CountryId" +
    $"group by CountryName";
IDbManager db = new SqliteDbManager();
DbConnection conn = db.getConnection();
conn.Open();
conn.CreateCommand().CommandText=query;
conn.BeginTransaction();
conn.sql
if (conn == null)
{
    Console.WriteLine("Failed to get connection");
}


var stateService = new ConcreteStatService();
using var context = new CitystatecountryContext();
var dbPopulationCount = context.Countries.Select(x => Tuple.Create(x.CountryName, x.States.Sum(y => y.Cities.Sum(z=> z.Population))))
    .ToDictionary(z => z.Item1, z=>z.Item2 );

var apiPopulationCount = (await stateService.GetCountryPopulationsAsync()).Where(x => !dbPopulationCount.ContainsKey(x.Item1))
     .ToDictionary(z => z.Item1, z => z.Item2).Union(dbPopulationCount);


foreach (var item in dbPopulationCount)
{
    Console.WriteLine($"{item.Key}:\t {item.Value}");

}