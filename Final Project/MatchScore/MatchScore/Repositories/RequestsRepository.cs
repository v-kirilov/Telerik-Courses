using MatchScore.Data;
using MatchScore.Exceptions;
using MatchScore.Models;
using MatchScore.Models.Enums;
using MatchScore.Models.QueryParameters;
using MatchScore.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchScore.Repositories
{
    public class RequestsRepository : IRequestsRepository
    {
        private const string NotFoundRequestIdErrorMessage = "Request with id:{0} does not exist";
        private readonly ApplicationContext context;

        public RequestsRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public List<Request> GetAll()
        {
            var requests = this.GetRequests().ToList();
            return requests;
        }

        public Request GetById(int id)
        {
            var request = this.GetRequests().FirstOrDefault(r => r.Id == id);
            return request ?? throw new EntityNotFoundException(string.Format(NotFoundRequestIdErrorMessage, id));
        }

        public Request Create(Request request)
        {
            
            this.context.Requests.Add(request);
            this.context.SaveChanges();

            return request;
        }

        public void UpdateStatus(int id, RequestStatus status)
        {
            var requestToUpdate = this.GetById(id);
            requestToUpdate.Status = status;
            this.context.Update(requestToUpdate);
            this.context.SaveChanges();
        }

        public PaginatedList<Request> FilterBy(RequestQueryParameters filterParameters)
        {
            IQueryable<Request> result = this.GetRequests();
            result = FilterByUserEmail(result, filterParameters.UserEmail);
            result = SortBy(result, filterParameters.SortBy);
            result = Order(result, filterParameters.SortOrder);

            double totalPages = Math.Ceiling((1.0 * result.Count()) / filterParameters.PageSize);
            result = Paginate(result, filterParameters.PageNumber, filterParameters.PageSize);

            return new PaginatedList<Request>(result.ToList(), (int)totalPages, filterParameters.PageNumber);
        }
        private static IQueryable<Request> FilterByUserEmail(IQueryable<Request> requests, string userEmail)
        {
            if (!string.IsNullOrEmpty(userEmail))
            {
                return requests.Where(r => r.User.Email.Contains(userEmail));
            }

            return requests;
        }
        private static IQueryable<Request> SortBy(IQueryable<Request> requests, string sortCriteria)
        {
            if (string.IsNullOrEmpty(sortCriteria))
            {
                return requests;
            }
            switch (sortCriteria.ToLower())
            {
                case "useremail":
                    return requests.OrderBy(r => r.User.Email);
                default:
                    return requests;
            }
        }

        private static IQueryable<Request> Order(IQueryable<Request> requests, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortOrder))
            {
                return requests;
            }
            switch (sortOrder.ToLower())
            {
                case "desc":
                    return requests.Reverse();
                default:
                    return requests;
            }
        }

        private static IQueryable<Request> Paginate(IQueryable<Request> requests, int pageNumber, int pageSize)
        {
            return requests.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        private IQueryable<Request> GetRequests()
        {
            return this.context.Requests.Where(r => r.Status == RequestStatus.Waiting)
                    .Include(r => r.User);
        }
    }
}
