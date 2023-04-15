namespace mlbstats;

public interface IStats
{
    int CalculateRating();
    string GetDisplayString(DisplayType displayType);
}

[Flags]
public enum DisplayType
{
    Rating = 0,
    Pitches = 1,
    HomeRuns = 2,
    Walks = 4,
    Steals = 8,
    StrikeOuts = 16,
    Singles = 32,
    Doubles = 64,
    Triples = 128,
    GroundOuts = 256,
    FlyBalls = 512,
    Scores = 1024,
    GrandSlams = 2048,
    WildPitches = 5096
}