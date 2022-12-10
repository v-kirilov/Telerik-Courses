using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.QueryParameters;
using MatchScore.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Controllers.Api
{
    [Route("api/users")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly IAuthManager authManager;
        private readonly IModelMapper modelMapper;

        public UsersApiController(IUsersService usersService, IAuthManager authManager, IModelMapper modelMapper)
        {
            this.usersService = usersService;
            this.authManager = authManager;
            this.modelMapper = modelMapper;
        }

        #region Users
        [HttpGet("")]
        public IActionResult GetUsers([FromQuery] UserQueryParameters filterParameters)
        {
            try
            {
                List<UserDto> result = this.usersService.FilterBy(filterParameters);

                return this.StatusCode(StatusCodes.Status200OK, result);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                UserDto user = this.usersService.GetById(id);

                return this.StatusCode(StatusCodes.Status200OK, user);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult CreateUser([FromBody] CreateUserDto dto)
        {
            try
            {
                var user = this.modelMapper.MapCreateDtoToUser(dto);
                UserDto createdUser = this.usersService.Create(user);

                return this.StatusCode(StatusCodes.Status201Created, createdUser);
            }
            catch (DuplicateEntityException e)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromHeader] string credentials, [FromBody] UpdateUserDto dto)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(credentials);
                var user = this.modelMapper.MapUpdateDtoToUser(dto);

                UserDto updatedUser = this.usersService.Update(id, user, authUser);

                return this.StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (DuplicateEntityException e)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id, [FromHeader] string credentials)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(credentials);
                this.usersService.Delete(id, authUser);

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
        }
        #endregion
    }
}

