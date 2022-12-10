using MatchScore.Models;
using MatchScore.Models.Enums;
using MatchScore.Models.QueryParameters;
using System.Collections.Generic;

namespace MatchScore.Repositories.Contracts
{
    public interface IRequestsRepository
    {
        List<Request> GetAll();
        PaginatedList<Request> FilterBy(RequestQueryParameters filterParameters);
        Request GetById(int id);
        Request Create(Request request);
        void UpdateStatus(int id, RequestStatus status);
    }
}
