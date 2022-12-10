using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.Enums;
using MatchScore.QueryParameters;
using System.Collections.Generic;

namespace MatchScore.Repositories.Contracts
{
    public interface ITournamentsRepository
    {
        List<Tournament> GetAll();
        PaginatedList<Tournament> FilterBy(TournamentQueryParameters filterPar);
        void Delete(int id);
        void DeleteRound(int tournamentId, int roundId);

        Tournament GetById(int id);
        Round CreateRound(Tournament tournament);
        Tournament CreateTournament(Tournament tournament);
        Tournament UpdateTournament(Tournament updateTournament);
        Tournament UpdateTournamentTitle(int id, string title);
        Tournament UpdateTournamentPrize(int id, TournamentPrize prize);
        Tournament GetByTitle(string title);
        Tournament GetByDirector(User manager);
        int GetIdOfTournament(int id);
        Round GetRoundById(int tournaentId, int roundId);
        List<Round> GetRounds(int tournamentId);
        Round GetRoundByRoundNumber(int tournamentId, int roundNumber);
        Round UpdateRound(Round round, int roundNumber, int tournamentId);
        void UpdateRound(Round roundToUpdate);
        List<Tournament> GetByPlayer(int playerId);
        Player AddPlayerToTournament(Player player, int tournamentId);

        void RemovePlayerFromTournament(Player player, int tournamentId);
        List<Player> GetPlayersOfTournament(int tournamentId);
        Tournament UpdateTournamentRounds(Tournament updateTournament);
        int GetTournamentIdByMatchId(int matchId);
        void UpdateTournamentStandings(int tournamentId, List<string> results);
        void CreateStanding(List<string> players, int tournamenId);
        void UpdateFinishedTournament(TournamentDto dto);
    }
}
