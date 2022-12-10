using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.QueryParameters;
using MatchScore.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Controllers.Api
{
    [Route("api/players")]
    [ApiController]
    public class PlayersApiController : ControllerBase
    {
        private readonly IPlayersService playersService;
        private readonly IAuthManager authManager;
        private readonly IModelMapper modelMapper;

        public PlayersApiController(IPlayersService playersService, IAuthManager authManager, IModelMapper modelMapper)
        {
            this.playersService = playersService;
            this.authManager = authManager;
            this.modelMapper = modelMapper;
        }


        [HttpGet("")]
        public IActionResult GetPlayers([FromQuery] PlayerQueryParameters filterParameters)
        {
            try
            {
                List<PlayerDto> result = this.playersService.FilterBy(filterParameters);

                return this.StatusCode(StatusCodes.Status200OK, result);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayerById(int id)
        {
            try
            {
                PlayerDto player = this.playersService.GetById(id);

                return this.StatusCode(StatusCodes.Status200OK, player);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult CreatePlayer([FromHeader] string credentials, [FromBody] PlayerDto dto)
        {
            try
            {
                var authUser = this.authManager.TryGetUser(credentials);

                var player = this.modelMapper.MapDtoToPlayer(dto);
                var createdPlayer = this.playersService.Create(player, authUser);

                return this.StatusCode(StatusCodes.Status201Created, createdPlayer);
            }
            catch (DuplicateEntityException e)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, [FromHeader] string credentials, [FromBody] PlayerDto dto)
        {
            try
            {
                var authUser = this.authManager.TryGetUser(credentials);
                var player = this.modelMapper.MapDtoToPlayer(dto);

                var updatedPlayer = this.playersService.Update(id, player, authUser);

                return this.StatusCode(StatusCodes.Status200OK, updatedPlayer);
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
                this.playersService.Delete(id, authUser);

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
    }
}
