using MatchScore.Models;
using MatchScore.Models.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchScore.Repositories.Contracts
{
    public interface IPlayersRepository
    {
        List<Player> GetAll();
        Player GetById(int id);
        Player GetByFullName(string fullName);
        PaginatedList<Player> FilterBy(PlayerQueryParameters filterParameters);
        Player Create(Player player);
        Player UpdateCountry(int id, string countryName);
        Player UpdateFullName(int id, string fullName);
        Player UpdateSportClub(int id, string sportClubName);
        Player UpdateUser(int id, string userEmail);
        void Delete(int id);
        int CountMatchesPlayed(int id);
        int CountMatchesWon(int id);
        int CountMatchesLoss(int id);
        int CountMatchesDraw(int id);
        int CountTournamentsPlayed(int id);
        string GetNameMostPlayedOpponent(int id);
        string GetNameBestOpponent(int id);
        string GetNameWorstOpponent(int id);
        string GetBestWinLossRatio(int id);
        string GetWorstWinLossRatio(int id);
        int CountTournamentsWon(int id);
    }
}
