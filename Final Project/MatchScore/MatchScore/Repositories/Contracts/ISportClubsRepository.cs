using MatchScore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Repositories.Contracts
{
    public interface ISportClubsRepository
    {
        List<SportClub> GetAll();
        SportClub GetById(int id);
        SportClub GetByName(string name);
        SportClub Create(SportClub sportClub);
        SportClub UpdateName(int id, string sportClubName);
        void Delete(int id);
    }
}
