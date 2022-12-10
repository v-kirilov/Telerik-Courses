using MatchScore.Models;
using MatchScore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Services.Contracts
{
    public interface ISportClubsService
    {
        List<SportClub> GetAll();
        SportClub GetById(int id);
        SportClub GetByName(string name);
        SportClub Create(SportClub sportClub);
        SportClub Update(int id, SportClub sportClub, User authUser);
        void Delete(int id, User authUser);
        bool SportClubExists(string sportClubName);
    }
}
