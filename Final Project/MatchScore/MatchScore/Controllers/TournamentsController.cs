using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.QueryParameters;
using MatchScore.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Controllers
{

    public class TournamentsController : Controller
    {
        private readonly IAuthManager authManager;
        private readonly IModelMapper modelMapper;

        private readonly ITournamentsService tournamentsService;
        private readonly IPhotoService photoService;
        private readonly IPlayersService playersService;
        private readonly IUsersService usersService;
        private readonly IMatchesService matchesService;
        public TournamentsController(IAuthManager authManager, IModelMapper modelMapper, IPhotoService photoService, IPlayersService playersService, IUsersService usersService, ITournamentsService tournamentsService, IMatchesService matchesService)
        {
            this.authManager = authManager;
            this.modelMapper = modelMapper;
            this.photoService = photoService;
            this.playersService = playersService;
            this.usersService = usersService;
            this.tournamentsService = tournamentsService;
            this.matchesService = matchesService;
        }

        [HttpGet]
        public IActionResult Index(TournamentQueryParameters filterPar, string filter, string search, string sort)
        {
            try
            {
                if (filter == "Title")
                {
                    filterPar.Title = search;
                }
                else if (filter == "Email")
                {
                    filterPar.ManagerEmail = search;
                }
                else if (filter=="Player")
                {
                    filterPar.FullName = search;
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    if (sort == "Honorary" || sort == "Financial")
                    {
                        filterPar.Prize = sort;
                    }
                    else
                    {
                        filterPar.Format = sort;
                    }
                }

                PaginatedList<TournamentDto> tournaments = this.tournamentsService.FilterBy(filterPar);
                return this.View(tournaments);
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
        }

        [HttpGet]
        public IActionResult Future(TournamentQueryParameters filterPar)
        {
            filterPar.Status = "Future";
            return RedirectToAction("Index", "Tournaments", filterPar);
        }
        [HttpGet]
        public IActionResult Past(TournamentQueryParameters filterPar)
        {
            filterPar.Status = "Past";
            return RedirectToAction("Index", "Tournaments", filterPar);
        }
        [HttpGet]
        public IActionResult Current(TournamentQueryParameters filterPar)
        {
            filterPar.Status = "Current";
            return RedirectToAction("Index", "Tournaments", filterPar);
        }


        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            try
            {
                var tournamentDto = this.tournamentsService.GetById(id);
                return this.View(tournamentDto);
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }
            try
            {
                User user = this.authManager.CurrentUser;
                TournamentDto tournamentDto = new TournamentDto();
                return this.View(tournamentDto);
            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
        }

        [HttpPost]
        public async  Task<IActionResult> Create(TournamentDto tournamentDto)
        {
            User user = this.authManager.CurrentUser;
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }
            if (!this.ModelState.IsValid)
            {
                return this.View(tournamentDto);
            }
            try
            {
                this.tournamentsService.IsDirectorOrAdmin(user);

            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
            var tournament = this.modelMapper.MapTournamentCreate(tournamentDto, user.Id);

            var createdTournament = await this.tournamentsService.CreateTournament(tournament);
            return this.RedirectToAction("Details", "Tournaments", new { id = createdTournament.Id });
        }

        [HttpGet]
        public IActionResult Update([FromRoute] int id)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }
            try
            {
                User user = this.authManager.CurrentUser;
                TournamentDto tournament = this.tournamentsService.GetById(id);
                return this.View(tournament);
            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
        }

        [HttpPost]
        public IActionResult Update([FromRoute] int id, TournamentDto tournamentDto)
        {
            User user = this.authManager.CurrentUser;
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }
            if (!this.ModelState.IsValid)
            {
                return this.View(tournamentDto);
            }
            try
            {
                this.tournamentsService.IsDirectorOrAdmin(user);
            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
            var updatedTournament = this.tournamentsService.UpdateTournament(id, tournamentDto,user.Id);
            return this.RedirectToAction("Details", "Tournaments", new { id = updatedTournament.Id });

        }


        [HttpGet]
        public async Task<IActionResult> AddPlayer(Dictionary<string, string> dict)
        {
            int playerId = int.Parse(dict["playerId"]);
            int tournamentId = int.Parse(dict["tournamentId"]);
            try
            {
              var addedPlayerId = await this.tournamentsService.AddPlayerToTournament(playerId, tournamentId);
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
            return this.RedirectToAction("Details", "Tournaments", new { id = tournamentId });
        }

        [HttpGet]
        public async Task<IActionResult> RemovePlayer(Dictionary<string, string> dict)
        {
            int playerId = int.Parse(dict["playerId"]);
            int tournamentId = int.Parse(dict["tournamentId"]);
            try
            {
               var removedPlayerId = await this.tournamentsService.RemovePlayerFromTournament(playerId, tournamentId);
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
            return this.RedirectToAction("Details", "Tournaments", new { id = tournamentId });
        }
        [HttpGet]
        public async Task<IActionResult> AddUnregisteredPlayer(Dictionary<string, string> dictNames, [FromRoute] int id)
        {
            if (string.IsNullOrEmpty(dictNames["firstName"]) || string.IsNullOrEmpty(dictNames["lastName"]))
            {
                this.Response.StatusCode = StatusCodes.Status403Forbidden;
                this.ViewData["ErrorMessage"] = "Please enter First and Last name!";
                return this.View("Error");
            }

            string newPlayer = dictNames["firstName"] + " " + dictNames["lastName"];

            var tournamentDto = this.tournamentsService.GetById(id);
            if (tournamentDto.PlayersFullName.Contains(newPlayer))
            {
                //Check THIS!

                this.Response.StatusCode = StatusCodes.Status403Forbidden;
                this.ViewData["ErrorMessage"] = "Player is already listed!";
                return this.View("Error");
            }
            try
            {
                PlayerDto playerDtoToAdd = new PlayerDto();
                playerDtoToAdd.FullName = newPlayer;

                Player playerToAdd = this.modelMapper.MapDtoToPlayer(playerDtoToAdd);
                PlayerDto createdPlayer = this.playersService.Create(playerToAdd, this.authManager.CurrentUser);
                var playerToAddId = await this.tournamentsService.AddPlayerToTournament(playerToAdd, id);
            }
            catch (DuplicateEntityException)
            {
                this.Response.StatusCode = StatusCodes.Status403Forbidden;
                this.ViewData["ErrorMessage"] = "Player already exists!";

                return this.View("Error");
            }

            return this.RedirectToAction("Details", "Tournaments", new { id = id });
            throw new EntityNotFoundException();
        }

        public IActionResult StartTournament([FromRoute] int id)
        {
            User user = this.authManager.CurrentUser;

            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }
            var tDto = this.tournamentsService.GetById(id);
            if (tDto.PlayersFullName.Count == 0)
            {
                this.Response.StatusCode = StatusCodes.Status403Forbidden;
                this.ViewData["ErrorMessage"] = "There are no players in the current tournament, please enter more players.";
                return this.View("Error");
            }
            if (tDto.PlayersFullName.Count % 4 != 0 && tDto.Format == Models.Enums.TournamentFormat.Knockout)
            {
                //Check THIS!
                this.Response.StatusCode = StatusCodes.Status403Forbidden;
                this.ViewData["ErrorMessage"] = "Players count must be divisible by 4, please enter more players.";
                return this.View("Error");
            }
            else if (tDto.PlayersFullName.Count % 2 != 0)
            {
                this.Response.StatusCode = StatusCodes.Status403Forbidden;
                this.ViewData["ErrorMessage"] = "Players count is odd , please enter more players.";
                return this.View("Error");
            }
            if (tDto.Format == Models.Enums.TournamentFormat.League)
            {
                this.tournamentsService.CreateStanding(tDto.PlayersFullName, tDto.Id);
            }
            //Create first round and matches to fill first round!
            var tournamentDto = this.tournamentsService.CreateRound(id);
            return this.RedirectToAction("Round", "Tournaments", new { id = id });
        }

        [HttpGet]
        public IActionResult NextRound([FromRoute] int id)
        {

            var tournamentDto = this.tournamentsService.CreateRound(id);
            if (tournamentDto.Winner != null)
            {
                return this.RedirectToAction("FinishKnockout", "Tournaments", new { id = id });
            }
            return this.RedirectToAction("Round", "Tournaments", new { id = id });
        }


        [HttpGet]
        public IActionResult Round([FromRoute] int id)
        {
            try
            {
                var tournamentDto = this.tournamentsService.GetById(id);
                return this.View(tournamentDto);
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            this.tournamentsService.DeleteTournament(id);
            return this.RedirectToAction("Index", "Tournaments");
        }
        [HttpGet]
        public IActionResult Results([FromRoute] int id, Dictionary<string, string> matchesScores)
        {
            return this.RedirectToAction("Index", "Tournaments");
        }

        [HttpGet]
        public async Task<IActionResult> FinishMatch([FromRoute] int id, Dictionary<string, string> dictScores)
        {
            int score1 = int.Parse(dictScores["score1"]);
            int score2 = int.Parse(dictScores["score2"]);

            int tourId = 0;

            TournamentDto tournamentDto = this.tournamentsService.GetTournamentIdByMatchId(id);
            if (tournamentDto.Format == Models.Enums.TournamentFormat.Knockout)
            {
                if (score1 == score2)
                {
                    this.Response.StatusCode = StatusCodes.Status403Forbidden;
                    this.ViewData["ErrorMessage"] = "Score cannot be equal!";
                    return this.View("Error");
                }
            }

            MatchDto matchDto = this.matchesService.GetMatchById(id);

            matchDto.Score1 = score1;
            matchDto.Score2 = score2;

            matchDto.Status = "Past";
            MatchDto updatedMatch = await this.matchesService.UpdateMatch(id, matchDto);


            if (tournamentDto.Format == Models.Enums.TournamentFormat.League)
            {
                string player1 = matchDto.Players[0];
                string player2 = matchDto.Players[1];
                List<string> results = new List<string> { player1, player2, dictScores["score1"], dictScores["score2"] };
                if (score1 == score2)
                {
                    results.Add("1");
                    results.Add("1");
                }
                else if (score1 > score2)
                {
                    results.Add("3");
                    results.Add("0");
                }
                else
                {
                    results.Add("0");
                    results.Add("3");
                }
                this.tournamentsService.UpdateTournamentStandings(tournamentDto.Id, results);
                return this.RedirectToAction("Round", "Tournaments", new { id = tournamentDto.Id });

            }
            tourId = tournamentDto.Id;
            return this.RedirectToAction("Round", "Tournaments", new { id = tourId });

        }

        [HttpGet]
        public IActionResult FinishLeague([FromRoute] int id)
        {
            TournamentDto tournament = this.tournamentsService.GetById(id);
            if (tournament.Status != Models.Enums.Status.Past)
            {
                tournament.Status = Models.Enums.Status.Past;
                tournament.EndDate = System.DateTime.Now;
                this.tournamentsService.UpdateFinishedTournament(tournament);
            }
            //UPDATE TOURNAMENT NOW!
            return this.View(tournament);
        }
        [HttpGet]
        public IActionResult FinishKnockout([FromRoute] int id)
        {
            TournamentDto tournament = this.tournamentsService.GetById(id);

            return this.View(tournament);
        }
    }
}
