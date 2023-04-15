namespace mlbstats.EF;

public partial class Venue
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Game> Games { get; } = new List<Game>();
}
