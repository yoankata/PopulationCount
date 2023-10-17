namespace PopulationCount.Entities;

public partial class Country
{
    public string CountryName { get; set; } = null!;

    public long CountryId { get; set; }
    public virtual ICollection<State> States { get; set; } = new List<State>();

}
