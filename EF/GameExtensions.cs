using System.ComponentModel.DataAnnotations.Schema;

namespace mlbstats.EF;

public partial class Game
{
    [NotMapped]
    public GameStats GameStats { get; set; } 
}