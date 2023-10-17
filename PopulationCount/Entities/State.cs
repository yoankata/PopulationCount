namespace PopulationCount.Entities;

public partial class State
{
    public long StateId { get; set; }

    public string StateName { get; set; } = null!;

    public long CountryId { get; set; }
    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
