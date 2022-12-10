using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.ViewModels;
using MatchScore.QueryParameters;
using MatchScore.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IAuthManager authManager;
        private readonly IModelMapper modelMapper;
        private readonly IWebHostEnvironment hostEnvironment;

        public UsersController(IUsersService usersService, IAuthManager authManager, IModelMapper modelMapper, IWebHostEnvironment hostEnvironment)
        {
            this.usersService = usersService;
            this.authManager = authManager;
            this.modelMapper = modelMapper;
            this.hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public IActionResult Index(string filter, string search, string sort, UserQueryParameters queryParameters)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            switch (filter)
            {
                case "Email":
                    queryParameters.Email = search;
                    break;
                case "Role":
                    queryParameters.Role = search;
                    break;
                default:
                    break;
            }

            switch (sort)
            {
                case "Email":
                    queryParameters.SortBy = "Email";
                    break;
                case "Role":
                    queryParameters.SortBy = "Role";
                    break;
                default:
                    break;
            }


            var users = this.usersService.FilterBy(queryParameters);
            return View(users);
        }

        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            try
            {
                var user = this.usersService.GetById(id);

                return this.View(user);
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
                var user = this.usersService.GetById(id);
                var userView = this.modelMapper.MapUserDtoToEditView(user);

                return this.View(userView);
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, EditUserViewModel editView)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editView);
            }

            var userToUpdate = this.usersService.GetById(id);

            if (editView.Password != editView.ConfirmPassword)
            {
                this.ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");

                return this.View(editView);
            }

            try
            {
                var authUser = this.authManager.CurrentUser;
                var updateUserIncoming = this.modelMapper.MapEditViewToUser(editView);
                var updatedUser = this.usersService.Update(id, updateUserIncoming, authUser);

                return this.RedirectToAction("Details", "Users", new { id = userToUpdate.Id });
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

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed([FromRoute] int id)
        {
            try
            {
                var authUser = this.authManager.CurrentUser;
                var userToDelete = this.usersService.GetById(id);
                this.usersService.Delete(id, authUser);

                if (authUser.Id == userToDelete.Id)
                {
                    return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    return this.RedirectToAction("Index", "Users");
                }
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

        [HttpGet]
        public IActionResult Login()
        {
            var viewModel = new LoginViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            try
            {
                this.authManager.Login(viewModel.Email, viewModel.Password);

                return this.RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedOperationException ex)
            {
                this.ModelState.AddModelError("Password", ex.Message);

                return this.View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            this.authManager.Logout();

            return this.RedirectToAction("Login", "Users");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var regViewModel = new RegisterViewModel();

            return this.View(regViewModel);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel regViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(regViewModel);
            }

            if (this.usersService.EmailExists(regViewModel.Email))
            {
                this.ModelState.AddModelError("Email", "User with same email already exists.");

                return this.View(regViewModel);
            }

            if (regViewModel.Password != regViewModel.ConfirmPassword)
            {
                this.ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");

                return this.View(regViewModel);
            }

            var userToCreate = this.modelMapper.MapRegViewToUser(regViewModel);
            this.usersService.Create(userToCreate);
            return this.RedirectToAction("Login", "Users");
        }

        [HttpGet]
        public IActionResult RemoveAssociatedPlayer(int id)
        {
            try
            {
                var authUser = this.authManager.CurrentUser;
                var userIncomingDto = this.usersService.GetById(id);
                userIncomingDto.Role = "Default";
                var userIncoming = this.modelMapper.MapDtoToUser(userIncomingDto);

                var updatedUser = this.usersService.Update(id, userIncoming, authUser);

                return this.RedirectToAction("Details", "Users", new {id = updatedUser.Id});
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
            catch (InvalidOperationException ex)
            {
                this.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
        }
    }
}
