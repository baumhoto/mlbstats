namespace mlbstats;

public class GameStats
{
    public List<InningStats> InningStats { get; set; }

    public GameStats()
    {
        InningStats = new List<InningStats>();
    }


    public int Pitches
    {
        get { return InningStats.Sum(i => i.Pitches); }
    }

    public int HomeRuns
    {
        get { return InningStats.Sum(i => i.HomeRuns); }
    }

    public int Walks
    {
        get { return InningStats.Sum(i => i.Walks); }
    }

    public int Steals
    {
        get { return InningStats.Sum(i => i.Steals); }
    }

    public int Singles
    {
        get { return InningStats.Sum(i => i.Singles); }
    }

    public int Doubles
    {
        get { return InningStats.Sum(i => i.Doubles); }
    }
    
    public int Triples
    {
        get { return InningStats.Sum(i => i.Triples); }
    }


    public int GroundOuts
    {
        get { return InningStats.Sum(i => i.GroundOuts); }
    }

    public int FlyBalls
    {
        get { return InningStats.Sum(i => i.FlyBalls); }
    }

    public int StrikeOuts
    {
        get { return InningStats.Sum(i => i.StrikeOuts); }
    }

    public int Scores
    {
        get { return InningStats.Sum(i => i.Scores); }
    }

    public int AwayTeamsScore
    {
        get { return InningStats.Where(i => i.Inning.Half == InningHalf.Top).Sum(i => i.Scores); }
    }
    
    public int HomeTeamScore
    {
        get { return InningStats.Where(i => i.Inning.Half == InningHalf.Bottom).Sum(i => i.Scores); }
    }

    public int Rating => CalculateGameRating();

    private int CalculateGameRating()
    {

        var inningRatingsSum = InningStats.Sum(x => x.InningRating);
        // Extra Innings
        if (InningStats.Count > 18)
        {
            inningRatingsSum += 50;
        }

        inningRatingsSum += 50 - Int32.Abs(AwayTeamsScore - HomeTeamScore) * 10;

        return inningRatingsSum;
    }
}