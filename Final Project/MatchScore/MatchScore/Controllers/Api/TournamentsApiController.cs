using MatchScore.Exceptions;
using MatchScore.Helpers;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.QueryParameters;
using MatchScore.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchScore.Controllers.Api
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentsApiController : ControllerBase
    {
        private readonly ITournamentsService tournamentService;
        private readonly IModelMapper modelMapper;
        private readonly IAuthManager authManager;

        public TournamentsApiController(ITournamentsService tournamentService, IModelMapper modelMapper, IAuthManager authManager)
        {
            this.tournamentService = tournamentService;
            this.modelMapper = modelMapper;
            this.authManager = authManager;
        }
        [HttpGet("")]
        public IActionResult GetTournaments([FromQuery] TournamentQueryParameters filterPar)
        {
            try
            {
                List<TournamentDto> result = tournamentService.FilterByForApi(filterPar);
                return StatusCode(StatusCodes.Status200OK, result);

            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTournamentById(int id)
        {
            try
            {
                TournamentDto tournament = tournamentService.GetById(id);
                return StatusCode(StatusCodes.Status200OK, tournament);
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, "There is no Tournament with that Id!");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateTournament([FromBody] TournamentDto tournamentDto, [FromHeader] string credentials)
        {
            try
            {
                User director = authManager.TryGetUser(credentials);
                Tournament tournament = modelMapper.MapTournamentCreate(tournamentDto, director.Id);
                TournamentDto createdTournament = await tournamentService.CreateTournament(tournament);

                return StatusCode(StatusCodes.Status200OK, createdTournament);
            }
            catch (DuplicateEntityException e)
            {
                return StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
            catch (EntityNotFoundException)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, "UserId required");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTournament(int id, [FromBody] TournamentDto tourDto, [FromHeader] string credentials)
        {
            try
            {
                User director = authManager.TryGetUser(credentials);
                tourDto.Id = id;
                bool isDirector = tournamentService.IsDirectorOrAdminView(director, id);

                TournamentDto updateTournament = tournamentService.UpdateTournament(id, tourDto,director.Id);

                return StatusCode(StatusCodes.Status200OK, updateTournament);
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (DuplicateEntityException e)
            {
                return StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTournament([FromHeader] string credentials, int id)
        {
            try
            {
                User director = authManager.TryGetUser(credentials);
                bool isDirector = tournamentService.IsDirector(director, id);
                tournamentService.DeleteTournament(id);
                return StatusCode(StatusCodes.Status200OK, "Tournament deleted!");
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
        }

        [HttpGet("{tournamentId}/rounds")]
        public IActionResult GetRounds(int tournamentId)
        {
            try
            {
                List<RoundDto> rounds = this.tournamentService.GetRounds(tournamentId);
                return StatusCode(StatusCodes.Status200OK, rounds);

            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("{tournamentId}/rounds/{roundNumber}")]
        public IActionResult GetRounds(int tournamentId, int roundNumber)
        {
            try
            {
                RoundDto round = this.tournamentService.GetRoundByRoundNumber(tournamentId, roundNumber);
                return StatusCode(StatusCodes.Status200OK, round);

            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        //[HttpPost("{tournamentId}/rounds/")]
        //public IActionResult CreateRound(int tournamentId, [FromBody] RoundDto roundDto, [FromHeader] string credentials)
        //{
        //    try
        //    {
        //        User director = authManager.TryGetUser(credentials);
        //        bool isDirector = tournamentService.IsDirector(director, tournamentId);

        //        Round round = modelMapper.MapDtoToRound(roundDto);
        //        RoundDto createdRound = tournamentService.CreateRound(round);

        //        return StatusCode(StatusCodes.Status200OK, createdRound);
        //    }
        //    catch (EntityNotFoundException)
        //    {
        //        return StatusCode(StatusCodes.Status406NotAcceptable, "UserId required");
        //    }
        //    catch (UnauthorizedOperationException e)
        //    {
        //        return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
        //    }
        //}


        //[HttpPut("{tournamentId}/rounds/{roundNumber}")]
        //public IActionResult UpdateTournamentRound(int tournamentId, [FromBody] RoundDto roundDto, [FromHeader] string credentials, int roundNumber)
        //{
        //    try
        //    {
        //        User director = authManager.TryGetUser(credentials);
        //        bool isDirector = tournamentService.IsDirector(director, tournamentId);
        //        Round round = modelMapper.MapDtoToRound(roundDto);
        //        RoundDto updatedRound = tournamentService.UpdateTournamentRound(round, roundNumber, tournamentId);

        //        return StatusCode(StatusCodes.Status200OK, updatedRound);
        //    }
        //    catch (EntityNotFoundException e)
        //    {
        //        return StatusCode(StatusCodes.Status404NotFound, e.Message);
        //    }
        //    catch (UnauthorizedOperationException e)
        //    {
        //        return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
        //    }

        //}

        //[HttpDelete("{tournamentId}/rounds/{roundNumber}")]
        //public IActionResult DeleteRound([FromHeader] string credentials, int tournamentId, int roundNumber)
        //{

        //    try
        //    {
        //        User director = authManager.TryGetUser(credentials);
        //        bool isDirector = tournamentService.IsDirector(director, tournamentId);

        //        RoundDto round = tournamentService.GetRoundByRoundNumber(tournamentId, roundNumber);
        //        return StatusCode(StatusCodes.Status200OK, round);
        //    }
        //    catch (EntityNotFoundException e)
        //    {
        //        return StatusCode(StatusCodes.Status404NotFound, e.Message);
        //    }
        //    catch (UnauthorizedOperationException e)
        //    {
        //        return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
        //    }
        //}

        [HttpGet("{tournamentId}/players")]
        public IActionResult GetPlayersOfTournament(int tournamentId)
        {
            try
            {
                List<PlayerDto> players = this.tournamentService.GetPlayersOfTournament(tournamentId);
                return StatusCode(StatusCodes.Status200OK, players);

            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("{tournamentId}/players/{playerId}")]
        public IActionResult GetPlayer(int tournamentId, int playerId)
        {
            try
            {
                PlayerDto player = this.tournamentService.GetSinglePLayer(tournamentId, playerId);
                return StatusCode(StatusCodes.Status200OK, player);

            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        //[HttpDelete("{tournamentId}/players/{playerId}")]
        //public IActionResult RemovePlayerFromTournament([FromHeader] string credentials, int tournamentId, int playerId)
        //{
        //    try
        //    {
        //        User director = authManager.TryGetUser(credentials);
        //        bool isDirector = tournamentService.IsDirector(director, tournamentId);
        //        this.tournamentService.RemovePlayerFromTournament(playerId, tournamentId);
        //        return StatusCode(StatusCodes.Status200OK, "Player removed from tournament!");

        //    }
        //    catch (UnauthorizedOperationException e)
        //    {
        //        return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
        //    }
        //    catch (EntityNotFoundException e)
        //    {
        //        return StatusCode(StatusCodes.Status404NotFound, e.Message);
        //    }
        //}
        //[HttpPost("{tournamentId}/players/{playerId}")]
        //public IActionResult AddPlayerToTournament([FromHeader] string credentials, int tournamentId, int playerId)
        //{
        //    try
        //    {
        //        User director = authManager.TryGetUser(credentials);
        //        bool isDirector = tournamentService.IsDirector(director, tournamentId);
        //        //this.tournamentService.AddPlayerToTournament(playerId, tournamentId);
        //        //return StatusCode(StatusCodes.Status200OK, "Player added to tournament!");

        //    }
        //    catch (UnauthorizedOperationException e)
        //    {
        //        return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
        //    }
        //    catch (EntityNotFoundException e)
        //    {
        //        return StatusCode(StatusCodes.Status404NotFound, e.Message);
        //    }
        //}

    }
}
