using ForumSystem.Exceptions;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Models.QueryParameters;
using ForumSystem.Models.Users.UsersViewModels;
using ForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.Controllers
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
        public IActionResult Index(string filter, string search, string sortFilter, UserQueryParameters queryParameters)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            switch (filter)
            {
                case "Username":
                    queryParameters.Username = search;
                    break;
                case "Email":
                    queryParameters.Email = search;
                    break;
                case "FirstName":
                    queryParameters.FirstName = search;
                    break;
                default:
                    break;
            }

            switch (sortFilter)
            {
                case "Username":
                    queryParameters.SortBy = "Username";
                    break;
                case "Email":
                    queryParameters.SortBy = "Email";
                    break;
                case "FirstName":
                    queryParameters.SortBy = "FirstName";
                    break;
                default:
                    break;
            }


            var users = this.usersService.FilterUserBy(queryParameters);
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
                var user = this.usersService.GetUserById(id);

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
                var user = this.usersService.GetUserById(id);
                this.ViewData["PictureName"] = user.ProfilePicture;

                var userView = new EditViewModel(user);

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
        public IActionResult Edit([FromRoute] int id, EditViewModel editView)
        {
            string uniqueFileName = UploadedFile(editView);

            if (!this.ModelState.IsValid)
            {
                return this.View(editView);
            }

            var userToUpdate = this.usersService.GetUserById(id);

            if (this.usersService.EmailExists(editView.Email) && userToUpdate.Email != editView.Email)
            {
                this.ModelState.AddModelError("Email", "User with same email already exists.");

                return this.View(editView);
            }

            if (editView.Password != editView.ConfirmPassword)
            {
                this.ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");

                return this.View(editView);
            }

            try
            {
                var authUser = this.authManager.CurrentUser;
                var updateUserDto = this.modelMapper.MapUserEditView(editView, uniqueFileName);
                var updatedUser = this.usersService.Update(id, updateUserDto, authUser);

                return this.RedirectToAction("Details", "Users", new { id = userToUpdate.Id });
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
            catch(UnauthorizedOperationException ex)
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
                var userToDelete = this.usersService.GetUserById(id);
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
            if(!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            try
            {
                this.authManager.Login(viewModel.Username, viewModel.Password);

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

            return this.RedirectToAction("Index", "Home");
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

            if (this.usersService.UsernameExists(regViewModel.Username))
            {
                this.ModelState.AddModelError("Username", "User with same username already exists.");

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

            var userToCreate = this.modelMapper.MapUserRegView(regViewModel);
            this.usersService.Create(userToCreate);
            return this.RedirectToAction("Login", "Users");
        }

        private string UploadedFile(EditViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}

