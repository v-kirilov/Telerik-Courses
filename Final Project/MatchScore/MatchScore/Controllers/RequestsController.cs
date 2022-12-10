using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.QueryParameters;
using MatchScore.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MatchScore.Controllers
{
    public class RequestsController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IAuthManager authManager;
        private readonly IModelMapper modelMapper;
        private readonly IRequestsService requestsService;
        public RequestsController(IUsersService usersService, IAuthManager authManager, IModelMapper modelMapper, IRequestsService requestsService)
        {
            this.usersService = usersService;
            this.authManager = authManager;
            this.modelMapper = modelMapper;
            this.requestsService = requestsService;
        }

        public IActionResult Index(string search, RequestQueryParameters queryParameters)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            queryParameters.UserEmail = search;

            try
            {
                var requests = this.requestsService.FilterBy(queryParameters, this.authManager.CurrentUser);
                return View(requests);
            }
            catch (UnauthorizedOperationException ex)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }  
        }

        [HttpGet]
        public IActionResult CreateLinkRequest(string type, string fullName)
        {
            try
            {
                var authUser = this.authManager.CurrentUser;

                var requestIncomingDto = new RequestDtoName() {Type = type, PlayerFullName = fullName };
                var requestIncoming = this.modelMapper.MapDtoToRequest(requestIncomingDto);

                var createdRequest = this.requestsService.Create(requestIncoming, authUser);

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
            catch (InvalidOperationException ex)
            {
                this.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
        }

        [HttpGet]
        public IActionResult CreateDirectorRequest(string type)
        {
            try
            {
                var authUser = this.authManager.CurrentUser;

                var requestIncomingDto = new RequestDtoName() { Type = type};
                var requestIncoming = this.modelMapper.MapDtoToRequest(requestIncomingDto);

                var createdRequest = this.requestsService.Create(requestIncoming, authUser);

                return this.RedirectToAction("Details", "Users", new {id = this.authManager.CurrentUser.Id});
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

        [HttpGet]
        public IActionResult Update([FromRoute] int id, string status)
        { 
            try
            {
                var authUser = this.authManager.CurrentUser;

                var requestIncomingDto = new RequestDtoName() { Id = id, Status = status };
                var requestIncoming = this.modelMapper.MapDtoToRequest(id, requestIncomingDto);

                var updatedRequest = this.requestsService.UpdateAsync(id, requestIncoming, this.authManager.CurrentUser);

                return this.RedirectToAction("Index", "Requests");
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
