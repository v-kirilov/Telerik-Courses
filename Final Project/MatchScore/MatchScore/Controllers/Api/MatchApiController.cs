using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.QueryParameters;
using MatchScore.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchScore.Controllers.Api
{
    [Route("api/matches")]
    [ApiController]
    public class MatchApiController : ControllerBase
    {
        private readonly IMatchesService matchesService;
        private readonly IAuthManager authManager;
        private readonly IModelMapper modelmapper;
        
        public MatchApiController(IMatchesService matchesService, IAuthManager authManager, IModelMapper modelmapper)
        {
            this.matchesService = matchesService;
            this.authManager = authManager;
            this.modelmapper = modelmapper;
        }

        [HttpGet("")]
        public IActionResult GetFilteredMatches([FromQuery] MatchQueryParameters filterParameters)
        {
            try
            {
                List<MatchDto> result = this.matchesService.FilterBy(filterParameters);

                return this.StatusCode(StatusCodes.Status200OK, result);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }

        }

        [HttpGet("{matchId}")]
        public IActionResult GetMatchById(int matchId)
        {
            try
            {
                MatchDto match = this.matchesService.GetMatchById(matchId);

                return this.StatusCode(StatusCodes.Status200OK, match);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("players/{participantId}")]
        public IActionResult GetMatchesByParticipant(int participantId)
        {
            try
            {
                List<MatchDto> matches = this.matchesService.GetMatchesByParticipant(participantId);

                return this.StatusCode(StatusCodes.Status200OK, matches);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("tournaments/{tournamentId}")]
        public IActionResult GetMatchesByTournament(int tournamentId)
        {
            try
            {
                List<MatchDto> matches = this.matchesService.GetMatchesByTournament(tournamentId);

                return this.StatusCode(StatusCodes.Status200OK, matches);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("tournaments/{tournamentId}/rounds/{roundId}")]
        public IActionResult GetMatchesByRound(int tournamentId, int roundId)
        {
            try
            {
                List<MatchDto> matches = this.matchesService.GetMatchesByRound(tournamentId, roundId);

                return this.StatusCode(StatusCodes.Status200OK, matches);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("directors/{directorId}")]
        public IActionResult GetMatchesByDirector(int directorId)
        {
            try
            {
                List<MatchDto> matches = this.matchesService.GetMatchesByDirector(directorId);

                return this.StatusCode(StatusCodes.Status200OK, matches);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateMatch([FromHeader] string credentials, [FromBody] MatchDto matchDto)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(credentials);
                if (!(authUser.Role.Name.Equals("Admin") || authUser.Role.Name.Equals("Director")))
                {
                    throw new UnauthorizedOperationException("You are not authorized to organize match.");
                }

                Match match = this.modelmapper.MapDtoToMatch(matchDto);
                MatchDto createdMatch = await this.matchesService.Create(match);

                return this.StatusCode(StatusCodes.Status201Created, createdMatch);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }

        }

        [HttpPut("{matchId}")]
        public async Task<IActionResult> UpdateMatch(int matchId, [FromHeader] string credentials, [FromBody] MatchDto matchDto)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(credentials);

                if (authUser.Role.Name.Equals("Admin") || (authUser.Role.Name.Equals("Director") && (authUser.Id == matchDto.DirectorId)))
                {
                    MatchDto updatedMatch = await this.matchesService.UpdateMatch(matchId, matchDto);

                    return this.StatusCode(StatusCodes.Status201Created, updatedMatch);
                }
                else
                {
                    throw new UnauthorizedOperationException("You are not authorized to organize match.");
                }
                
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }

        }

        [HttpDelete("{matchId}")]
        public IActionResult DeleteMatch(int matchId, [FromHeader] string credentials)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(credentials);
                if (!authUser.Role.Name.Equals("Admin"))
                {
                    throw new UnauthorizedOperationException("You are not authorized to organize match.");
                }
                this.matchesService.Delete(matchId);

                return this.StatusCode(StatusCodes.Status201Created);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }

        }
    }
}
