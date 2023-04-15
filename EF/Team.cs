namespace mlbstats.EF;

public partial class Team
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Abbreviation { get; set; } = null!;

    public virtual ICollection<Game> GameAwayTeams { get; } = new List<Game>();

    public virtual ICollection<Game> GameHomeTeams { get; } = new List<Game>();
}
