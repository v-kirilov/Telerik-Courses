using MatchScore.Data;
using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.Enums;
using MatchScore.QueryParameters;
using MatchScore.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MatchScore.Repositories
{
    public class TournamentsRepository : ITournamentsRepository
    {
        private readonly ApplicationContext context;
        private readonly IPlayersRepository playersRepository;

        public TournamentsRepository(ApplicationContext context, IPlayersRepository playersRepository)
        {
            this.context = context;
            this.playersRepository = playersRepository;

        }

        private IQueryable<Tournament> GetTournaments()
        {
            return this.context.Tournaments
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Director)
                .Include(x => x.Standings)
                .Include(x => x.TournamentPlayers)
                .Include(x => x.Rounds)
                    .ThenInclude(x => x.Matches)
                    .ThenInclude(x => x.Scores)
                    .ThenInclude(x=>x.Player);
        }

        public List<Tournament> GetAll()
        {
            return this.GetTournaments().Where(x => x.IsDeleted != true).ToList();
        }

        public PaginatedList<Tournament> FilterBy(TournamentQueryParameters filterPar)
        {
            List<Tournament> result = this.GetAll();
            if (!string.IsNullOrEmpty(filterPar.Title))
            {
                result = result.FindAll(x => x.Title.Contains(filterPar.Title));
            }
            if (!string.IsNullOrEmpty(filterPar.FullName))
            {
                result = result.FindAll(x => x.TournamentPlayers.Any(x=>x.FullName==filterPar.FullName));
            }
            if (filterPar.UserId.HasValue)
            {
                result = result.FindAll(x => x.DirectorId == filterPar.UserId);
            }
            if (!string.IsNullOrEmpty(filterPar.ManagerEmail))
            {
                result = result.FindAll(x => x.Director.Email == filterPar.ManagerEmail);
            }
            if (!string.IsNullOrEmpty(filterPar.Format))
            {
                result = result.FindAll(x => x.Format.ToString() == filterPar.Format);
            }
            if (!string.IsNullOrEmpty(filterPar.Prize))
            {
                result = result.FindAll(x => x.Prize.ToString() == filterPar.Prize);
            }
            if (!string.IsNullOrEmpty(filterPar.Status))
            {
                result = result.FindAll(x => x.Status.ToString() == filterPar.Status);
            }
            double totalPages = Math.Ceiling((1.0 * result.Count) / filterPar.PageSize);

            result = Paginate(result, filterPar.PageNumber, filterPar.PageSize);
            return new PaginatedList<Tournament>(result.ToList(), (int)totalPages, filterPar.PageNumber);
        }

        private List<Tournament> Paginate(List<Tournament> result, int pageNumber, int pageSize)
        {
            return result.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public Tournament CreateTournament(Tournament tournament)
        {
            var players = new List<Player>();
            if (tournament.TournamentPlayers.Count != 0)
            {
                players.AddRange(tournament.TournamentPlayers);
                tournament.TournamentPlayers.Clear();

            }
            this.context.Add(tournament);
            this.context.SaveChanges();

            foreach (var player in players)
            {
                AddPlayerToTournament(player, tournament.Id);
            }

            return tournament;
        }

        public void Delete(int id)
        {
            Tournament tournament = this.GetTournaments().FirstOrDefault(x => x.Id == id);
            if (tournament == null)
            {
                throw new EntityNotFoundException($"There is no Tournament with id:{id}");
            }
            tournament.IsDeleted = true;
            this.context.Update(tournament);
            this.context.SaveChanges();
        }
        public void DeleteRound(int tournamentId, int roundId)
        {
            Round round = this.GetRoundById(tournamentId, roundId);
            round.IsDeleted = true;
            this.context.Update(round);
            this.context.SaveChanges();
        }


        public Tournament GetById(int id)
        {
            Tournament tournament = this.GetTournaments().FirstOrDefault(x => x.Id == id);
            return tournament ?? throw new EntityNotFoundException($"There is no Tournament with id:{id}");
        }

        public Tournament GetByTitle(string title)
        {
            Tournament tournament = this.GetTournaments().FirstOrDefault(x => x.Title == title);
            return tournament ?? throw new EntityNotFoundException($"There is no Tournament with Title:{title}");
        }
        public Tournament GetByDirector(User director)
        {
            Tournament tournament = this.GetTournaments().FirstOrDefault(x => x.Director.Email == director.Email);
            return tournament ?? throw new EntityNotFoundException($"There is no Tournament with Manager:{director.Email}");
        }

        public Round CreateRound(Tournament tournament)
        {

            Round round = new Round();
            if (tournament.Rounds.Count == 0)
            {
                round.RoundNumber = 1;
            }
            else
            {
                round.RoundNumber = tournament.Rounds.Count + 1;
            }
            round.TournamentId = tournament.Id;
            round.IsDeleted = false;
            round.Status = Status.Future;
            this.context.Add(round);
            this.context.SaveChanges();
            return round;
        }

        public void UpdateRound(Round roundToUpdate)
        {
            this.context.Update(roundToUpdate);
            this.context.SaveChanges();
        }

        public Round UpdateRound(Round round, int roundNumber, int tournamentId)
        {
            Tournament tournamentToUpdate = this.GetById(tournamentId);

            Round roundToUpdate = this.GetRoundByRoundNumber(tournamentId, roundNumber);
            roundToUpdate.StartDate = round.StartDate;
            this.context.Update(roundToUpdate);
            this.context.SaveChanges();
            return roundToUpdate;

        }
        public Tournament UpdateTournament(Tournament updateTournament)
        {
            var tourToUpdate = this.GetById(updateTournament.Id);
            if (updateTournament.Title != null)
            {
                tourToUpdate.Title = updateTournament.Title;
            }
            if (updateTournament.Prize != 0)
            {
                tourToUpdate.Prize = updateTournament.Prize;
            }
            if (updateTournament.Status != tourToUpdate.Status)
            {
                tourToUpdate.Status = updateTournament.Status;
            }
            if (updateTournament.StartDate != tourToUpdate.StartDate)
            {
                tourToUpdate.StartDate = updateTournament.StartDate;
            }
            if (updateTournament.EndDate != tourToUpdate.EndDate)
            {
                tourToUpdate.EndDate = updateTournament.EndDate;
            }
            if (updateTournament.Format != tourToUpdate.Format)
            {
                tourToUpdate.Format = updateTournament.Format;
            }
            this.context.Update(tourToUpdate);
            this.context.SaveChanges();
            return tourToUpdate;
        }

        public Tournament UpdateTournamentRounds(Tournament updateTournament)
        {
            this.context.Update(updateTournament);
            this.context.SaveChanges();
            return updateTournament;
        }
        public void UpdateTournamentStandings(int tournamentId, List<string> results)
        {
            Tournament tournament = this.GetById(tournamentId);
            var standing1 = tournament.Standings.FirstOrDefault(x => x.PlayerName == results[0]);
            standing1.Goals += int.Parse(results[2]);
            standing1.Points += int.Parse(results[4]);

            var standing2 = tournament.Standings.FirstOrDefault(x => x.PlayerName == results[1]);
            standing2.Goals += int.Parse(results[3]);
            standing2.Points += int.Parse(results[5]);
            this.context.Update(standing1);
            this.context.Update(standing2);
            this.context.Update(tournament);
            this.context.SaveChanges();
        }
        public void CreateStanding(List<string> players, int tournamenId)
        {
            var tournament = this.GetById(tournamenId);
            foreach (var fullName in players)
            {
                Standing standing = new Standing(fullName, 0);
                tournament.Standings.Add(standing);
            }
            this.context.Update(tournament);
            this.context.SaveChanges();
        }
        public Tournament UpdateTournamentTitle(int id, string title)
        {
            Tournament tourToUpdate = this.GetTournaments().FirstOrDefault(x => x.Id == id);
            tourToUpdate.Title = title;
            this.context.Update(tourToUpdate);
            this.context.SaveChanges();
            return tourToUpdate;
        }

        public Tournament UpdateTournamentPrize(int id, TournamentPrize prize)
        {
            Tournament tourToUpdate = this.GetTournaments().FirstOrDefault(x => x.Id == id);
            tourToUpdate.Prize = prize;
            this.context.Update(tourToUpdate);
            this.context.SaveChanges();
            return tourToUpdate;
        }
        public Tournament UpdateTournamentDate(int id, DateTime StartDate, DateTime EndDate)
        {
            Tournament tourToUpdate = this.GetTournaments().FirstOrDefault(x => x.Id == id);
            tourToUpdate.StartDate = StartDate;
            tourToUpdate.EndDate = EndDate;
            this.context.Update(tourToUpdate);
            this.context.SaveChanges();
            return tourToUpdate;
        }

        public int GetIdOfTournament(int id)
        {
            var tournament = this.GetTournaments().FirstOrDefault(x => x.Id == id);
            if (tournament == null)
            {
                throw new EntityNotFoundException($"There is no Tournament with id{id}");
            }
            return tournament.Id;
        }

        public Round GetRoundById(int tournamentId, int roundId)
        {
            Round round = this.GetById(tournamentId).Rounds.FirstOrDefault(x => x.Id == roundId);
            return round ?? throw new EntityNotFoundException($"There is no Round with Id:{roundId}");
        }

        public Round GetRoundByRoundNumber(int tournamentId, int roundNumber)
        {
            Round round = this.GetById(tournamentId).Rounds.FirstOrDefault(x => x.RoundNumber == roundNumber);
            return round ?? throw new EntityNotFoundException($"There is no Round with Round Number:{roundNumber}");
        }
        public List<Round> GetRounds(int tournamentId)
        {
            List<Round> result = this.GetById(tournamentId).Rounds;
            if (result.Count == 0 || result == null)
            {
                throw new EntityNotFoundException($"There are no Rounds in Tournament with Id:{tournamentId}");
            }
            return result;
        }



        public Player AddPlayerToTournament(Player player, int tournamentId)
        {


            Tournament tournament = this.GetById(tournamentId);
            tournament.TournamentPlayers.Add(player);
            player.Tournaments.Add(tournament);

            this.context.Update(tournament);
            this.context.Update(player);

            this.context.SaveChanges();
            return player;
        }
        public void RemovePlayerFromTournament(Player player, int tournamentId)
        {
            Tournament tournament = this.GetById(tournamentId);
            tournament.TournamentPlayers.Remove(player);
            player.Tournaments.Remove(tournament);
            this.context.Update(player);
            this.context.Update(tournament);
            this.context.SaveChanges();
        }
        public List<Player> GetPlayersOfTournament(int tournamentId)
        {
            Tournament tournament = this.GetById(tournamentId);
            if (tournament.TournamentPlayers.Count == 0)
            {
                throw new EntityNotFoundException($"There are no Players in Tournament with Id:{tournamentId}");
            }
            return tournament.TournamentPlayers;
        }
        private IQueryable<TournamentParticipants> GetTournamentParticipants()
        {
            return this.context.TournamentParticipants
                .Include(x => x.Tournament)
                .Include(x => x.Player);
        }

        public List<Tournament> GetByPlayer(int playerId)
        {
            IEnumerable<Tournament> tournament = this.GetAll().Where(x => x.TournamentPlayers.Any(x => x.UserId == playerId));
            return tournament.ToList();
        }

        public int GetTournamentIdByMatchId(int matchId)
        {
            var rounds = this.context.Rounds
                .Include(x => x.Matches).ToList();
            var round = rounds.FirstOrDefault(x => x.Matches.Any(x => x.Id == matchId));
            var tournament = this.GetAll().FirstOrDefault(x => x.Rounds.Any(x => x.Id == round.Id));
            return tournament.Id;
        }

        public void UpdateFinishedTournament(TournamentDto dto)
        {
            var tournamentToUpdate = this.GetById(dto.Id);
            tournamentToUpdate.EndDate = dto.EndDate;
            tournamentToUpdate.Status = dto.Status;
            var winnerStanding = tournamentToUpdate.Standings.OrderByDescending(x => x.Points).ThenByDescending(x => x.Goals).First();
            tournamentToUpdate.Winner = winnerStanding.PlayerName;

            this.context.Update(tournamentToUpdate);
            this.context.SaveChanges();
        }


    }
}
