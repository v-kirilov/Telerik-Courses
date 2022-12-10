using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.QueryParameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchScore.Services.Contracts
{
    public interface IRequestsService
    {
        List<RequestDto> GetAll(User authUser);
        PaginatedList<RequestDtoName> FilterBy(RequestQueryParameters filterParameters, User authUser);
        RequestDto GetById(int id, User authUser);
        RequestDto Create(Request request, User authUser);
        Task UpdateAsync(int id, Request requestIncoming, User authUser);
    }
}
