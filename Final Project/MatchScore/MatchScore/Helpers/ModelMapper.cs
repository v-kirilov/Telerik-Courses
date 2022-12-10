using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.Enums;
using MatchScore.Models.ViewModels;
using MatchScore.Repositories.Contracts;
using MatchScore.Services.Contracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchScore.Helpers
{
    public class ModelMapper : IModelMapper
    {
        private const string RequestTypeErrorMessage = "This request type does not exist.";
        private const string RequestStatusErrorMessage = "This request status does not exist.";

        private readonly ICountriesRepository countriesRepository;
        private readonly ISportClubsRepository sportClubsRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IRolesService rolesService;
        private readonly IPlayersRepository playersRepository;
        private readonly IRequestsRepository requestsRepository;
        private readonly IPhotoService photoService;

        public ModelMapper(ICountriesRepository countriesRepository, ISportClubsRepository sportClubsRepository, IUsersRepository usersRepository, IRolesService rolesService, IPlayersRepository playersRepository, IRequestsRepository requestsRepository, IPhotoService photoService)
        {
            this.countriesRepository = countriesRepository;
            this.sportClubsRepository = sportClubsRepository;
            this.usersRepository = usersRepository;
            this.rolesService = rolesService;
            this.playersRepository = playersRepository;
            this.requestsRepository = requestsRepository;
            this.photoService = photoService;
        }

        public TournamentDto MapTournamentToDto(Tournament tournament)
        {
            return new TournamentDto
            {
                Id = tournament.Id,
                Title = tournament.Title,
                DirectorId = tournament.DirectorId,
                Format = tournament.Format,
                Prize = tournament.Prize,
                StartDate = tournament.StartDate,
                EndDate = tournament.EndDate,
                DirectorEmail = tournament.Director.Email,
                PlayersFullName = tournament.TournamentPlayers.Select(x => x.FullName).ToList(),
                Status = tournament.Status,
                IsDeleted = tournament.IsDeleted,
                Winner = tournament.Winner,
                Standings = tournament.Standings,
                Rounds = tournament.Rounds
                .Select(x => MapRoundToDto(x))
                .ToList(),
            };
        }
        public TournamentDto MapTournamentToDtoDetails(Tournament tournament)
        {
            return new TournamentDto
            {
                Id = tournament.Id,
                Title = tournament.Title,
                DirectorId = tournament.DirectorId,
                Format = tournament.Format,
                Prize = tournament.Prize,
                StartDate = tournament.StartDate,
                EndDate = tournament.EndDate,
                IsDeleted = tournament.IsDeleted,
                DirectorEmail = tournament.Director.Email,
                PlayersFullName = tournament.TournamentPlayers.Select(x => x.FullName).ToList(),
                Status = tournament.Status,
                Standings = tournament.Standings,
                Winner = tournament.Winner

            };
        }

        public Tournament MapDtoToTournament(TournamentDto dto, int userId)
        {
            return new Tournament
            {
                Title = dto.Title,
                IsDeleted = dto.IsDeleted,
                Format = dto.Format,
                Prize = dto.Prize,
                Rounds = dto.Rounds.Select(x => MapDtoToRound(x)).ToList(),
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                DirectorId = dto.DirectorId,
                Standings = dto.Standings,
                Winner = dto.Winner
            };
        }
        public Tournament MapTournamentCreate(TournamentDto dto, int userId)
        {
            if (dto.PlayersFullName.Count != 0)
            {
                return new Tournament
                {
                    Title = dto.Title,
                    Format = dto.Format,
                    Prize = dto.Prize,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    DirectorId = userId,
                    TournamentPlayers = new List<Player>(dto.PlayersFullName.Select(x => playersRepository.GetByFullName(x)).ToList())
                };
            }
            return new Tournament
            {
                Title = dto.Title,
                Format = dto.Format,
                Prize = dto.Prize,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                DirectorId = userId
            };
        }

        public RoundDto MapRoundToDto(Round round)
        {
            return new RoundDto
            {
                Id = round.Id,
                TournamentId = round.TournamentId,
                RoundNumber = round.RoundNumber,
                StartDate = round.StartDate,
                Status = round.Status,
                RoundPlayers = round.RoundPlayers,
                Matches = round.Matches
                .Select(x => MapMatchToDto(x))
                .ToList(),
            };
        }
        public Round MapDtoToRound(RoundDto dto)
        {
            return new Round
            {
                TournamentId = dto.TournamentId,
                RoundNumber = dto.RoundNumber,
                StartDate = dto.StartDate,
                Status = dto.Status,
                RoundPlayers = dto.RoundPlayers,
                Matches = dto.Matches
                .Select(x => MapDtoToMatch(x))
                .ToList()
            };
        }

        public MatchDto MapMatchToDto(Match match)
        {
            List<Player> players = match.Scores.Select(x => x.Player).ToList();
            Player player1 = players[0];
            Player player2 = players[1];
            return new MatchDto
            {
                Id = match.Id,
                Date = match.Date,
                Format = match.Format.ToString(),
                TournamentId = match.TournamentId,
                RoundId = match.RoundId,
                DirectorId = match.DirectorId,
                Status = match.Status.ToString(),
                Players = match.Scores.Select(p => p.Player.FullName).ToList(),
                Score1 = match.Scores.Where(pm => pm.PlayerId == player1.Id).Select(p => p.Value).First(),
                Score2 = match.Scores.Where(pm => pm.PlayerId == player2.Id).Select(p => p.Value).First(),
            };
        }


        public Match MapDtoToMatch(MatchDto matchDto)
        {
            List<Player> players = new List<Player>();
            for (int i = 0; i < matchDto.Players.Count(); i++)
            {
                try
                {
                    Player player = this.playersRepository.GetByFullName(matchDto.Players[i]);
                    players.Add(player);

                }
                catch (EntityNotFoundException)
                {
                    Player newPlayer = new Player()
                    {
                        FullName = matchDto.Players[i]
                    };
                    Player player = this.playersRepository.Create(newPlayer);
                    players.Add(player);
                }

            }


            return new Match
            {
                Id = matchDto.Id,
                Date = matchDto.Date,
                Format = Enum.Parse<MatchFormat>(matchDto.Format),
                TournamentId = matchDto.TournamentId,
                RoundId = matchDto.RoundId,
                DirectorId = matchDto.DirectorId,
                Status = matchDto.Status != null ? Enum.Parse<Status>(matchDto.Status) : Status.Future,
                Scores = new List<Scores>()
                {
                    new Scores(){ PlayerId = players[0].Id, MatchId = matchDto.Id, Value = matchDto.Score1 },
                    new Scores(){ PlayerId = players[1].Id, MatchId = matchDto.Id, Value = matchDto.Score2 }
                }
            };
        }

        public SportClub MapDtoToSportClub(SportClubDto sportClubDto)
        {
            return new SportClub
            {
                Name = sportClubDto.Name,
                Players = sportClubDto.Players
                                           .Select(p => MapDtoToPlayer(p))
                                           .ToList()
            };
        }

        public SportClubDto MapSportClubToDto(SportClub sportClub)
        {
            return new SportClubDto
            {
                Id = sportClub.Id,
                Name = sportClub.Name,
                Players = sportClub.Players
                                        .Select(p => MapPlayerToDto(p))
                                        .ToList()
            };
        }

        public Country MapDtoToCountry(CountryDto countryDto)
        {
            return new Country
            {
                Name = countryDto.Name,
                Players = countryDto.Players
                                        .Select(p => MapDtoToPlayer(p))
                                        .ToList()
            };
        }

        public CountryDto MapCountryToDto(Country country)
        {
            return new CountryDto
            {
                Id = country.Id,
                Name = country.Name,
                Players = country.Players
                                    .Select(p => MapPlayerToDto(p))
                                    .ToList()
            };
        }

        public Player MapDtoToPlayer(PlayerDto playerDto)
        {
            return new Player
            {
                FullName = playerDto.FullName,
                Country = playerDto.Country != null ? this.countriesRepository.GetByName(playerDto.Country) : null,
                SportClub = playerDto.SportClub != null ? this.sportClubsRepository.GetByName(playerDto.SportClub) : null,
                User = playerDto.UserEmail != null ? this.usersRepository.GetByEmail(playerDto.UserEmail) : null,
            };
        }

        public PlayerDto MapPlayerToDto(Player player)
        {
            return new PlayerDto
            {
                Id = player.Id,
                FullName = player.FullName,
                Country = player.Country?.Name ?? "none",
                SportClub = player.SportClub?.Name ?? "none",
                UserEmail = player.User?.Email ?? "none",
                PhotoUrl = this.photoService.GetPhotoUrl(player.Id),
                Tournaments = player.Tournaments?
                                    .Select(t => t.Title)
                                    .ToList() ?? new List<string> { "none" },
                TournamentsPlayed = this.playersRepository.CountTournamentsPlayed(player.Id),
                TournamentsWon = this.playersRepository.CountTournamentsWon(player.Id),
                MatchesPlayed = this.playersRepository.CountMatchesPlayed(player.Id),
                MatchesWon = this.playersRepository.CountMatchesWon(player.Id),
                MatchesLost = this.playersRepository.CountMatchesLoss(player.Id),
                MatchesDraw = this.playersRepository.CountMatchesDraw(player.Id),
                MostPlayedOpponent = this.playersRepository.GetNameMostPlayedOpponent(player.Id),
                BestOpponent = this.playersRepository.GetNameBestOpponent(player.Id),
                BestWinLossRatio = this.playersRepository.GetBestWinLossRatio(player.Id),
                WorstOpponent = this.playersRepository.GetNameWorstOpponent(player.Id),
                WorstWinLossRatio = this.playersRepository.GetWorstWinLossRatio(player.Id)

                //.Select(t => MapTournamentToDto(t))
                //.ToList(),
                //MatchesPlayed = this.playersService.MatchesPlayed(player.Id) 
            };
        }

        public User MapRegViewToUser(RegisterViewModel regView)
        {
            return new User
            {
                Email = regView.Email,
                Password = regView.Password
            };
        }

        public EditUserViewModel MapUserDtoToEditView(UserDto dto)
        {
            return new EditUserViewModel
            {
                Id = dto.Id,
                Email = dto.Email,
                Role = dto.Role
            };
        }

        public User MapEditViewToUser(EditUserViewModel editView)
        {
            return new User
            {
                Id = editView.Id,
                Email = editView.Email,
                Password = editView.Password,
            };
        }

        public User MapCreateDtoToUser(CreateUserDto userDto)
        {
            return new User
            {
                Email = userDto.Email,
                Password = userDto.Password
            };
        }

        public UserDto MapUserToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.Name,
                PlayerFullName = user.Player?.FullName,
                Tournaments = user.Tournaments?
                                  .Select(t => MapTournamentToDto(t))
                                  .ToList(),
                Matches = user.Matches?
                              .Select(m => MapMatchToDto(m))
                              .ToList()

            };
        }

        public User MapDtoToUser(UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Role = this.rolesService.GetByName(userDto.Role),
                Player = userDto.PlayerFullName != null ? this.playersRepository.GetByFullName(userDto.PlayerFullName) : null,
                Tournaments = userDto.Tournaments?
                                     .Select(t => MapDtoToTournament(t, t.DirectorId))
                                     .ToList(),
                Matches = userDto.Matches?
                                 .Select(m => MapDtoToMatch(m))
                                 .ToList()
            };
        }

        public User MapUpdateDtoToUser(UpdateUserDto userDto)
        {
            return new User
            {
                Email = userDto.Email,
                Password = userDto.Password,
                Role = this.rolesService.GetByName(userDto.RoleName)
            };
        }

        public Request MapDtoToRequest(RequestDtoName dto)
        {
            var isFound = Enum.TryParse<RequestType>(dto.Type, true, out RequestType result);

            if (!isFound)
            {
                throw new EntityNotFoundException(RequestTypeErrorMessage);
            }

            return new Request
            {
                Type = result,
                PlayerFullName = dto.PlayerFullName
            };
        }

        public Request MapDtoToRequest(int id, RequestDtoName dto)
        {
            var request = this.requestsRepository.GetById(id);
            var isFound = Enum.TryParse<RequestStatus>(dto.Status, true, out RequestStatus result);

            if (!isFound)
            {
                throw new EntityNotFoundException(RequestStatusErrorMessage);
            }

            return new Request
            {
                Id = request.Id,
                PlayerFullName = request.PlayerFullName,
                Status = result,
                Type = request.Type,
                User = request.User,
                UserId = request.UserId
            };
        }

        public RequestDto MapRequestToDto(Request request)
        {
            if (request.Type == RequestType.PromoteToDirector)
            {
                return new RequestDto
                {
                    Id = request.Id,
                    Status = request.Status.ToString(),
                    Type = request.Type.ToString(),
                    UserEmail = request.User.Email,
                };
            }
            else
            {
                return MapRequestToDtoWithName(request);
            }
        }

        public RequestDtoName MapRequestToDtoWithName(Request request)
        {
            return new RequestDtoName
            {
                Id = request.Id,
                Status = request.Status.ToString(),
                Type = request.Type.ToString(),
                UserEmail = request.User.Email,
                PlayerFullName = request?.PlayerFullName
            };
        }

        public Player MapCreateViewToPlayer(CreatePlayerViewModel viewModel)
        {
            return new Player
            {
                FullName = viewModel.FullName,
                Country = this.countriesRepository.GetByName(viewModel.Country),
                SportClub = this.sportClubsRepository.GetByName(viewModel.SportClub)
            };
        }

        public EditPlayerViewModel MapPlayerDtoToEditPlayerViewModel(PlayerDto dto)
        {
            return new EditPlayerViewModel
            {
                Id = dto.Id,
                FullName = dto.FullName,
                Country = dto.Country,
                SportClub = dto.SportClub,
                CurrentProfileImageUrl = dto.PhotoUrl
            };
        }

        public Player MapEditPlayerViewModelToPlayer(EditPlayerViewModel viewModel)
        {
            return new Player
            {
                Id = viewModel.Id,
                FullName = viewModel.FullName,
                Country = viewModel.Country != null ? this.countriesRepository.GetByName(viewModel.Country) : null,
                SportClub = viewModel.SportClub != null ? this.sportClubsRepository.GetByName(viewModel.SportClub) : null
            };
        }
    }
}
