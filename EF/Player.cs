namespace mlbstats.EF;

public partial class Player
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string NameId { get; set; } = null!;

    public long Bats { get; set; }

    public long Throws { get; set; }

    public virtual ICollection<Play> PlayBatters { get; } = new List<Play>();

    public virtual ICollection<Play> PlayPitchers { get; } = new List<Play>();
}
