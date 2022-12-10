using MatchScore.Exceptions;
using MatchScore.Helpers;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.Enums;
using MatchScore.Models.QueryParameters;
using MatchScore.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IMatchesService matchesService;
        private readonly IAuthManager authManager;
        private readonly IModelMapper modelMapper;

        public MatchesController(IMatchesService matchesService, IAuthManager authManager, IModelMapper modelMapper)
        {
            this.matchesService = matchesService;
            this.authManager = authManager;
            this.modelMapper = modelMapper;
        }

        [HttpGet]
        public IActionResult Index(MatchQueryParameters filterPar, string filter, string search, string sort)
        {
            try
            {
                if (filter == "Participant")
                {
                    filterPar.Participant = search;
                }
                else if (filter == "Format")
                {
                    filterPar.Format = search;
                }
                else if (filter == "Tournament")
                {
                    filterPar.Tournament = search;
                }
                else if (filter == "Director")
                {
                    filterPar.DirectorEmail = search;
                }
                if (!string.IsNullOrEmpty(sort))
                { 
                    filterPar.SortBy = sort;
                    
                }

                PaginatedList<MatchDto> matches = this.matchesService.FilterBy(filterPar);

                if (filterPar.Status == "Future")
                {

                }
                this.ViewData["Title"] = "Matches";

                return View("_MatchForm", matches);
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");

            }
            
        }

        [HttpGet]
        public IActionResult Current(MatchQueryParameters filters)
        {
            filters.Status = "Current";
            return RedirectToAction("Index", "Matches", filters);
        }

        [HttpGet]
        public IActionResult Future(MatchQueryParameters filters)
        {
            filters.Status = "Future";
            return RedirectToAction("Index", "Matches", filters);
        }

        [HttpGet]
        public IActionResult Past(MatchQueryParameters filters)
        {
            filters.Status = "Past";
            return RedirectToAction("Index", "Matches", filters);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }
            var matchDto = new MatchDto();
            return this.View(matchDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MatchDto matchDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(matchDto);
            }
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            try
            {
                var user = this.authManager.CurrentUser;
                if (!(user.Role.Name.Equals("Admin") || user.Role.Name.Equals("Director")))
                {
                    throw new UnauthorizedOperationException("You are not authorized to organize match.");
                }
                else
                {
                    if (matchDto.Date == DateTime.Today)
                    {
                        matchDto.Status = "Current";
                    }
                    else if (matchDto.Date > DateTime.Today)
                    {
                        matchDto.Status = "Future";
                    }
                    var match = this.modelMapper.MapDtoToMatch(matchDto);
                    match.Director = user;
                    var createdMatch = await this.matchesService.Create(match);
                    
                    return this.RedirectToAction("Details", "Matches", new { id = createdMatch.Id });
                }
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");

            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
        }

        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            try
            {
                MatchDto matchDto = this.matchesService.GetMatchById(id);
                return this.View(matchDto);

            }
            catch (EntityNotFoundException x)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = x.Message;
                return this.View("Error");

            }
        }

        [HttpGet]
        public IActionResult Update([FromRoute]int id)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }
            try
            {
                var user = this.authManager.CurrentUser;
                MatchDto matchDto = this.matchesService.GetMatchById(id);

                if (user.Role.Name.Equals("Admin") || (user.Role.Name.Equals("Director") && (user.Id == matchDto.DirectorId)))
                {
                    
                    return this.View(matchDto);
                }
                else
                {
                    
                    throw new UnauthorizedOperationException("You are not authorized to organize match.");
                }
                    
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");

            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update([FromRoute]int id, MatchDto matchDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(matchDto);
            }
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            try
            {
                var user = this.authManager.CurrentUser;

                
                if (matchDto.Date == DateTime.Today)
                {
                    matchDto.Status = "Current";
                }
                else if (matchDto.Date > DateTime.Today)
                {
                    matchDto.Status = "Future";
                }

                matchDto.DirectorId = user.Id;
                var updatedMatch = await this.matchesService.UpdateMatch(id, matchDto);
                return this.RedirectToAction("Details", "Matches", new { id = updatedMatch.Id });
                
                
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");

            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed([FromRoute] int id)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Users");
            }

            try
            {
                var user = this.authManager.CurrentUser;
                if (user.Role.Name.Equals("Admin"))
                {
                    this.matchesService.Delete(id);
                    return this.RedirectToAction("Index", "Matches");
                }
                else
                {
                    throw new UnauthorizedOperationException("You are not authorized to organize match.");
                }
                
            }
            catch (EntityNotFoundException e)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = e.Message;

                return this.View("Error");
            }
            catch (UnauthorizedOperationException e)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = e.Message;
                return this.View("Error");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Finish(Dictionary<string, string> dictNames, [FromRoute] int id)
        {
            int score1 = int.Parse(dictNames["score1"]);
            int score2 = int.Parse(dictNames["score2"]);

            MatchDto matchDto = this.matchesService.GetMatchById(id);

            matchDto.Score1 = score1;
            matchDto.Score2 = score2;

            matchDto.Status = "Past";

            var updatedMatch = await this.matchesService.UpdateMatch(id, matchDto);

            try
            {
                PlayerDto winner = this.matchesService.GetMatchWinner(updatedMatch.Id);
                this.ViewData["id"] = winner.Id;
            }
            catch (EntityNotFoundException e)
            {
                this.ViewData["result"] = e.Message;
            }
            return this.View(updatedMatch);
        }

    }
}
