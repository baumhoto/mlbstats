using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using mlbstats;
using mlbstats.EF;

internal class Program
{
    public static void Main(string[] args)
    {
        var gameDate = DateTime.Today.AddDays(-1);
        var gameId = 0;

        ListGames(gameDate, gameId);


        var userInput = string.Empty;

        do
        {
            userInput = Console.ReadLine();
            if (int.TryParse(userInput, out gameId))
            {
                ListGameDetails(gameId);
            }
            else if (userInput.ToLower().StartsWith("i"))
            {
                BestInnings(gameDate);
            }
            else if (userInput.ToLower().Contains("q"))
            {
                break;
            }
            else
            {
                ListGames(gameDate, 0);
            }

            userInput = null;

        } while (string.IsNullOrWhiteSpace(userInput));

        
        
    }

    private static void BestInnings(DateTime gameDate)
    {
        var context = new StatsContext();

        Console.WriteLine($"Best innings for {gameDate}");

        var gameStats = new List<GameStats>();
        

        foreach (var game in context.Games
                     .AsNoTracking()
                     .Include(g => g.Plays)
                     .Include(g => g.AwayTeam)
                     .Include(g => g.HomeTeam))
        {
            if (game.Date == gameDate)
            {
                gameStats.Add(CalculateGameStats(game));
            }
        }

        var allInnings = gameStats.SelectMany(x => x.InningStats);

        foreach (var inningStats in allInnings.OrderByDescending(x => x.InningRating).Take(10))
        {
            Console.WriteLine(
                $"GameId: {inningStats.GameId} Inning {inningStats.Inning.Number,2} Half: {inningStats.Inning.Half,10} " +
                // $"Pitches: {inningStats.Pitches,2} " +
                // $"HomeRuns: {inningStats.HomeRuns,2} " +
                // $"Walks/Hit: {inningStats.Walks,2} " +
                // $"Steals: {inningStats.Steals,2} " +
                // $"Strikeouts: {inningStats.StrikeOuts,2} " +
                // $"Singles: {inningStats.Singles,2}  " +
                // $"Doubles: {inningStats.Doubles,2} " +
                // $"Triples: {inningStats.Triples,2} " +
                // $"GroundOuts: {inningStats.GroundOuts,2} " +
                // $"FlyBalls: {inningStats.FlyBalls,2} " +
                // $"Scores: {inningStats.Scores,2} " +
                $"Rating: {inningStats.InningRating}");
        }
    }

    private static void ListGames(DateTime gameDate, int gameId)
    {
        var context = new StatsContext();

        Console.WriteLine($"Games for {gameDate}");

        foreach (var game in context.Games
                     .AsNoTracking()
                     .Include(g => g.Plays)
                     .Include(g => g.AwayTeam)
                     .Include(g => g.HomeTeam))
        {
            if ((game.Date == gameDate && gameId == 0) || game.Id == gameId)
            {
                var gameStats = CalculateGameStats(game);

                Console.WriteLine($"Game: {game.AwayTeam.Abbreviation} vs. {game.HomeTeam.Abbreviation} " +
                                  // $"Innings: {gameStats.InningStats.Count} " +
                                  // $"Plays: {game.Plays.Count, 2} " +
                                  // $"Pitches: {gameStats.Pitches, 2} " +
                                  // $"HomeRuns: {gameStats.HomeRuns, 2} " +
                                  // $"Walks/Hit: {gameStats.Walks, 2} " +
                                  // $"Steals: {gameStats.Steals, 2} " +
                                  // $"Strikeouts: {gameStats.StrikeOuts, 2} " +
                                  // $"Singles: {gameStats.Singles, 2}  " +
                                  // $"Doubles: {gameStats.Doubles, 2} " +
                                  // $"Triples: {gameStats.Triples, 2} " +
                                  // $"GroundOuts: {gameStats.GroundOuts, 2} " +
                                  // $"FlyBalls: {gameStats.FlyBalls, 2} " +
                                  // $"Scores: {gameStats.Scores, 2} " +
                                  // $"AwayTeamScore: {gameStats.AwayTeamsScore, 2} " +
                                  // $"HomeTeamScore: {gameStats.HomeTeamScore, 2} " +
                                  $"GameRating: {gameStats.Rating, 3} " +
                                  $"Id: {game.Id}");
            }
        }
    }

    public static void ListGameDetails(int gameid)
    {
        if (gameid == 0)
            return;
        
        var context = new StatsContext();

        Console.WriteLine($"Details for {gameid}");
        var game = context.Games
            .AsNoTracking()
            .Include(g => g.Plays)
            .Include(g => g.AwayTeam)
            .Include(g => g.HomeTeam)
            .SingleOrDefault(g => g.Id == gameid);


        if (game == null)
        {
            Console.WriteLine($"No game data found for {gameid}");
        }

        var gameStats = CalculateGameStats(game);

        Console.WriteLine($"Game: {game.AwayTeam.Abbreviation} vs. {game.HomeTeam.Abbreviation}");
        
        // TODO Generate Pseudo InningStats to prevent spoiler
        // if (gameStats.InningStats.Count == 17)
        // {
        //     var pseudoInning = new InningStats() { Inning = new Inning(9, 
        //         InningHalf.Bottom)};
        //     pseudoInning.InningRating = gameStats.InningStats.Average(i => i.InningRating);
        // }

        // Show only regular innings
        foreach (var inningStats in gameStats.InningStats.Where(i => i.Inning.Number < 10))
        {
            Console.WriteLine(
                $"Inning {inningStats.Inning.Number,2} Half: {inningStats.Inning.Half,10} " +
                // $"Pitches: {inningStats.Pitches,2} " +
                // $"HomeRuns: {inningStats.HomeRuns,2} " +
                // $"Walks/Hit: {inningStats.Walks,2} " +
                // $"Steals: {inningStats.Steals,2} " +
                // $"Strikeouts: {inningStats.StrikeOuts,2} " +
                // $"Singles: {inningStats.Singles,2}  " +
                // $"Doubles: {inningStats.Doubles,2} " +
                // $"Triples: {inningStats.Triples,2} " +
                // $"GroundOuts: {inningStats.GroundOuts,2} " +
                // $"FlyBalls: {inningStats.FlyBalls,2} " +
                // $"Scores: {inningStats.Scores,2} " +
                $"Rating: {inningStats.InningRating}");
        }

        if (gameStats.InningStats.Count == 17)
        {
            var random = new Random();
             Console.WriteLine(
                            $"Inning {9,2} Half: {InningHalf.Bottom,10} " +
                            // $"Pitches: {inningStats.Pitches,2} " +
                            // $"HomeRuns: {inningStats.HomeRuns,2} " +
                            // $"Walks/Hit: {inningStats.Walks,2} " +
                            // $"Steals: {inningStats.Steals,2} " +
                            // $"Strikeouts: {inningStats.StrikeOuts,2} " +
                            // $"Singles: {inningStats.Singles,2}  " +
                            // $"Doubles: {inningStats.Doubles,2} " +
                            // $"Triples: {inningStats.Triples,2} " +
                            // $"GroundOuts: {inningStats.GroundOuts,2} " +
                            // $"FlyBalls: {inningStats.FlyBalls,2} " +
                            // $"Scores: {inningStats.Scores,2} " +
                            $"Rating: {random.Next(12, 24)}");
        }

    }


    public static GameStats CalculateGameStats(Game game)
    {
        var gameStats = new GameStats();

        foreach (var play in game.Plays)
        {
            var inning = GetInning(play.InningHalf);

            var inningStats = gameStats.InningStats.SingleOrDefault(x => x.Inning.Equals(inning));

            if (inningStats == null)
            {
                inningStats = new InningStats() { Inning = inning };
                inningStats.GameId = game.Id;
                gameStats.InningStats.Add(inningStats);
            }

            // Pitches
            var pitchesPlay = play.PitchCt.Split(',')[0];
            int pitchCount = 0;
            int.TryParse(pitchesPlay, out pitchCount);
            inningStats.Pitches += pitchCount;

            var descriptionDetails = play.Desc.Split(";");

            var homeRunInDescription = false;
            var numberOfScoresInDescription = 0;

            foreach (var descriptionDetail in descriptionDetails)
            {
                var playDetail = descriptionDetail.ToLowerInvariant().Trim();
                
                // HomeRuns
                if (playDetail.StartsWith("home run"))
                {
                    homeRunInDescription = true;
                    inningStats.HomeRuns++;
                    inningStats.Scores++;
                }
                

                // Walks or Hit by Pitch
                inningStats.Walks += playDetail.StartsWith("walk") ? 1 : 0;
                inningStats.Walks += playDetail.StartsWith("hit by pitch") ? 1 : 0;

                // Steals
                inningStats.Steals += playDetail.Contains(" steals ") ? 1 : 0;

                // Strikeout
                inningStats.StrikeOuts += playDetail.StartsWith("strikeout") ? 1 : 0;

                // Singles
                inningStats.Singles += playDetail.StartsWith("single") ? 1 : 0;

                // Doubles
                inningStats.Doubles += playDetail.StartsWith("double") ? 1 : 0;

                // Triples
                inningStats.Triples += playDetail.StartsWith("triple") ? 1 : 0;

                // Groundout
                inningStats.GroundOuts += playDetail.StartsWith("groundout") ? 1 : 0;

                // Flyballs
                inningStats.FlyBalls += playDetail.StartsWith("flyball") ? 1 : 0;
                
                // Wild Pitch
                inningStats.WildPitches += playDetail.StartsWith("wild pitch") ? 1 : 0;

                // Scores
                if (playDetail.Contains(" scores"))
                {
                    numberOfScoresInDescription++;
                    inningStats.Scores++;
                }
            }
            
            // Grand Slam
            if (homeRunInDescription && numberOfScoresInDescription == 3)
            {
                inningStats.GrandSlams++;
            }
        }

        return gameStats;
    }

    private static Inning GetInning(long number)
    {
        var inningNumber = number + 1;
        var inningHalf = InningHalf.Top;
        if (inningNumber % 2 == 0)
        {
            inningHalf = InningHalf.Bottom;
        }

        var inning = (int)Math.Ceiling(inningNumber / 2d);

        return new Inning(inning, inningHalf);
    }
}