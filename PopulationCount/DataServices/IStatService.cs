namespace Backend
{
    interface IStatService
    {
        List<Tuple<string, long>> GetCountryPopulations();
        Task<List<Tuple<string, long>>> GetCountryPopulationsAsync();
    }
}
