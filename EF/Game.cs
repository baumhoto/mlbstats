namespace mlbstats.EF;

public partial class Game
{
    public long Id { get; set; }

    public string NameId { get; set; } = null!;

    public string? LocalStartTime { get; set; }

    public long? TimeOfDay { get; set; }

    public long? FieldType { get; set; }

    public DateTime? Date { get; set; } = null!;

    public long? VenueId { get; set; }

    public long HomeTeamId { get; set; }

    public long AwayTeamId { get; set; }

    public virtual Team AwayTeam { get; set; } = null!;

    public virtual Team HomeTeam { get; set; } = null!;

    public virtual ICollection<Play> Plays { get; } = new List<Play>();

    public virtual Venue? Venue { get; set; }
}
