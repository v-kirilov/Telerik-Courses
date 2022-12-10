using MatchScore.Data;
using MatchScore.Exceptions;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.Enums;
using MatchScore.Models.QueryParameters;
using MatchScore.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MatchScore.Repositories
{
    public class MatchesRepository : IMatchesRepository
    {
        private readonly ApplicationContext context;
        private readonly IPlayersRepository playersRepository;
        private readonly ITournamentsRepository tournamentsRepository;
        private readonly IUsersRepository usersRepository;

        public MatchesRepository(ApplicationContext context, IPlayersRepository playersRepository, ITournamentsRepository tournamentsRepository, IUsersRepository usersRepository)
        {
            this.context = context;
            this.playersRepository = playersRepository;
            this.tournamentsRepository = tournamentsRepository;
            this.usersRepository = usersRepository;
        }

        public List<Match> GetAll()
        {
            List<Match> matches = this.GetMatches().ToList();

            if (matches.Any())
            {
                return matches;
            }
            
            throw new EntityNotFoundException("There is no match.");
            
        }

        public Match GetMatchById(int matchId)
        {
            Match match = this.GetMatches().Where(m => m.Id == matchId).FirstOrDefault();

            return match ?? throw new EntityNotFoundException("The match is not found.");
        }

        public List<Match> GetMatchesByParticipant(int participantId)
        {
            List<Match> matches = this.GetMatches().Where(m => m.Scores.Any(p => p.PlayerId == participantId)).ToList();

            if (matches.Any())
            {
                return matches;
            }
            
            throw new EntityNotFoundException("This player has no match."); 
            
        }

        public List<Match> GetMatchesByTournament(int tournamentId)
        {
            List<Match> matches = this.GetMatches().Where(m => m.TournamentId == tournamentId).ToList();

            if (matches.Any())
            {
                return matches;
            }

            throw new EntityNotFoundException("There is no match for this tournament.");
            
        }

        public List<Match> GetMatchesByRound(int tournamentId, int roundId)
        {
            List<Match> matches = this.GetMatchesByTournament(tournamentId).Where(m => m.RoundId == roundId).ToList();

            if (matches.Any())
            {
                return matches;
            }

            throw new EntityNotFoundException("There is no match for this round.");
            
        }

        public List<Match> GetMatchesByDirector(int directorId)
        {
            List<Match> matches = this.GetMatches().Where(m => m.DirectorId == directorId).ToList();

            if (matches.Any())
            {
                return matches;
            }

            throw new EntityNotFoundException("There is no match for this director.");
            
        }

        public Player GetMatchWinner(int matchId)
        {
            Match match = this.GetMatchById(matchId);

            if (match.Status == Status.Past)
            {
                if (match.Scores[0].Value > match.Scores[1].Value)
                {
                    return match.Scores[0].Player;
                }
                else if (match.Scores[1].Value > match.Scores[0].Value)
                {
                    return match.Scores[1].Player;
                }
                
                throw new EntityNotFoundException("The match has finished in a draw.");
                
            }
            
            throw new EntityNotFoundException("The match is not finished yet.");
            
        }

        public PaginatedList<Match> FilterBy(MatchQueryParameters filterParameters)
        {
            List<Match> result = this.GetAll();
            result = FilterByDate(result, filterParameters.Date);
            result = FilterByParticipant(result, filterParameters.Participant);
            result = FilterByFormat(result, filterParameters.Format);
            result = FilterByTournament(result, filterParameters.Tournament);
            result = FilterByDirector(result, filterParameters.DirectorEmail);
            result = FilterByStatus(result, filterParameters.Status);
            result = SortBy(result, filterParameters.SortBy);
            result = Order(result, filterParameters.SortOrder);
            double totalPages = Math.Ceiling((1.0 * result.Count) / filterParameters.PageSize);
            result = Paginate(result, filterParameters.PageNumber, filterParameters.PageSize);

            return new PaginatedList<Match>(result, (int)totalPages, filterParameters.PageNumber);

        }

        public Match Create(Match match)
        {
            this.context.Matches.Add(match);
            this.context.SaveChanges();

            return match;
        }

        public Match UpdateMatchDate(int matchId, string date)
        {
            Match matchToUpdate = this.GetMatchById(matchId);
            matchToUpdate.Date = DateTime.Parse(date);

            this.context.Update(matchToUpdate);
            this.context.SaveChanges();

            return matchToUpdate;
        }
        public Match UpdateMatchFormat(int matchId, MatchFormat format)
        {
            Match matchToUpdate = this.GetMatchById(matchId);
            matchToUpdate.Format = format;

            this.context.Update(matchToUpdate);
            this.context.SaveChanges();

            return matchToUpdate;
        }

        public Match UpdateMatchParticipant(int matchId, string newName, string oldName)
        {
            Player oldPlayer = this.playersRepository.GetByFullName(oldName);
            Player newPlayer = new Player();
            try
            {
                newPlayer = this.playersRepository.GetByFullName(newName);
            }
            catch (EntityNotFoundException)
            {
                newPlayer.FullName = newName;
                newPlayer = this.playersRepository.Create(newPlayer);
            }
            
            
            Scores matchToUpdate = this.GetPlayerMatches().Where(m => m.MatchId == matchId && m.PlayerId == oldPlayer.Id).FirstOrDefault();

            matchToUpdate.Player = newPlayer;
            matchToUpdate.PlayerId = newPlayer.Id;
            
            this.context.Scores.Update(matchToUpdate);
            this.context.SaveChanges();

            return matchToUpdate.Match;
        }

        public Match UpdateMatchTournament(int matchId, int tournamentId)
        {
            Match matchToUpdate = this.GetMatchById(matchId);
            Tournament tournament = this.tournamentsRepository.GetById(tournamentId);

            matchToUpdate.TournamentId = tournament.Id;
            matchToUpdate.Tournament = tournament;

            this.context.Update(matchToUpdate);
            this.context.SaveChanges();

            return matchToUpdate;
        }

        public Match UpdateMatchScore(int matchId, string playerName, int score)
        {
            Player player = this.playersRepository.GetByFullName(playerName);            
            
            Scores matchToUpdate = this.GetPlayerMatches().Where(m => m.MatchId == matchId && m.PlayerId == player.Id).FirstOrDefault();
            matchToUpdate.Value = score;            

            this.context.Scores.Update(matchToUpdate);
            this.context.SaveChanges();

            Match updatedMatch = this.GetMatchById(matchId);
            return updatedMatch;
        }

        public Match UpdateMatchDirector(int matchId, int directorId)
        {
            Match matchToUpdate = this.GetMatchById(matchId);
            User director = this.usersRepository.GetById(directorId);

            matchToUpdate.DirectorId = director.Id;
            matchToUpdate.Director = director;

            this.context.Update(matchToUpdate);
            this.context.SaveChanges();

            return matchToUpdate;
        }

        public Match UpdateMatchStatus(int matchId, Status status)
        {
            Match matchToUpdate = this.GetMatchById(matchId);
            matchToUpdate.Status = status;

            this.context.Update(matchToUpdate);
            this.context.SaveChanges();

            return matchToUpdate;
        }

        public void Delete(int matchId)
        {
            Match matchToDelete = this.GetMatchById(matchId);
            matchToDelete.IsDeleted = true;

            this.context.Update(matchToDelete);
            this.context.SaveChanges();
        }

        private IQueryable<Match> GetMatches()
        {
            return this.context.Matches.Where(m => m.IsDeleted == false)
                .Include(m => m.Tournament)
                .Include(m => m.Round)
                .Include(m => m.Director)
                .Include(m => m.Scores)
                    .ThenInclude(s => s.Player)
                    .ThenInclude(p => p.SportClub)
                .Include(m => m.Scores)
                    .ThenInclude(s => s.Player)
                    .ThenInclude(p => p.Country)
                .Include(m => m.Scores)
                    .ThenInclude(s => s.Player)
                    .ThenInclude(p => p.User);

        }

        private IQueryable<Scores> GetPlayerMatches()
        {
            return this.context.Scores.Where(m => m.IsDeleted == false)
                .Include(m => m.Match)
                .Include(m => m.Player);
        }
        private static List<Match> Paginate(List<Match> matches, int pageNumber, int pageSize)
        {
            return matches.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        private static List<Match> FilterByDate(List<Match> matches, string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                DateTime newDate = DateTime.ParseExact(date, "yyyy/MM/dd", null);
                return matches.Where(m => m.Date == newDate).ToList();
            }
            
            return matches;
            
        }

        private static List<Match> FilterByParticipant(List<Match> matches, string participant)
        {
            if (!string.IsNullOrEmpty(participant))
            {
                return matches.Where(m => m.Scores.Any(x => x.Player.FullName == participant)).ToList();
            }

            return matches;
            
        }

        private static List<Match> FilterByFormat(List<Match> matches, string format)
        {
            if (!string.IsNullOrEmpty(format))
            {
                return matches.Where(m => m.Format.ToString() == format).ToList();
            }

            return matches;
            
        }

        private static List<Match> FilterByTournament(List<Match> matches, string tournament)
        {
            if (!string.IsNullOrEmpty(tournament))
            {
                return matches.Where(m => m.Tournament.Title.Contains(tournament)).ToList();
            }

            return matches;

        }

        private static List<Match> FilterByDirector(List<Match> matches, string directorEmail)
        {
            if (!string.IsNullOrEmpty(directorEmail))
            {
                return matches.Where(m => m.Director.Email == directorEmail).ToList();
            }            
            
            return matches;
            
        }

        private static List<Match> FilterByStatus(List<Match> matches, string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                return matches.Where(m => m.Status.ToString().Contains(status)).ToList();
            }
                
            return matches;
            
        }

        private static List<Match> SortBy(List<Match> matches, string sortCriteria)
        {
            if (string.IsNullOrEmpty(sortCriteria))
            {
                return matches;
            }
            switch (sortCriteria.ToLower())
            {
                case "date":
                    return matches.OrderBy(match => match.Date).ToList();
                case "format":
                    return matches.OrderBy(match => match.Format.ToString()).ToList();
                case "tournament":
                    return matches.OrderBy(match => match.Tournament.Title).ToList();
                case "email":
                    return matches.OrderBy(match => match.Director.Email).ToList();
                case "status":
                    return matches.OrderBy(match => match.Status.ToString()).ToList();
                default:
                    return matches;
            }
        }

        private static List<Match> Order(List<Match> matches, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                return matches;
            }
            switch (sortOrder.ToLower())
            {
                case "desc":
                    matches.Reverse();
                    return matches;
                default:
                    return matches;
            }
        }
    }
}

