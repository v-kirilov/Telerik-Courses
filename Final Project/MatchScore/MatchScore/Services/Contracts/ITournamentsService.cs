using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.QueryParameters;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MatchScore.Services.Contracts
{
    public interface ITournamentsService
    {
        Task<TournamentDto> CreateTournament(Tournament tournament);
        public TournamentDto CreateRound(int tournamentId);
        void DeleteTournament(int id);
        List<TournamentDto> GetAll();
        PaginatedList<TournamentDto> FilterBy(TournamentQueryParameters filterPar);
        PaginatedList<TournamentDto> FilterByForApi(TournamentQueryParameters filterPar);
        TournamentDto GetById(int id);
        TournamentDto UpdateTournament(int id, TournamentDto tourDto, int directorId);
        RoundDto UpdateTournamentRound(Round round, int roundNumber, int tournamentId);
        RoundDto GetRoundById(int tournamentId, int roundId);
        List<RoundDto> GetRounds(int tournamentId);
        RoundDto GetRoundByRoundNumber(int tournamentId, int roundNumber);
        Task<PlayerDto> AddPlayerToTournament(int playerId, int tournamentId);
        Task<int> AddPlayerToTournament(Player player, int tournamentId);
        Task<int> RemovePlayerFromTournament(int playerId, int tournamentId);
        List<PlayerDto> GetPlayersOfTournament(int tournamentId);
        PlayerDto GetSinglePLayer(int tournamentId, int playerId);
        List<TournamentDto> GetByPlayer(int playerId);
        bool IsDirector(User director, int tournamentId);
        bool IsDirectorOrAdmin(User user);
        bool IsDirectorOfCurrentTournament(User director, int tournamentId);
        TournamentDto GetTournamentIdByMatchId(int matchId);
        void UpdateTournamentStandings(int tournamentId, List<string> results);
        void CreateStanding(List<string> players, int tournamentId);
        void UpdateFinishedTournament(TournamentDto dto);
        bool IsDirectorOrAdminView(User user, int tournamentId);
    }
}
