using MatchScore.Models;
using MatchScore.Models.DTO;
using MatchScore.Models.QueryParameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchScore.Services.Contracts
{
    public interface IMatchesService
    {
        List<MatchDto> GetAll();
        MatchDto GetMatchById(int matchId);
        List<MatchDto> GetMatchesByParticipant(int participantId);
        List<MatchDto> GetMatchesByTournament(int tournamentId);
        List<MatchDto> GetMatchesByRound(int tournamentId, int roundId);
        List<MatchDto> GetMatchesByDirector(int directorId);
        PlayerDto GetMatchWinner(int matchId);
        PaginatedList<MatchDto> FilterBy(MatchQueryParameters filterParameters);
        Task<MatchDto> Create(Match matchDto);
        Task<MatchDto> UpdateMatch(int matchId, MatchDto matchDto);
        void Delete(int matchId);

    }
}
