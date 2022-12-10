using CloudinaryDotNet;
using Mailjet.Client.Resources;
using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.Enums;
using MatchScore.Models.QueryParameters;
using MatchScore.Repositories.Contracts;
using MatchScore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Services
{
    public class MatchesService : IMatchesService
    {
        private readonly IMatchesRepository matchesRepository;
        private readonly IModelMapper modelMapper;
        private readonly IEmailSender emailSender;
        private readonly IPlayersRepository playersRepository;
        public MatchesService(IMatchesRepository matchesRepository, IModelMapper modelMapper, IEmailSender emailSender, IPlayersRepository playersRepository)
        {
            this.matchesRepository = matchesRepository;
            this.modelMapper = modelMapper;
            this.emailSender = emailSender;
            this.playersRepository = playersRepository;
        }

        public List<MatchDto> GetAll()
        {
            return this.matchesRepository.GetAll()
                                          .Select(m => this.modelMapper.MapMatchToDto(m))
                                          .ToList();
        }

        public MatchDto GetMatchById(int matchId)
        {
            Match match = this.matchesRepository.GetMatchById(matchId);
            return this.modelMapper.MapMatchToDto(match);
        }

        public List<MatchDto> GetMatchesByParticipant(int participantId)
        {
            return this.matchesRepository.GetMatchesByParticipant(participantId)
                                         .Select(m => this.modelMapper.MapMatchToDto(m))
                                         .ToList(); 
        }

        public List<MatchDto> GetMatchesByTournament(int tournamentId)
        {
            return this.matchesRepository.GetMatchesByTournament(tournamentId)
                                         .Select(m => this.modelMapper.MapMatchToDto(m))
                                         .ToList();
        }

        public List<MatchDto> GetMatchesByRound(int tournamentId, int roundId)
        {
            return this.matchesRepository.GetMatchesByRound(tournamentId, roundId)
                                         .Select(m => this.modelMapper.MapMatchToDto(m))
                                         .ToList();
        }

        public List<MatchDto> GetMatchesByDirector(int directorId)
        {
            return this.matchesRepository.GetMatchesByDirector(directorId)
                                         .Select(m => this.modelMapper.MapMatchToDto(m))
                                         .ToList();
        }

        public PlayerDto GetMatchWinner(int matchId)
        {
            return this.modelMapper.MapPlayerToDto(this.matchesRepository.GetMatchWinner(matchId));
        }

        public PaginatedList<MatchDto> FilterBy(MatchQueryParameters filterParameters)
        {
            PaginatedList<Match> filterList = this.matchesRepository.FilterBy(filterParameters);
            
            if (filterList.Count == 0)
            {
                throw new EntityNotFoundException("There is no match meeting the criteria.");
            }
            
            List<MatchDto> matchesDto = new List<MatchDto>(filterList.Select(m => modelMapper.MapMatchToDto(m))).ToList();
            PaginatedList<MatchDto> matches = new PaginatedList<MatchDto>(matchesDto, filterList.TotalPages, filterList.PageNumber);
            
            return matches;
        }

        public async Task<MatchDto> Create(Match matchDto)
        {
             MatchDto match = this.modelMapper.MapMatchToDto(this.matchesRepository.Create(matchDto));

            // Send email to players
            Player player1 = this.playersRepository.GetByFullName(match.Players[0]);
            Player player2 = this.playersRepository.GetByFullName(match.Players[1]);
            if (player1.User != null)
            {
                await this.emailSender.SendMatchEmailAsync(player1.User.Email, match.Id, match.Date, player2.FullName, "newMatch");
            }
            if (player2.User != null)
            {
                await this.emailSender.SendMatchEmailAsync(player2.User.Email, match.Id, match.Date, player1.FullName, "newMatch");
            }
            
            return match;
        }

        public async Task<MatchDto> UpdateMatch(int matchId, MatchDto matchDto)
        {
            try
            {
                Match matchToUpdate = this.matchesRepository.GetMatchById(matchId);
                matchToUpdate = await this.UpdateMatchDate(matchId, matchDto, matchToUpdate);
                matchToUpdate = this.UpdateMatchFormat(matchId, matchDto, matchToUpdate);
                matchToUpdate = this.UpdateMatchParticipant1(matchId, matchDto, matchToUpdate);
                matchToUpdate = this.UpdateMatchParticipant2(matchId, matchDto, matchToUpdate);
                matchToUpdate = this.UpdateMatchTournament(matchId, matchDto, matchToUpdate);
                matchToUpdate = this.UpdateMatchScore(matchId, matchDto, matchToUpdate);
                matchToUpdate = this.UpdateMatchDirector(matchId, matchDto, matchToUpdate);
                matchToUpdate = this.UpdateMatchStatus(matchId, matchDto, matchToUpdate);

                return this.modelMapper.MapMatchToDto(matchToUpdate);
            }
            catch (EntityNotFoundException)
            {
                throw;
            }
            
        }

        public void Delete(int matchId)
        {
            this.matchesRepository.Delete(matchId);
        }

        private async Task<Match> UpdateMatchDate(int matchId, MatchDto matchDto, Match matchToUpdate)
        {
            if (matchDto.Date != null)
            {
                matchToUpdate = this.matchesRepository.UpdateMatchDate(matchId, matchDto.Date.ToString());
                // Send email to players
                Player player1 = matchToUpdate.Scores[0].Player;
                Player player2 = matchToUpdate.Scores[1].Player;
                if (player1.User != null)
                {
                    await this.emailSender.SendMatchEmailAsync(player1.User.Email, matchToUpdate.Id, matchToUpdate.Date, player2.FullName, "updateMatch");
                }
                if (player2.User != null)
                {
                    await this.emailSender.SendMatchEmailAsync(player2.User.Email, matchToUpdate.Id, matchToUpdate.Date, player1.FullName, "updateMatch");
                }
            }
            
            return matchToUpdate;
        }

        private Match UpdateMatchFormat(int matchId, MatchDto matchDto, Match matchToUpdate)
        {
            if (matchDto.Format != matchToUpdate.Format.ToString())
            {
                MatchFormat format = Enum.Parse<MatchFormat>(matchDto.Format);
                matchToUpdate = this.matchesRepository.UpdateMatchFormat(matchId, format);
            }

            return matchToUpdate;
        }

        private Match UpdateMatchParticipant1(int matchId, MatchDto matchDto, Match matchToUpdate)
        {
            if (!string.IsNullOrEmpty(matchDto.Players[0]))
            {
                matchToUpdate = this.matchesRepository.UpdateMatchParticipant(matchId, matchDto.Players[0], matchToUpdate.Scores[0].Player.FullName);
            }

            return matchToUpdate;
        }

        private Match UpdateMatchParticipant2(int matchId, MatchDto matchDto, Match matchToUpdate)
        {
            if (!string.IsNullOrEmpty(matchDto.Players[1]))
            {
                matchToUpdate = this.matchesRepository.UpdateMatchParticipant(matchId, matchDto.Players[1], matchToUpdate.Scores[1].Player.FullName);
            }

            return matchToUpdate;
        }

        private Match UpdateMatchTournament(int matchId, MatchDto matchDto, Match matchToUpdate)
        {
            if (matchDto.TournamentId != null)
            {
                matchToUpdate = this.matchesRepository.UpdateMatchTournament(matchId, (int)matchDto.TournamentId);
            }

            return matchToUpdate;
        }

        private Match UpdateMatchScore(int matchId, MatchDto matchDto, Match matchToUpdate)
        {
            if (matchDto.Score1 != 0)
            {
                matchToUpdate = this.matchesRepository.UpdateMatchScore(matchId, matchDto.Players[0], matchDto.Score1);
            }
            if (matchDto.Score2 != 0)
            {
                matchToUpdate = this.matchesRepository.UpdateMatchScore(matchId, matchDto.Players[1], matchDto.Score2);
            }

            return matchToUpdate;
        }
        private Match UpdateMatchDirector(int matchId, MatchDto matchDto, Match matchToUpdate)
        {
            if (matchDto.DirectorId != 0)
            {
                matchToUpdate = this.matchesRepository.UpdateMatchDirector(matchId, matchDto.DirectorId);
            }

            return matchToUpdate;
        }

        private Match UpdateMatchStatus(int matchId, MatchDto matchDto, Match matchToUpdate)
        {
            if (matchDto.Status != matchToUpdate.Status.ToString())
            {
                Status status = Enum.Parse<Status>(matchDto.Status);
                matchToUpdate = this.matchesRepository.UpdateMatchStatus(matchId, status);
            }

            return matchToUpdate;
        }

       
    }
}
