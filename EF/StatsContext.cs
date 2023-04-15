using Microsoft.EntityFrameworkCore;

namespace mlbstats.EF;

public partial class StatsContext : DbContext
{
    public StatsContext()
    {
    }

    public StatsContext(DbContextOptions<StatsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Play> Plays { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=stats.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.ToTable("game");

            entity.HasIndex(e => e.AwayTeamId, "game_away_team_id");

            entity.HasIndex(e => e.HomeTeamId, "game_home_team_id");

            entity.HasIndex(e => e.NameId, "game_name_id").IsUnique();

            entity.HasIndex(e => e.VenueId, "game_venue_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AwayTeamId).HasColumnName("away_team_id");
            entity.Property(e => e.Date)
                .HasColumnType("DATE")
                .HasColumnName("date");
            entity.Property(e => e.FieldType).HasColumnName("field_type");
            entity.Property(e => e.HomeTeamId).HasColumnName("home_team_id");
            entity.Property(e => e.LocalStartTime)
                .HasColumnType("TIME")
                .HasColumnName("local_start_time");
            entity.Property(e => e.NameId)
                .HasColumnType("CHAR(12)")
                .HasColumnName("name_id");
            entity.Property(e => e.TimeOfDay).HasColumnName("time_of_day");
            entity.Property(e => e.VenueId).HasColumnName("venue_id");

            entity.HasOne(d => d.AwayTeam).WithMany(p => p.GameAwayTeams)
                .HasForeignKey(d => d.AwayTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.GameHomeTeams)
                .HasForeignKey(d => d.HomeTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Venue).WithMany(p => p.Games).HasForeignKey(d => d.VenueId);
        });

        modelBuilder.Entity<Play>(entity =>
        {
            entity.ToTable("play");

            entity.HasIndex(e => e.BatterId, "play_batter_id");

            entity.HasIndex(e => e.GameId, "play_game_id");

            entity.HasIndex(e => e.PitcherId, "play_pitcher_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BatterId).HasColumnName("batter_id");
            entity.Property(e => e.Desc)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("desc");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.InningHalf).HasColumnName("inning_half");
            entity.Property(e => e.PitchCt)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("pitch_ct");
            entity.Property(e => e.PitcherId).HasColumnName("pitcher_id");
            entity.Property(e => e.PlayNum).HasColumnName("play_num");
            entity.Property(e => e.StartOnBase).HasColumnName("start_on_base");
            entity.Property(e => e.StartOuts).HasColumnName("start_outs");

            entity.HasOne(d => d.Batter).WithMany(p => p.PlayBatters)
                .HasForeignKey(d => d.BatterId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Game).WithMany(p => p.Plays)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Pitcher).WithMany(p => p.PlayPitchers)
                .HasForeignKey(d => d.PitcherId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.ToTable("player");

            entity.HasIndex(e => e.NameId, "player_name_id").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Bats).HasColumnName("bats");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("name");
            entity.Property(e => e.NameId)
                .HasColumnType("VARCHAR(9)")
                .HasColumnName("name_id");
            entity.Property(e => e.Throws).HasColumnName("throws");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.ToTable("team");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Abbreviation)
                .HasColumnType("CHAR(3)")
                .HasColumnName("abbreviation");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.ToTable("venue");

            entity.HasIndex(e => e.Name, "venue_name").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
