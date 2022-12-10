using MatchScore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Services.Contracts
{
    public interface IPhotoService
    {
        string GetPhotoUrl(int id);
        Task<PhotoForReturn> AddPhotoForPlayer(int playerId, PhotoForCreation photoDto);
        Task SaveAll();
    }
}
