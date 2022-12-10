using System;
using System.Threading.Tasks;

namespace MatchScore.Helpers.Contracts
{
    public interface IEmailSender
    {
        Task SendRequestEmailAsync(string userEmail, string requestType, string requestStatus);
        Task SendMatchEmailAsync(string userEmail, int matchId, DateTime date, string opponent, string type);
        Task SendMailWhenAddedToTournament(string userEmail, int tournamentId, string action);
    }
}
