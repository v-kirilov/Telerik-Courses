using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.QueryParameters;
using MatchScore.Models.ViewModels;
using MatchScore.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IAuthManager authManager;
        private readonly IModelMapper modelMapper;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IPlayersService playersService;
        private readonly ICountriesService countriesService;
        private readonly ISportClubsService sportClubsService;
        private readonly IPhotoService photoService;

        public PlayersController(IUsersService usersService, IAuthManager authManager, IModelMapper modelMapper, IWebHostEnvironment hostEnvironment, IPlayersService playersService, ICountriesService countriesService, ISportClubsService sportClubsService, IPhotoService photoService)
        {
            this.usersService = usersService;
            this.authManager = authManager;
            this.modelMapper = modelMapper;
            this.hostEnvironment = hostEnvironment;
            this.playersService = playersService;
            this.countriesService = countriesService;
            this.sportClubsService = sportClubsService;
            this.photoService = photoService;
        }

        [HttpGet]
        public IActionResult Index(string filter, string search, string sortFilter, PlayerQueryParameters queryParameters)
        {
            switch (filter)
            {
                case "FullName":
                    queryParameters.FullName = search;
                    break;
                case "Country":
                    queryParameters.Country = search;
                    break;
                case "SportClub":
                    queryParameters.SportClub= search;
                    break;
                default:
                    break;
            }

            switch (sortFilter)
            {
                case "Username":
                    queryParameters.SortBy = "FullName";
                    break;
                case "Email":
                    queryParameters.SortBy = "Country";
                    break;
                case "FirstName":
                    queryParameters.SortBy = "SportClub";
                    break;
                default:
                    break;
            }


            var players = this.playersService.FilterBy(queryParameters);
            return View(players);
        }

        public IActionResult PlayersRanking(string filter, string search, string sortFilter, PlayerQueryParameters queryParameters)
        {
            //switch (filter)
            //{
            //    case "FullName":
            //        queryParameters.FullName = search;
            //        break;
            //    case "Country":
            //        queryParameters.Country = search;
            //        break;
            //    case "SportClub":
            //        queryParameters.SportClub = search;
            //        break;
            //    default:
            //        break;
            //}

            //switch (sortFilter)
            //{
            //    case "Username":
            //        queryParameters.SortBy = "FullName";
            //        break;
            //    case "Email":
            //        queryParameters.SortBy = "Country";
            //        break;
            //    case "FirstName":
            //        queryParameters.SortBy = "SportClub";
            //        break;
            //    default:
            //        break;
            //}


            var players = this.playersService.FilterBy(queryParameters);
            return View(players);
        }

        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            try
            {
                var player = this.playersService.GetById(id);

                return this.View(player);
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

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
                //this.tournamentsService.IsDirectorOrAdmin(user);
                CreatePlayerViewModel playerView = new CreatePlayerViewModel();
                return this.View(playerView);
            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatePlayerViewModel viewModel)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            if (this.playersService.FullNameExists(viewModel.FullName))
            {
                this.ModelState.AddModelError("FullName", "This player already exists.");

                return this.View(viewModel);
            }

            if (viewModel.Country == "Choose" && viewModel.NewCountry == null)
            {
                this.ModelState.AddModelError("Country", "You need to select country.");

                return this.View(viewModel);
            }

            if (viewModel.SportClub == "Choose" && viewModel.NewSportClub == null)
            {
                this.ModelState.AddModelError("SportClub", "You need to select sport club.");

                return this.View(viewModel);
            }

            if (viewModel.NewCountry != null)
            {
                if (this.countriesService.CountryExists(viewModel.NewCountry))
                {
                    this.ModelState.AddModelError("NewCountry", "This country already exists.");

                    return this.View(viewModel);
                }
                else
                {
                    var newCountry = this.countriesService.Create(new Country { Name = viewModel.NewCountry });
                    viewModel.Country = newCountry.Name;
                }
            }

            if (viewModel.NewSportClub != null)
            {
                if (this.sportClubsService.SportClubExists(viewModel.NewSportClub))
                {
                    this.ModelState.AddModelError("NewSportClub", "This sport club already exists.");

                    return this.View(viewModel);
                }
                else
                {
                    var newSportClub = this.sportClubsService.Create(new SportClub { Name = viewModel.NewSportClub });
                    viewModel.SportClub = newSportClub.Name;
                }    
            }

            if (viewModel.Country == "Choose...")
            {

            }

            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            try
            {
                
                User authUser = this.authManager.CurrentUser;
                var player = this.modelMapper.MapCreateViewToPlayer(viewModel);
                var createdPlayer = this.playersService.Create(player, authUser);

                if (viewModel.ProfileImage != null)
                {
                    await this.photoService.AddPhotoForPlayer(createdPlayer.Id, new PhotoForCreation { File = viewModel.ProfileImage });
                }               

                return this.RedirectToAction("Details", "Players", new { id = createdPlayer.Id });
            }
            catch (UnauthorizedOperationException e)
            {
                
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }
            try
            {
                User user = this.authManager.CurrentUser;
                var player = this.playersService.GetById(id);
                EditPlayerViewModel playerView = this.modelMapper.MapPlayerDtoToEditPlayerViewModel(player);
                return this.View(playerView);
            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync([FromRoute] int id, EditPlayerViewModel viewModel)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            var player = this.playersService.GetById(id);

            if (viewModel.FullName != player.FullName && this.playersService.FullNameExists(viewModel.FullName))
            {
                this.ModelState.AddModelError("FullName", "Player with the same name already exists.");

                return this.View(viewModel);
            }

            if (viewModel.NewCountry != null)
            {
                if (this.countriesService.CountryExists(viewModel.NewCountry))
                {
                    this.ModelState.AddModelError("NewCountry", "This country already exists.");

                    return this.View(viewModel);
                }
                else
                {
                    var newCountry = this.countriesService.Create(new Country { Name = viewModel.NewCountry });
                    viewModel.Country = newCountry.Name;
                }
            }

            if (viewModel.NewSportClub != null)
            {
                if (this.sportClubsService.SportClubExists(viewModel.NewSportClub))
                {
                    this.ModelState.AddModelError("NewSportClub", "This sport club already exists.");

                    return this.View(viewModel);
                }
                else
                {
                    var newSportClub = this.sportClubsService.Create(new SportClub { Name = viewModel.NewSportClub });
                    viewModel.SportClub = newSportClub.Name;
                }
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            try
            {

                User authUser = this.authManager.CurrentUser;
                var playerIncoming = this.modelMapper.MapEditPlayerViewModelToPlayer(viewModel);
                var updatedPlayer = this.playersService.Update(id, playerIncoming, authUser);

                if (viewModel.NewProfileImage != null)
                {
                    await this.photoService.AddPhotoForPlayer(id, new PhotoForCreation { File = viewModel.NewProfileImage });
                }

                return this.RedirectToAction("Details", "Players", new { id = updatedPlayer.Id });
            }
            catch (UnauthorizedOperationException e)
            {

                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed([FromRoute] int id)
        {
            try
            {
                var authUser = this.authManager.CurrentUser;
                var playerToDelete = this.playersService.GetById(id);
                this.playersService.Delete(id, authUser);

                return this.RedirectToAction("Index", "Players");

            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
            catch (UnauthorizedOperationException ex)
            {
                this.Response.StatusCode = StatusCodes.Status403Forbidden;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
        }
    }
}
