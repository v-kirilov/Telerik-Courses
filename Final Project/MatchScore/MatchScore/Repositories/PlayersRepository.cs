using MatchScore.Data;
using MatchScore.Exceptions;
using MatchScore.Models;
using MatchScore.Models.QueryParameters;
using MatchScore.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchScore.Repositories
{
    public class PlayersRepository : IPlayersRepository
    {
        private const string NotFoundPlayerIdErrorMessage = "Player with id:{0} does not exist";
        private const string NotFoundPlayerFullNameErrorMessage = "Player with full name:{0} does not exist";

        private readonly ApplicationContext context;
        private readonly IRolesRepository rolesRepository;
        private readonly IUsersRepository usersRepository;
        private readonly ICountriesRepository countriesRepository;
        private readonly ISportClubsRepository sportClubsRepository;

        public PlayersRepository(ApplicationContext context, IRolesRepository rolesRepository,IUsersRepository usersRepository,
                                 ICountriesRepository countriesRepository, ISportClubsRepository sportClubsRepository)
        {
            this.rolesRepository = rolesRepository;
            this.usersRepository = usersRepository;
            this.context = context;
            this.countriesRepository = countriesRepository;
            this.sportClubsRepository = sportClubsRepository;
        }

        public List<Player> GetAll()
        {
            return this.GetPlayers().ToList();
        }

        public Player GetById(int id)
        {
            Player player = this.GetPlayers().Where(p => p.Id == id).FirstOrDefault();
            return player ?? throw new EntityNotFoundException(string.Format(NotFoundPlayerIdErrorMessage, id));
        }

        public Player GetByFullName(string fullName)
        {
            Player player = this.GetPlayers().Where(p => p.FullName == fullName).FirstOrDefault();

            return player ?? throw new EntityNotFoundException(string.Format(NotFoundPlayerFullNameErrorMessage, fullName));
        }

        public PaginatedList<Player> FilterBy(PlayerQueryParameters filterParameters)
        {
            IQueryable<Player> result = this.GetPlayers();
            result = FilterByFullName(result, filterParameters.FullName);
            result = FilterByCountry(result, filterParameters.Country);
            result = FilterBySportClub(result, filterParameters.SportClub);
            result = SortBy(result, filterParameters.SortBy);
            result = Order(result, filterParameters.SortOrder);

            double totalPages = Math.Ceiling((1.0 * result.Count()) / filterParameters.PageSize);
            result = Paginate(result, filterParameters.PageNumber, filterParameters.PageSize);

            return new PaginatedList<Player>(result.ToList(), (int)totalPages, filterParameters.PageNumber);
        }

        public Player Create(Player player)
        {
            this.context.Players.Add(player);
            this.context.SaveChanges();

            return player;
        }

        public Player UpdateCountry(int id, string countryName)
        {
            var country = this.countriesRepository.GetByName(countryName);

            var playerToUpdate = this.GetById(id);
            playerToUpdate.CountryId = country.Id;
            playerToUpdate.Country = country;

            this.context.Update(playerToUpdate);
            this.context.SaveChanges();

            return playerToUpdate;
        }

        public Player UpdateSportClub(int id, string sportClubName)
        {
            var sportClub = this.sportClubsRepository.GetByName(sportClubName);

            var playerToUpdate = this.GetById(id);
            playerToUpdate.SportClubId = sportClub.Id;
            playerToUpdate.SportClub = sportClub;

            this.context.Update(playerToUpdate);
            this.context.SaveChanges();

            return playerToUpdate;
        }

        public Player UpdateUser(int id, string userEmail)
        {
            var user = this.usersRepository.GetByEmail(userEmail);

            var playerToUpdate = this.GetById(id);
            playerToUpdate.UserId= user.Id;
            playerToUpdate.User = user;

            this.context.Update(playerToUpdate);
            this.context.SaveChanges();

            return playerToUpdate;
        }
        public Player UpdateFullName(int id, string fullName)
        {
            var playerToUpdate = this.GetById(id);
            playerToUpdate.FullName = fullName;

            this.context.Update(playerToUpdate);
            this.context.SaveChanges();

            return playerToUpdate;
        }


        public string GetWorstWinLossRatio(int id)
        {
            var player = this.GetById(id);
            var nameWorstOpponent = this.GetNameWorstOpponent(player.Id);

            if (nameWorstOpponent == "none")
            {
                return "none";
            }

            var worstOpponent = this.GetByFullName(nameWorstOpponent);

            double wins, losses, draws;
            GetWinLossAndDrawByOpponent(player, worstOpponent, out wins, out losses, out draws);

            double ratio = 0.0;

            if (losses == 0)
            {
                ratio = 1.0;
            }
            else if (wins == 0)
            {
                ratio = 0.0;
            }
            else
            {
                ratio = wins / losses;
            }

            string winLossString = $"{wins}(W)/{losses}(L)";

            return winLossString;
        }

        public string GetBestWinLossRatio(int id)
        {
            var player = this.GetById(id);
            var nameBestOpponent = this.GetNameBestOpponent(player.Id);

            if (nameBestOpponent == "none")
            {
                return "none";
            }

            var bestOpponent = this.GetByFullName(nameBestOpponent);

            double wins, losses, draws;
            GetWinLossAndDrawByOpponent(player, bestOpponent, out wins, out losses, out draws);

            double ratio = 0.0;

            if (losses == 0)
            {
                ratio = 1.0;
            }
            else if (wins == 0)
            {
                ratio = 0.0;
            }
            else
            {
                ratio = wins / losses;
            }

            string winLossString = $"{wins}(W)/{losses}(L)";

            return winLossString;
        }

        public string GetNameBestOpponent(int id)
        {
            var player = this.GetById(id);
            var matchesByPlayer = this.GetMatchesByPlayer(player.Id);

            var scores = matchesByPlayer.Select(m => m.Scores.Where(s => s.PlayerId != id).FirstOrDefault()).ToList();
            var scoresGroupedByPlayers = scores.GroupBy(s => s.PlayerId)
                                         .Select(scoresGroup => new
                                         {
                                             PlayerId = scoresGroup.Key,
                                             Score = scoresGroup.Sum(s => s.Value)
                                         }).ToList();
            var nameBestOpponent = string.Empty;

            var opponentWinLossDiff = new Dictionary<Player, double>();


            if (scoresGroupedByPlayers.Count > 0)
            {
                foreach (var score in scoresGroupedByPlayers)
                {
                    var opponent = this.GetById(score.PlayerId);
                    double wins, losses, draws;
                    double diff;
                    GetWinLossAndDrawByOpponent(player, opponent, out wins, out losses, out draws);
                    diff = wins - losses;

                    opponentWinLossDiff.Add(opponent, diff);
                }

                nameBestOpponent = opponentWinLossDiff.OrderByDescending(o => o.Value).FirstOrDefault().Key.FullName;

            }
            else
            {
                nameBestOpponent = "none";
            }
            //if (scoresGroupedByPlayers.Count > 0)
            //{
            //    var minScore = scoresGroupedByPlayers.Min(sc => sc.Score);
            //    var bestOpponentId = scoresGroupedByPlayers.Where(sc => sc.Score == minScore).Select(sc => sc.PlayerId).FirstOrDefault();
            //    var bestOpponent = this.GetById(bestOpponentId);
            //    nameBestOpponent = bestOpponent.FullName;
            //}
            //else
            //{
            //    nameBestOpponent = "none";
            //}


            return nameBestOpponent;
        }

        public string GetNameWorstOpponent(int id)
        {
            var player = this.GetById(id);
            var matchesByPlayer = this.GetMatchesByPlayer(player.Id);

            var scores = matchesByPlayer.Select(m => m.Scores.Where(s => s.PlayerId != id).FirstOrDefault()).ToList();
            var scoresGroupedByPlayers = scores.GroupBy(s => s.PlayerId)
                                         .Select(scoresGroup => new
                                         {
                                             PlayerId = scoresGroup.Key,
                                             Score = scoresGroup.Sum(s => s.Value)
                                         }).ToList();
            var nameWorstOpponent = string.Empty;

            var opponentWinLossDiff = new Dictionary<Player, double>();


            if (scoresGroupedByPlayers.Count > 0)
            {
                foreach (var score in scoresGroupedByPlayers)
                {
                    var opponent = this.GetById(score.PlayerId);
                    double wins, losses, draws;
                    double diff;
                    GetWinLossAndDrawByOpponent(player, opponent, out wins, out losses, out draws);
                    diff = wins - losses;

                    opponentWinLossDiff.Add(opponent, diff);
                }

                nameWorstOpponent = opponentWinLossDiff.OrderBy(o => o.Value).FirstOrDefault().Key.FullName;

            }
            else
            {
                nameWorstOpponent = "none";
            }

            //if (scoresGroupedByPlayers.Count > 0)
            //{
            //    var maxScore = scoresGroupedByPlayers.Max(sc => sc.Score);
            //    var worstOpponentId = scoresGroupedByPlayers.Where(sc => sc.Score == maxScore).Select(sc => sc.PlayerId).FirstOrDefault();
            //    var worstOpponent = this.GetById(worstOpponentId);
            //    nameWorstOpponent = worstOpponent.FullName;
            //}
            //else
            //{
            //    nameWorstOpponent = "none";
            //}


            return nameWorstOpponent;
        }

        public string GetNameMostPlayedOpponent(int id)
        {
            var player = this.GetById(id);
            var matches = this.GetMatchesByPlayer(player.Id);
            var scoresTable = new List<Scores>();

            foreach (var match in matches)
            {
                scoresTable.Add(match.Scores.Where(s => s.PlayerId != player.Id).FirstOrDefault());
            }

            string name = string.Empty;

            if (scoresTable.Count > 0)
            {
                var groupReccuringPlayerId = scoresTable.GroupBy(s => s.PlayerId)
                                                        .Select(scoresGroup => new
                                                        {
                                                            PlayerId = scoresGroup.Key,
                                                            Count = scoresGroup.Sum(s => s.PlayerId)
                                                        }).ToList();


                var maxCount = groupReccuringPlayerId.Max(sc => sc.Count);
                var mostOpponentId = groupReccuringPlayerId.Where(sc => sc.Count == maxCount).Select(sc => sc.PlayerId).FirstOrDefault();
                var mostOpponent = this.GetById(mostOpponentId);

                name = mostOpponent.FullName;
            }
            else
            {
                name = "none";
            }
            

            return name;
        }

        public int CountMatchesPlayed(int id)
        {
            var player = this.GetById(id);
            var matchesPlayed = this.GetMatchesByPlayer(id).Count;

            return matchesPlayed;
        }

        public int CountTournamentsPlayed(int id)
        {
            var player = this.GetById(id);
            var tournamentsPlayed = player.Tournaments.Where(t => t.Status == Models.Enums.Status.Past).Count();

            return tournamentsPlayed;
        }

        public int CountTournamentsWon(int id)
        {
            var player = this.GetById(id);
            var tournamentsWon = player.Tournaments.Where(t=>t.Winner == player.FullName && t.Status == Models.Enums.Status.Past).Count();

            return tournamentsWon;
        }

        public int CountMatchesWon(int id)
        {
            var player = this.GetById(id);
            var matchesByPlayer = this.GetMatchesByPlayer(id);
            int count = 0;

            foreach (var match in matchesByPlayer)
            {
                int score = this.GetScoreInMatchByPlayer(match.Id, player.Id);

                if (IsWin(score, match))
                {
                    count++;
                }
            }

            return count;
        }

        public int CountMatchesDraw(int id)
        {
            var player = this.GetById(id);
            var matchesByPlayer = this.GetMatchesByPlayer(id);
            int count = 0;

            foreach (var match in matchesByPlayer)
            {
                int score = this.GetScoreInMatchByPlayer(match.Id, player.Id);

                if (IsDraw(score, match))
                {
                    count++;
                }
            }

            return count;
        }

        public int CountMatchesLoss(int id)
        {
            var player = this.GetById(id);
            var matchesByPlayer = this.GetMatchesByPlayer(id);
            int count = 0;

            foreach (var match in matchesByPlayer)
            {
                int score = this.GetScoreInMatchByPlayer(match.Id, player.Id);

                if (IsLoss(score, match))
                {
                    count++;
                }
            }

            return count;
        }

        public void Delete(int id)
        {
            var existingPlayer = this.GetById(id);
            existingPlayer.IsDeleted = true;

            this.context.Update(existingPlayer);
            this.context.SaveChanges();
        }

        private bool IsWin(int score, Match match)
        {
            if (score == match.Scores.Max(s => s.Value) && match.Scores.Where(s => s.Value == score).Count() == 1)
            {
                return true;
            }

            return false;
        }

        private bool IsLoss(int score, Match match)
        {
            if (score == match.Scores.Min(s => s.Value) && match.Scores.Where(s => s.Value == score).Count() == 1)
            {
                return true;
            }

            return false;
        }

        private bool IsDraw(int score, Match match)
        {
            if (match.Scores.Where(s => s.Value == score).Count() != 1)
            {
                return true;
            }

            return false;
        }

        private List<Match> GetMatchesByPlayer(int id)
        {
            var player = this.GetAll().Where(p => p.Id == id).FirstOrDefault();
            var matches = player.Scores.Select(s => s.Match).Where(p => p.Status == Models.Enums.Status.Past).ToList();

            return matches;
        }

        private int GetScoreInMatchByPlayer(int matchId, int playerId)
        {
            int score = this.context.Scores.Where(s => s.MatchId == matchId && s.PlayerId == playerId).FirstOrDefault().Value;

            return score;
        }

        private void GetWinLossAndDrawByOpponent(Player player, Player opponent, out double wins, out double losses, out double draws)
        {
            var scoresByPlayer = this.context.Scores.Where(s => s.PlayerId == player.Id).ToList();
            var scoresByOpponent = this.context.Scores.Where(s => s.PlayerId == opponent.Id).ToList();
            var scoreIdBtwPlayers = scoresByPlayer.Select(sp => sp.MatchId).Intersect(scoresByOpponent.Select(sbo => sbo.MatchId)).ToList();

            var scoresBtwPlayers = new List<Scores>();

            foreach (var scoreId in scoreIdBtwPlayers)
            {
                scoresBtwPlayers.AddRange(this.context.Scores.Where(s => s.MatchId == scoreId));
            }

            var groupedScoresBtwPlayers = scoresBtwPlayers.GroupBy(sp => sp.MatchId)
                                                          .Select(scoresGroup => new
                                                          {
                                                              MatchId = scoresGroup.Key,
                                                              PlayerScore = scoresGroup.Where(sc => sc.PlayerId == player.Id).Select(sc => sc.Value).FirstOrDefault(),
                                                              OpponentScore = scoresGroup.Where(sc => sc.PlayerId == opponent.Id).Select(sc => sc.Value).FirstOrDefault()
                                                          }).ToList();

            wins = 0.0;
            losses = 0.0;
            draws = 0.0;

            foreach (var match in groupedScoresBtwPlayers)
            {
                if (match.PlayerScore > match.OpponentScore)
                {
                    wins++;
                }
                else if (match.PlayerScore < match.OpponentScore)
                {
                    losses++;
                }
                else
                {
                    draws++;
                }
            }
        }

        private static IQueryable<Player> FilterByFullName(IQueryable<Player> players, string fullName)
        {
            if (!string.IsNullOrEmpty(fullName))
            {
                return players.Where(p => p.FullName.Contains(fullName));
            }

            return players;
        }

        private static IQueryable<Player> FilterByCountry(IQueryable<Player> players, string countryName)
        {
            if (!string.IsNullOrEmpty(countryName))
            {
                return players.Where(p => p.Country.Name.Contains(countryName));
            }

            return players;
        }

        private static IQueryable<Player> FilterBySportClub(IQueryable<Player> players, string sportClubName)
        {
            if (!string.IsNullOrEmpty(sportClubName))
            {
                return players.Where(p => p.SportClub.Name.Contains(sportClubName));
            }

            return players;
        }

        private static IQueryable<Player> SortBy(IQueryable<Player> players, string sortCriteria)
        {
            if (string.IsNullOrEmpty(sortCriteria))
            {
                return players;
            }
            switch (sortCriteria.ToLower())
            {
                case "fullname":
                    return players.OrderBy(player => player.FullName);
                case "sportclub":
                    return players.OrderBy(player => player.SportClub.Name);
                case "country":
                    return players.OrderBy(player => player.Country.Name);
                default:
                    return players;
            }
        }

        private static IQueryable<Player> Order(IQueryable<Player> players, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                return players;
            }
            switch (sortOrder.ToLower())
            {
                case "desc":
                    return players.Reverse();
                default:
                    return players;
            }
        }

        private static IQueryable<Player> Paginate(IQueryable<Player> players, int pageNumber, int pageSize)
        {
            return players.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        private IQueryable<Player> GetPlayers()
        {
            return this.context.Players.Where(p => p.IsDeleted == false)
                           .Include(p => p.Country)
                           .Include(p => p.SportClub)
                           .Include(p => p.Tournaments)
                           .Include(p => p.Scores)
                                .ThenInclude(s => s.Match)
                           .Include(p => p.User);                   
        }
    }
}
