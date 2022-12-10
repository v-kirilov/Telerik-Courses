using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Repositories.Contracts;
using MatchScore.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using MatchScore.QueryParameters;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MatchScore.Services
{
    public class TournamentsService : ITournamentsService
    {
        private readonly ITournamentsRepository tournamentsRepository;
        private readonly IPlayersRepository playersRepository;
        private readonly IModelMapper modelMapper;
        private readonly IMatchesService matchesService;
        private readonly IEmailSender emailSender;

        public TournamentsService(ITournamentsRepository tournamentsRepository, IModelMapper modelMapper, IPlayersRepository playersRepository, IMatchesService matchesService, IEmailSender emailSender)
        {
            this.tournamentsRepository = tournamentsRepository;
            this.playersRepository = playersRepository;
            this.modelMapper = modelMapper;
            this.matchesService = matchesService;
            this.emailSender = emailSender;
        }

        public async Task<TournamentDto> CreateTournament(Tournament tournament)
        {

            Tournament newTournament = this.tournamentsRepository.CreateTournament(tournament);

            if (tournament.TournamentPlayers.Count != 0)
            {
                foreach (var player in tournament.TournamentPlayers)
                {
                    if (player.User != null)
                    {
                        await this.emailSender.SendMailWhenAddedToTournament(player.User.Email, newTournament.Id, "added");
                    }
                }
            }
            return modelMapper.MapTournamentToDto(newTournament);
        }

        public TournamentDto CreateRound(int tournamentId)
        {
            Tournament tournament = this.tournamentsRepository.GetById(tournamentId);
            tournament.Status = Models.Enums.Status.Current;
            List<string> players = new List<string>();

            if (tournament.Format == Models.Enums.TournamentFormat.Knockout)
            {
                //Take names to put in round and create Matches from them!
                if (tournament.Rounds.Count == 0)
                {
                    players = tournament.TournamentPlayers.Select(x => x.FullName).ToList();
                }
                else
                {
                    players = this.TakeWinners(tournament.Rounds[tournament.Rounds.Count - 1]);
                }
                if (players.Count == 1)
                {
                    //Finish tournaments !!!!!!!!!!!!!
                    tournament.Winner = players[0];
                    tournament.Status = Models.Enums.Status.Past;
                    tournament.EndDate = System.DateTime.Now;
                    return this.modelMapper.MapTournamentToDto(this.tournamentsRepository.UpdateTournamentRounds(tournament));

                }
                //Create Matches and fill them with players.
                Round round = this.tournamentsRepository.CreateRound(tournament);
                Random random = new Random();

                while (players.Count != 0)
                {
                    List<string> matchPlayers = new List<string>();
                    var rNum = random.Next(0, players.Count / 2);
                    matchPlayers.Add(players[rNum]);
                    matchPlayers.Add(players[players.Count - 1]);
                    players.RemoveAt(players.Count - 1);
                    players.RemoveAt(rNum);
                    MatchDto matchDto = new MatchDto("ScoreLimited", tournamentId, round.Id, tournament.DirectorId, "Current", matchPlayers);

                    Match match = this.modelMapper.MapDtoToMatch(matchDto);
                    round.Matches.Add(match);
                    this.tournamentsRepository.UpdateRound(round);
                }
                return this.modelMapper.MapTournamentToDto(this.tournamentsRepository.UpdateTournamentRounds(tournament));
            }
            else
            {
                players = tournament.TournamentPlayers.Select(x => x.FullName).ToList();

                for (int i = 0; i < players.Count - 1; i++)
                {
                    Round round = this.tournamentsRepository.CreateRound(tournament);
                    for (int j = 0; j < players.Count / 2; j++)
                    {
                        MatchDto matchDto = new MatchDto("TimeLimited", tournamentId, round.Id, tournament.DirectorId, "Current", new List<string>() { players[j], players[players.Count - 1 - j] });
                        Match match = this.modelMapper.MapDtoToMatch(matchDto);
                        round.Matches.Add(match);
                        this.tournamentsRepository.UpdateRound(round);
                    }
                    players.Insert(1, players[players.Count - 1]);
                    players.RemoveAt(players.Count - 1);
                }
                return this.modelMapper.MapTournamentToDto(this.tournamentsRepository.UpdateTournamentRounds(tournament));
            }
            //return this.modelMapper.MapRoundToDto();
            throw new EntityNotFoundException();
        }

        private List<string> TakeWinners(Round round)
        {
            List<string> players = new List<string>();
            foreach (var match in round.Matches)
            {
                var winner = this.matchesService.GetMatchWinner(match.Id);
                players.Add(winner.FullName);
            }
            return players;
        }

        public void DeleteTournament(int id)
        {
            var tournament = this.tournamentsRepository.GetById(id);
            if (tournament.Rounds.Count != 0)
            {
                foreach (var round in tournament.Rounds)
                {
                    if (round.Matches.Count != 0)
                    {
                        foreach (var match in round.Matches)
                        {
                            this.matchesService.Delete(match.Id);
                        }
                        this.tournamentsRepository.DeleteRound(tournament.Id, round.Id);
                    }
                }
            }
            this.tournamentsRepository.Delete(id);
        }
        public void DeleteRound(int tournamentId, int roundId)
        {
            this.tournamentsRepository.DeleteRound(tournamentId, roundId);
        }

        public List<TournamentDto> GetAll()
        {
            List<Tournament> result = this.tournamentsRepository.GetAll();
            return result
                .Select(x => modelMapper.MapTournamentToDto(x))
                .ToList();
        }

        public PaginatedList<TournamentDto> FilterBy(TournamentQueryParameters filterPar)
        {
            PaginatedList<Tournament> filterdList = this.tournamentsRepository.FilterBy(filterPar);
            if (filterdList.Count == 0)
            {
                throw new EntityNotFoundException("There are no Tournamnets matching your query.");
            }
            List<TournamentDto> tournamentDtos = new List<TournamentDto>(filterdList.Select(x => modelMapper.MapTournamentToDto(x))).ToList();
            PaginatedList<TournamentDto> result = new PaginatedList<TournamentDto>(tournamentDtos, filterdList.TotalPages, filterdList.PageNumber);
            return result;
        }
        public PaginatedList<TournamentDto> FilterByForApi(TournamentQueryParameters filterPar)
        {
            PaginatedList<Tournament> filterdList = this.tournamentsRepository.FilterBy(filterPar);
            if (filterdList.Count == 0)
            {
                throw new EntityNotFoundException("There are no Tournamnets matching your query.");
            }
            List<TournamentDto> tournamentDtos = new List<TournamentDto>(filterdList.Select(x => modelMapper.MapTournamentToDtoDetails(x))).ToList();
            PaginatedList<TournamentDto> result = new PaginatedList<TournamentDto>(tournamentDtos, filterdList.TotalPages, filterdList.PageNumber);
            return result;
        }
        public TournamentDto GetById(int id)
        {
            var tournament = this.tournamentsRepository.GetById(id);
            var tournamentDto = this.modelMapper.MapTournamentToDtoDetails(tournament);
            return tournamentDto;
        }

        public TournamentDto UpdateTournament(int id, TournamentDto tourDto, int directorId)
        {
            bool duplicateExist = true;
            try
            {
                if (this.tournamentsRepository.GetIdOfTournament(tourDto.Id) == id)
                {
                    duplicateExist = false;
                }
            }
            catch (EntityNotFoundException)
            {
                duplicateExist = false;
            }
            if (duplicateExist)
            {
                throw new DuplicateEntityException();
            }
            Tournament tourToUpdate = this.modelMapper.MapTournamentCreate(tourDto, directorId);
            tourToUpdate.Id = id;
            Tournament updatedTournament = this.tournamentsRepository.UpdateTournament(tourToUpdate);

            return this.modelMapper.MapTournamentToDto(updatedTournament);
        }


        public RoundDto UpdateTournamentRound(Round round, int roundNumber, int tournamentId)
        {
            this.tournamentsRepository.UpdateRound(round, roundNumber, tournamentId);
            return modelMapper.MapRoundToDto(round);
        }

        public RoundDto GetRoundById(int tournamentId, int roundId)
        {
            Round round = this.tournamentsRepository.GetRoundById(tournamentId, roundId);
            return this.modelMapper.MapRoundToDto(round);
        }

        public RoundDto GetRoundByRoundNumber(int tournamentId, int roundNumber)
        {
            Round round = this.tournamentsRepository.GetRoundByRoundNumber(tournamentId, roundNumber);
            return this.modelMapper.MapRoundToDto(round);
        }

        public List<RoundDto> GetRounds(int tournamentId)
        {
            List<Round> rounds = this.tournamentsRepository.GetRounds(tournamentId);

            return rounds.Select(x => this.modelMapper.MapRoundToDto(x)).ToList();
        }

        public List<PlayerDto> GetPlayersOfTournament(int tournamentId)
        {
            List<Player> players = this.tournamentsRepository.GetPlayersOfTournament(tournamentId);

            return players.Select(x => this.modelMapper.MapPlayerToDto(x)).ToList();
        }
        public PlayerDto GetSinglePLayer(int tournamentId, int playerId)
        {
            List<Player> players = this.tournamentsRepository.GetPlayersOfTournament(tournamentId);
            Player player = players.FirstOrDefault(p => p.Id == playerId);
            if (player == null)
            {
                throw new EntityNotFoundException($"There is no player with id{playerId} in this tournament!");
            }
            return this.modelMapper.MapPlayerToDto(player);
        }

        public async Task<PlayerDto> AddPlayerToTournament(int playerId, int tournamentId)
        {

            Player player = this.playersRepository.GetById(playerId);

            this.tournamentsRepository.AddPlayerToTournament(player, tournamentId);
            if (player.User != null)
            {
                await this.emailSender.SendMailWhenAddedToTournament(player.User.Email, tournamentId, "added");
            }

            return this.modelMapper.MapPlayerToDto(player);
        }
        public async Task<int> AddPlayerToTournament(Player player, int tournamentId)
        {
            if (player.User != null)
            {
                await this.emailSender.SendMailWhenAddedToTournament(player.User.Email, tournamentId, "added");
            }

            this.tournamentsRepository.AddPlayerToTournament(player, tournamentId);
            return player.Id;

        }

        public async Task<int> RemovePlayerFromTournament(int playerId, int tournamentId)
        {
            var player = this.playersRepository.GetById(playerId);

            if (player.User != null)
            {
                await this.emailSender.SendMailWhenAddedToTournament(player.User.Email, tournamentId, "removed");
            }

            this.tournamentsRepository.RemovePlayerFromTournament(player, tournamentId);

            return player.Id;
        }

        //Check this method !!!!!!!
        public bool IsDirector(User director, int tournamentId)
        {
            if (director.Role.Name == "Admin")
            {
                return true;
            }
            if (director.Tournaments.FirstOrDefault(t => t.Id == tournamentId) != null)
            {
                return true;
            }
            else
            {
                throw new UnauthorizedOperationException("You are not the director of this tournamet!");
            }
        }
        public bool IsDirectorOfCurrentTournament(User director, int tournamentId)
        {
            
            if (director.Tournaments.FirstOrDefault(t => t.Id == tournamentId) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsDirectorOrAdmin(User user)
        {
            if (user.Role.Name == "Admin" || user.Role.Name == "Director")
            {
                return true;
            }
            else
            {
                throw new UnauthorizedOperationException("You are not admin or director!");
            }
        }

        public bool IsDirectorOrAdminView(User user, int tournamentId)
        {
            if (user.Role.Name == "Admin")
            {
                return true;
            }
            if (user.Tournaments.FirstOrDefault(t => t.Id == tournamentId) != null)
            {
                return true;
            }
            return false;
        }

        public List<TournamentDto> GetByPlayer(int playerId)
        {
            List<Tournament> result = this.tournamentsRepository.GetByPlayer(playerId);
            return result
                .Select(x => modelMapper.MapTournamentToDto(x))
                .ToList();
        }

        public TournamentDto GetTournamentIdByMatchId(int matchId)
        {
            int id = this.tournamentsRepository.GetTournamentIdByMatchId(matchId);
            var tournament = this.GetById(id);
            return tournament;
        }

        public void CreateStanding(List<string> players, int tournamentId)
        {
            this.tournamentsRepository.CreateStanding(players, tournamentId);
        }

        public void UpdateTournamentStandings(int tournamentId, List<string> results)
        {
            this.tournamentsRepository.UpdateTournamentStandings(tournamentId, results);
        }
        public void UpdateFinishedTournament(TournamentDto dto)
        {
            this.tournamentsRepository.UpdateFinishedTournament(dto);
        }
    }
}
