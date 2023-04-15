namespace mlbstats.EF;

public partial class Play
{
    public long Id { get; set; }

    public long GameId { get; set; }

    public long InningHalf { get; set; }

    public long StartOuts { get; set; }

    public long StartOnBase { get; set; }

    public long PlayNum { get; set; }

    public string Desc { get; set; } = null!;

    public string? PitchCt { get; set; }

    public long BatterId { get; set; }

    public long PitcherId { get; set; }

    public virtual Player Batter { get; set; } = null!;

    public virtual Game Game { get; set; } = null!;

    public virtual Player Pitcher { get; set; } = null!;
}
