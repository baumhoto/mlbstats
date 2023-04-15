using System.Text;

namespace mlbstats;

public class InningStats : IStats
{
    public long GameId { get; set; }
    public Inning Inning { get; set; }
    public int Pitches { get; set; }
    public int HomeRuns { get; set; }
    public int Walks { get; set; }
    public int Steals { get; set; }
    public int StrikeOuts { get; set; }
    public int Singles { get; set; }
    public int Doubles { get; set; }
    public int Triples { get; set; }
    public int GroundOuts { get; set; }
    public int FlyBalls { get; set; }
    public int Scores { get; set; }
    public int GrandSlams { get; set; }
    public int WildPitches { get; set; }
    public int InningRating => CalculateRating();

    public int CalculateRating()
    {
        var result = this.Pitches + (HomeRuns * 5) + (Scores * 4) + (Triples * 3) + (Doubles * 2) + (Singles * 1) +
                     Walks + Steals + WildPitches + (GrandSlams * 5);
        return result;
    }

    public string GetDisplayString(DisplayType displayType)
    {
        var displayString = new StringBuilder();
        displayString.Append($"Inning {this.Inning.Number,2} Half: {this.Inning.Half,10} ");

        if (displayType == DisplayType.Rating)
        {
            displayString.Append($"Rating: {this.InningRating}");
        }
        
        if (displayType == DisplayType.Pitches)
        {
            displayString.Append($"Pitches: {this.Pitches,2} ");
        }
        
        if (displayType == DisplayType.HomeRuns)
        {
            displayString.Append($"HomeRuns: {this.HomeRuns,2} ");
        }

        if (displayType == DisplayType.Walks)
        {
            displayString.Append($"Walks/Hit: {this.Walks,2} ");
        }

        if (displayType == DisplayType.Steals)
        {
            displayString.Append($"Steals: {this.Steals,2} ");
        }

        if (displayType == DisplayType.StrikeOuts)
        {
            displayString.Append($"Strikeouts: {this.StrikeOuts,2} ");
        }

        if (displayType == DisplayType.Singles)
        {
            displayString.Append($"Singles: {this.Singles,2}  ");
        }

        if (displayType == DisplayType.Doubles)
        {
            displayString.Append($"Doubles: {this.Doubles,2} ");
        }

        if (displayType == DisplayType.Triples)
        {
            displayString.Append($"Triples: {this.Triples,2} ");
        }

        if (displayType == DisplayType.GroundOuts)
        {
            displayString.Append($"GroundOuts: {this.GroundOuts,2} ");
        }

        if (displayType == DisplayType.FlyBalls)
        {
            displayString.Append($"FlyBalls: {this.FlyBalls,2} ");
        }

        if (displayType == DisplayType.Scores)
        {
            displayString.Append($"Scores: {this.Scores,2} ");
        }

        displayString.Append($"{Environment.NewLine}");

        return displayString.ToString();
    }
}