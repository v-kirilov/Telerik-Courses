using Mailjet.ConsoleApplication;
using MatchScore.Exceptions;
using MatchScore.Helpers.Contracts;
using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.Enums;
using MatchScore.Models.QueryParameters;
using MatchScore.Repositories.Contracts;
using MatchScore.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Services
{
    public class RequestsService : IRequestsService
    {
        private const string GetRequestsErrorMessage = "Only the admin can see the requests.";
        private const string ModifyRequestErrorMessage = "Only the admin can modify this request's status.";
        private const string AlreadyAssociatedPlayerErrorMessage = "Тhis player is already associated.";
        private const string DirectorToPlayerErrorMessage = "Directors can't be associated with player.";
        private const string VerifiedToDirectorErrorMessage = "Verified users(associated with player) can't be directors.";
        private const string AlreadyAssociatedUserErrorMessage = "You are already associated with a player.";
        private const string AlreadyDirectorErrorMessage = "You are already a director.";
        private const string NoRequestsErrorMessage = "There are no requests to show";
        private const string ManyRequestsErrorMessage = "It is allowed only one request per user";
        private const string DirectorPlayerErrorMessage = "You can't pass player name if your request is promote to director.";
        private const string RequestLinkProfileErrorMessage = "This request needs player full name";

        private readonly IRequestsRepository requestsRepository;
        private readonly IPlayersRepository playersRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IModelMapper modelMapper;
        private readonly IEmailSender emailSender;

        public RequestsService(IRequestsRepository requestsRepository, IPlayersRepository playersRepository, IUsersRepository usersRepository, IModelMapper modelMapper, IEmailSender emailSender)
        {
            this.requestsRepository = requestsRepository;
            this.playersRepository = playersRepository;
            this.usersRepository = usersRepository;
            this.modelMapper = modelMapper;
            this.emailSender = emailSender;
        }

        public List<RequestDto> GetAll(User authUser)
        {
            if (authUser.Role.Name != "Admin")
            {
                throw new UnauthorizedOperationException(GetRequestsErrorMessage);
            }

            var requests = this.requestsRepository.GetAll();

            if (requests.Count <= 0)
            {
                throw new EntityNotFoundException(NoRequestsErrorMessage);
            }

            var requestsToReturn = requests.Select(r => this.modelMapper.MapRequestToDto(r)).ToList();

            return requestsToReturn;
        }

        public PaginatedList<RequestDtoName> FilterBy(RequestQueryParameters filterParameters, User authUser)
        {
            if (authUser.Role.Name != "Admin")
            {
                throw new UnauthorizedOperationException(GetRequestsErrorMessage);
            }

            var requests = this.requestsRepository.FilterBy(filterParameters);
            var requestsList = requests.Select(r => this.modelMapper.MapRequestToDtoWithName(r)).ToList();
            var requestsToReturn = new PaginatedList<RequestDtoName>(requestsList, requests.TotalPages, requests.PageNumber);

            return requestsToReturn;
        }

        public RequestDto GetById(int id, User authUser)
        {
            if (authUser.Role.Name != "Admin")
            {
                throw new UnauthorizedOperationException(GetRequestsErrorMessage);
            }

            var request = this.requestsRepository.GetById(id);
            var requestToReturn = this.modelMapper.MapRequestToDto(request);

            return requestToReturn;
        }

        public RequestDto Create(Request request, User authUser)
        {
            request.User = authUser;
            request.UserId = authUser.Id;

            var allRequests = this.requestsRepository.GetAll();
            if (allRequests.Any(r => r.UserId == authUser.Id))
            {
                throw new InvalidOperationException(ManyRequestsErrorMessage);
            }

            if (request.Type == RequestType.PromoteToDirector && request.PlayerFullName != null)
            {
                throw new InvalidOperationException(DirectorPlayerErrorMessage);
            }

            if (request.Type == RequestType.LinkProfile && request.PlayerFullName == null)
            {
                throw new InvalidOperationException(RequestLinkProfileErrorMessage);
            }

            if (request.PlayerFullName != null)
            {
                request.Type = RequestType.LinkProfile;
                var player = this.playersRepository.GetByFullName(request.PlayerFullName);

                if (player.User != null)
                {
                    throw new InvalidOperationException(AlreadyAssociatedPlayerErrorMessage);
                }

                if (authUser.Role.Name == "Director")
                {
                    throw new InvalidOperationException(DirectorToPlayerErrorMessage);
                }

                if (authUser.Role.Name == "Verified")
                {
                    throw new InvalidOperationException(AlreadyAssociatedUserErrorMessage);
                }
            }
            else
            {
                request.Type = RequestType.PromoteToDirector;

                if (authUser.Role.Name == "Director")
                {
                    throw new InvalidOperationException(AlreadyDirectorErrorMessage);
                }

                if (authUser.Role.Name == "Verified")
                {
                    throw new InvalidOperationException(VerifiedToDirectorErrorMessage);
                }
            }
            request.Status = RequestStatus.Waiting;

            var createdRequest = this.requestsRepository.Create(request);
            var createdRequestToReturn = this.modelMapper.MapRequestToDto(createdRequest);

            return createdRequestToReturn;
        }

        public async Task UpdateAsync(int id, Request requestIncoming, User authUser)
        {
            var requestToUpdate = this.requestsRepository.GetById(id);
            var user = this.usersRepository.GetById(requestToUpdate.UserId);

            if (authUser.Role.Name != "Admin")
            {
                throw new UnauthorizedOperationException(ModifyRequestErrorMessage);
            }

            if (requestToUpdate.Type == RequestType.LinkProfile)
            {
                var player = this.playersRepository.GetByFullName(requestIncoming.PlayerFullName);
                if (requestIncoming.Status == RequestStatus.Approved)
                {   
                    player.UserId = requestToUpdate.UserId;
                    player.User = requestToUpdate.User;
                    this.usersRepository.UpdateRole(user.Id, "Verified");
                }
            }
            else
            {
                if (requestIncoming.Status == RequestStatus.Approved)
                {
                    this.usersRepository.UpdateRole(user.Id, "Director");
                }
                
            }
            this.requestsRepository.UpdateStatus(id, requestIncoming.Status);
            
            // Send email to user
            await this.emailSender.SendRequestEmailAsync(user.Email, requestIncoming.Type.ToString(), requestIncoming.Status.ToString());
        }
    }
}
