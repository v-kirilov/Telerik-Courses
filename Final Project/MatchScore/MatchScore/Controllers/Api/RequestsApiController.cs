using Mailjet.ConsoleApplication;
using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MatchScore.Controllers.Api
{
    [Route("api/requests")]
    [ApiController]
    public class RequestsApiController : ControllerBase
    {
        private readonly IRequestsService requestsService;
        private readonly IAuthManager authManager;
        private readonly IModelMapper modelMapper;

        public RequestsApiController(IRequestsService requestsService, IAuthManager authManager, IModelMapper modelMapper)
        {
            this.requestsService = requestsService;
            this.authManager = authManager;
            this.modelMapper = modelMapper;
        }


        [HttpGet("")]
        public IActionResult GetRequests([FromHeader] string credentials)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(credentials);

                var requests = this.requestsService.GetAll(authUser);

                return this.StatusCode(StatusCodes.Status200OK, requests);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (InvalidOperationException e)
            {
                return this.StatusCode(StatusCodes.Status405MethodNotAllowed, e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetRequestById(int id, [FromHeader] string credentials)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(credentials);

                var request = this.requestsService.GetById(id, authUser);

                return this.StatusCode(StatusCodes.Status200OK, request);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (InvalidOperationException e)
            {
                return this.StatusCode(StatusCodes.Status405MethodNotAllowed, e.Message);
            }
        }
        

        [HttpPost("")]
        public IActionResult CreateRequest([FromHeader] string credentials, [FromBody] RequestDtoName dto)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(credentials);
                var request = this.modelMapper.MapDtoToRequest(dto);

                var createdRequest = this.requestsService.Create(request, authUser);

                return this.StatusCode(StatusCodes.Status200OK, createdRequest);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (InvalidOperationException e)
            {
                return this.StatusCode(StatusCodes.Status405MethodNotAllowed, e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRequest(int id, [FromHeader] string credentials, [FromBody] RequestDtoName dto)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(credentials);
                var request = this.modelMapper.MapDtoToRequest(id, dto);

                this.requestsService.UpdateAsync(id, request, authUser);

                return this.StatusCode(StatusCodes.Status200OK);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (InvalidOperationException e)
            {
                return this.StatusCode(StatusCodes.Status405MethodNotAllowed, e.Message);
            }
        }
    }
}
