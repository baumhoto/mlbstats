using Microsoft.EntityFrameworkCore;
using mlbstats;
using mlbstats.EF;

internal class Program
{
    private static List<Game> _games;
    private static DateTime _gameDate;

    public static void Main(string[] args)
    {
        _gameDate = DateTime.Today.AddDays(-1);
        _games = CalculateGameStats(_gameDate);
        
        var gameId = 0;

        ListGames(gameId);

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
                BestInnings();
            }
            else if (userInput.ToLower().Contains("q"))
            {
                break;
            }
            else
            {
                ListGames(0);
            }

            userInput = null;

        } while (string.IsNullOrWhiteSpace(userInput));
    }

    private static List<Game> CalculateGameStats(DateTime gameDate)
    {
        var result = new List<Game>();
        
        var context = new StatsContext();
        foreach (var game in context.Games
                     .AsNoTracking()
                     .Include(g => g.Plays)
                     .Include(g => g.AwayTeam)
                     .Include(g => g.HomeTeam))
        {
            if (game.Date == gameDate)
            {
                game.GameStats = CalculateGameStats(game);
                result.Add(game);
            }
        } 
        
        return result;
    }

    private static void BestInnings()
    {

        Console.WriteLine($"Best innings for {_gameDate}");

        
        var allInnings = _games.SelectMany(x => x.GameStats.InningStats);

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

    private static void ListGames(int gameId)
    {
        Console.WriteLine($"Games for {_gameDate}");

        foreach (var game in _games.OrderByDescending(x => x.GameStats.Rating))
        {
            if (gameId == 0 || game.Id == gameId)
            {

                Console.WriteLine($"Id: {game.Id, 4} " +
                                  $"Game: {game.AwayTeam.Abbreviation} vs. {game.HomeTeam.Abbreviation} " +
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
                                  $"GameRating: {game.GameStats.Rating, 3} " +
                                  $"CloseGame: {game.GameStats.CloseGame, 5} " +
                                  $"AwayTeamRating {game.GameStats.AwayTeamRating} " +
                                  $"HomeTeamRating {game.GameStats.HomeTeamRating}");
            }
        }
    }

    public static void ListGameDetails(int gameid)
    {
        if (gameid == 0)
            return;
        
        Console.WriteLine($"Details for {gameid}");

        var game = _games.SingleOrDefault(g => g.Id == gameid);

        if (game == null)
        {
            Console.WriteLine($"No game data found for {gameid}");
        }


        Console.WriteLine($"Game: {game.AwayTeam.Abbreviation} vs. {game.HomeTeam.Abbreviation} " +
                          $"Close Game: {game.GameStats.CloseGame} " +
                        $"AwayTeamRating: { game.GameStats.AwayTeamRating } " +
                        $"HomeTeamRating: {game.GameStats.HomeTeamRating} ");
        
        // TODO Generate Pseudo InningStats to prevent spoiler
        // if (gameStats.InningStats.Count == 17)
        // {
        //     var pseudoInning = new InningStats() { Inning = new Inning(9, 
        //         InningHalf.Bottom)};
        //     pseudoInning.InningRating = gameStats.InningStats.Average(i => i.InningRating);
        // }

        // Show only regular innings
        foreach (var inningStats in game.GameStats.InningStats.Where(i => i.Inning.Number < 10))
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
        
        
        // generate a seudo-inning to prevent spoilers if bottom 9th is skipped
        if (game.GameStats.InningStats.Count == 17)
        {
            var inningMinHome = game.GameStats.InningStats.Where(i => i.Inning.Half == InningHalf.Bottom)
                .Min(i => i.InningRating);

            var inningMaxHome = game.GameStats.InningStats.Where(i => i.Inning.Half == InningHalf.Bottom)
                .Max(i => i.InningRating);
                
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
                            $"Rating: {random.Next(inningMinHome-2, inningMaxHome+2)}");
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