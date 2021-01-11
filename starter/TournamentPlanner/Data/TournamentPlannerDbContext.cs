using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TournamentPlanner.Data
{
    public enum PlayerNumber { Player1 = 1, Player2 = 2 };

    public class TournamentPlannerDbContext : DbContext
    {
        public TournamentPlannerDbContext(DbContextOptions<TournamentPlannerDbContext> options)
            : base(options)
        { }

        // This class is NOT COMPLETE.
        // Todo: Complete the class according to the requirements

        public DbSet<Match> Matches { get; set; }

        public DbSet<Player> Players { get; set; }

        /// <summary>
        /// Adds a new player to the player table
        /// </summary>
        /// <param name="newPlayer">Player to add</param>
        /// <returns>Player after it has been added to the DB</returns>
        public async Task<Player> AddPlayer(Player newPlayer)
        {
            this.Players.Add(newPlayer);
            await this.SaveChangesAsync();
            return newPlayer;
        }

        /// <summary>
        /// Adds a match between two players
        /// </summary>
        /// <param name="player1Id">ID of player 1</param>
        /// <param name="player2Id">ID of player 2</param>
        /// <param name="round">Number of the round</param>
        /// <returns>Generated match after it has been added to the DB</returns>
        public async Task<Match> AddMatch(int player1Id, int player2Id, int round)
        {
            var newMatch = new Match { Player1ID = player1Id, Player2ID = player2Id, Round = round };
            this.Matches.Add(newMatch);
            await this.SaveChangesAsync();
            return newMatch;
        }

        /// <summary>
        /// Set winner of an existing game
        /// </summary>
        /// <param name="matchId">ID of the match to update</param>
        /// <param name="player">Player who has won the match</param>
        /// <returns>Match after it has been updated in the DB</returns>
        public async Task<Match> SetWinner(int matchId, PlayerNumber player)
        {
            var match = this.Matches.Single(m => m.ID == matchId);
            match.WinnerOfTheMatchID = player switch
            {
               PlayerNumber.Player1 => match.Player1ID,
               PlayerNumber.Player2 => match.Player2ID,
               _ => throw new ArgumentOutOfRangeException(nameof(player))
            };
            await this.SaveChangesAsync();
            return match;
        }

        /// <summary>
        /// Get a list of all matches that do not have a winner yet
        /// </summary>
        /// <returns>List of all found matches</returns>
        public async Task<IList<Match>> GetIncompleteMatches()
        {
            return await Matches.Where(m => m.WinnerOfTheMatch == null).ToListAsync();
        }

        /// <summary>
        /// Delete everything (matches, players)
        /// </summary>
        public async Task DeleteEverything()
        {
            foreach (var match in Matches)
            {
               this.Remove(match);
            }
            foreach (var player in Players)
            {
                this.Remove(player);
            }
            await this.SaveChangesAsync();
        }

        /// <summary>
        /// Get a list of all players whose name contains <paramref name="playerFilter"/>
        /// </summary>
        /// <param name="playerFilter">Player filter. If null, all players must be returned</param>
        /// <returns>List of all found players</returns>
        public async Task<IList<Player>> GetFilteredPlayers(string playerFilter = null)
        {
            return await this.Players.Where(p => playerFilter == null || p.Name.Contains(playerFilter)).ToListAsync();
        }

        /// <summary>
        /// Generate match records for the next round
        /// </summary>
        /// <exception cref="InvalidOperationException">Error while generating match records</exception>
        public async Task GenerateMatchesForNextRound()
        {
            if ((await GetIncompleteMatches()).Any()) throw new InvalidOperationException("Incomplete matches");

            if (this.Players.Count() != 32) throw new InvalidOperationException("Incorrect number of players");

            var numOfMatches = this.Matches.Count();

            switch (numOfMatches)
            {
                case 0:
                    await AddFirstRound(this.Matches, await GetFilteredPlayers());
                    break;
                case 16:
                case 24:
                case 28:
                case 30:
                    await AddRound(this.Matches);
                    break;
                default:
                    throw new InvalidOperationException("Incorrect number of matches");
            }
            await this.SaveChangesAsync();
        }

        private async Task AddRound(DbSet<Match> matches)
        {
            Random random = new();
            var lastRound = await matches.MaxAsync(m => m.Round);
            var lastRoundMatches = await matches.Where(m => m.Round == lastRound).ToListAsync();
            var nextRound = lastRound + 1;

            for (int i = lastRoundMatches.Count / 2; i > 0; i--)
            {
                var match1 = lastRoundMatches[random.Next(lastRoundMatches.Count)];
                lastRoundMatches.Remove(match1);
                var match2 = lastRoundMatches[random.Next(lastRoundMatches.Count)];
                lastRoundMatches.Remove(match2);
                matches.Add(new Match
                {
                    Player1 = match1.WinnerOfTheMatch,
                    Player2 = match2.WinnerOfTheMatch,
                    Round = nextRound
                });
            }
        }

        private Task AddFirstRound(DbSet<Match> matches, IList<Player> players)
        {
            Random random = new();

            for (int i = 0; i < 16; i++)
            {
                var player1 = players[random.Next(players.Count)];
                players.Remove(player1);
                var player2 = players[random.Next(players.Count)];
                players.Remove(player2);
                var newMatch = new Match { Player1 = player1, Player2 = player2, Round = 1 };
                matches.Add(newMatch);
            }
            return Task.CompletedTask;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                        .HasOne(d => d.Player1)
                        .WithMany()
                        .HasForeignKey(d => d.Player1ID)
                        .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Match>()
                       .HasOne(d => d.Player2)
                       .WithMany()
                       .HasForeignKey(d => d.Player2ID)
                       .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Match>()
                       .HasOne(d => d.WinnerOfTheMatch)
                       .WithMany()
                       .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
