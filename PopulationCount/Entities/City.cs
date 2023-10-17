namespace PopulationCount.Entities;

public partial class City
{
    public long CityId { get; set; }

    public string CityName { get; set; } = null!;

    public long Population { get; set; }

    public long StateId { get; set; }

    public virtual State State { get; set; } = null!;
}
