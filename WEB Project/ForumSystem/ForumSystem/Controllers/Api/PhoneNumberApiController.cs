using ForumSystem.Exceptions;
using ForumSystem.Helpers;
using ForumSystem.Helpers.Contracts;
using ForumSystem.Models;
using ForumSystem.Models.DTO;
using ForumSystem.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ForumSystem.Controllers.Api
{
    [ApiController]
    [Route("api/users")]
    public class PhoneNumberApiController : Controller
    {
        private readonly IPhoneNumbersService phoneNumberService;
        private readonly IModelMapper modelmapper;
        private readonly IAuthManager authManager;

        public PhoneNumberApiController(IPhoneNumbersService phoneNumberService, IModelMapper modelmapper, IAuthManager authManager)
        {
            this.phoneNumberService = phoneNumberService;
            this.modelmapper = modelmapper;
            this.authManager = authManager;
        }

        [HttpGet("phonenumbers")]
        public IActionResult GetAllNumbers()
        {
            List<PhoneNumberDto> result = this.phoneNumberService.GetAll();

            return this.StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("{userId}/phonenumbers")]
        public IActionResult GetByUserId(int userId)
        {
            try
            {
                PhoneNumberDto result = this.phoneNumberService.GetByUserId(userId);

                return this.StatusCode(StatusCodes.Status200OK, result);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpGet("{userId}/phonenumbers/{phoneNumberId}")]
        public IActionResult GetById(int userId, int phoneNumberId)
        {
            try
            {
                PhoneNumberDto phoneNumber = this.phoneNumberService.GetById(userId, phoneNumberId);

                return this.StatusCode(StatusCodes.Status200OK, phoneNumber);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }


        [HttpPost("{userId}/phonenumbers")]
        public IActionResult CreatePhoneNumber(int userId, [FromHeader] string username, [FromBody] PhoneNumberDto phoneNumberDto)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);
                PhoneNumber phoneNumber = this.modelmapper.ToModel(phoneNumberDto, userId);
                PhoneNumberDto createdPhoneNumber = this.phoneNumberService.Create(phoneNumber, authUser);

                return this.StatusCode(StatusCodes.Status201Created, createdPhoneNumber);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }


        }

        [HttpPut("{userId}/phonenumbers/{phoneNumberId}")]
        public IActionResult UpdatePhoneNumber(int userId, int phoneNumberId, [FromHeader] string username, [FromBody] PhoneNumberDto phoneNumberDto)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);

                PhoneNumber phoneNumber = this.modelmapper.ToModel(phoneNumberDto, userId);
                PhoneNumberDto updatedPhoneNumber = this.phoneNumberService.Update(userId, phoneNumberId, phoneNumber, authUser);

                return this.StatusCode(StatusCodes.Status200OK, updatedPhoneNumber);
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

        [HttpDelete("{userId}/phonenumbers/{phoneNumberId}")]
        public IActionResult DeletePhoneNumber(int userId, int phoneNumberId, [FromHeader] string username)
        {
            try
            {
                User authUser = this.authManager.TryGetUser(username);

                this.phoneNumberService.Delete(userId, phoneNumberId, authUser);

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
