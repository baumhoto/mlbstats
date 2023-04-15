namespace mlbstats;

public class Inning
{
    public int Number { get; set; }

    public InningHalf Half { get; set; }

    public Inning(int number, InningHalf half)
    {
        Number = number;
        Half = half;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Inning)
        {
            var other = obj as Inning;
            return other.Number == this.Number && other.Half == this.Half;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}