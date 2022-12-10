using MatchScore.Models;
using MatchScore.Models.Enums;
using MatchScore.Models.QueryParameters;
using System;
using System.Collections.Generic;

namespace MatchScore.Repositories.Contracts
{
    public interface IMatchesRepository
    {
        List<Match> GetAll();
        Match GetMatchById(int matchId);
        List<Match> GetMatchesByParticipant(int participantId);
        List<Match> GetMatchesByTournament(int tournamentId);
        List<Match> GetMatchesByRound(int tournamentId, int roundId);
        List<Match> GetMatchesByDirector(int directorId);
        Player GetMatchWinner(int matchId);
        PaginatedList<Match> FilterBy(MatchQueryParameters filterParameters);
        Match Create(Match match);
        Match UpdateMatchDate(int matchId, string date);
        Match UpdateMatchFormat(int matchId, MatchFormat format);
        Match UpdateMatchScore(int matchId, string playerName, int score);
        Match UpdateMatchParticipant(int matchId, string newName, string oldName);
        Match UpdateMatchTournament(int matchId, int tournamentId);
        Match UpdateMatchDirector(int matchId, int directorId);
        Match UpdateMatchStatus(int matchId, Status status);
        void Delete(int matchId);

    }
}
