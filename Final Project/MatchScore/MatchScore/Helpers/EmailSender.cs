using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Mailjet.Client.TransactionalEmails;
using MatchScore.Helpers.Contracts;
using MatchScore.Models.Enums;
using Microsoft.Extensions.Options;
using MatchScore.Helpers;

namespace Mailjet.ConsoleApplication
{
    public class EmailSender : IEmailSender
    {
        private const string EmailSenderContact = "test.match.score@abv.bg";
        private const string RequestsSubject = "PlayToWin Requests";
        private readonly IOptions<EmailSenderConfig> emailSenderConfig;

        public EmailSender(IOptions<EmailSenderConfig> emailSenderConfig)
        {
            this.emailSenderConfig = emailSenderConfig;
        }

        public async Task SendRequestEmailAsync(string userEmail, string requestType, string requestStatus)
        {
            var client = CreateMailJetClient();

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            };

            var text = GetEmailTemplateForRequests(requestType, requestStatus);

            // construct your email with builder
            TransactionalEmail email = ConstructEmail(text);

            // invoke API to send email
            var response = await client.SendTransactionalEmailAsync(email);
            // check response
            //Assert.AreEqual(1, response.Messages.Length);
        }

        private static TransactionalEmail ConstructEmail(string text)
        {
            return new TransactionalEmailBuilder()
                               .WithFrom(new SendContact(EmailSenderContact))
                               .WithSubject(RequestsSubject)
                               .WithTextPart(text)
                               .WithTo(new SendContact("test.match.score@abv.bg"))
                               .Build();
        }

        private MailjetClient CreateMailJetClient()
        {
            return new MailjetClient(
                this.emailSenderConfig.Value.ApiKey,
                this.emailSenderConfig.Value.ApiSecret
                );
        }

        private static string GetEmailTemplateForRequests(string requestType, string requstStatus)
        {
            var requestTypeEmail = string.Empty;

            if (requestType.Equals("promotetodirector", StringComparison.InvariantCultureIgnoreCase))
            {
                requestTypeEmail = "promote-to-director";
            }
            else
            {
                requestTypeEmail = "link-profile";
            }

            var text = $"Hello,\n\nYour {requestTypeEmail} request was {requstStatus?.ToLower()}.\n\nBest Regards,\nPlayToWin";

            return text;
        }

        public async Task SendMatchEmailAsync(string userEmail, int matchId, DateTime date, string opponent, string type)
        {
            var client = CreateMailJetClient();

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            };
            var text = "";
            if (type == "newMatch")
            {
                text = GetEmailTemplateForNewMatch(matchId, date, opponent);
            }
            if (type == "updateMatch")
            {
                text = GetEmailTemplateForMatchDateUpdate(matchId, date, opponent);
            }

            // construct your email with builder
            TransactionalEmail email = ConstructEmail(text);

            // invoke API to send email
            var response = await client.SendTransactionalEmailAsync(email);
            // check response
            //Assert.AreEqual(1, response.Messages.Length);
        }



        private static string GetEmailTemplateForNewMatch(int matchId, DateTime date, string opponent)
        {
            var requestTypeEmail = string.Empty;

            var text = $"Hello,\n\nYou have been added to match with opponent {opponent}. Match id: {matchId}. Date: {date.Date.ToString()}.\n\nBest Regards,\nPlayToWin";

            return text;
        }

        private static string GetEmailTemplateForMatchDateUpdate(int matchId, DateTime date, string opponent)
        {
            var requestTypeEmail = string.Empty;

            var text = $"Hello,\n\nThe date of your match with id {matchId} and opponent {opponent} has been changed to {date.Date}.\n\nBest Regards,\nPlayToWin";

            return text;
        }

        public async Task SendMailWhenAddedToTournament(string userEmail, int tournamentId, string action)
        {
            var client = CreateMailJetClient();

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            };
            var text = string.Empty;
            if (action == "added")
            {
                text = GetEmailTemplateForAddedToTournament(tournamentId);

            }
            else
            {
                text = GetEmailTemplateForRemovedFromTournament(tournamentId);
            }

            // construct your email with builder
            TransactionalEmail email = ConstructEmail(text);

            // invoke API to send email
            var response = await client.SendTransactionalEmailAsync(email);
            // check response
        }

        private string GetEmailTemplateForRemovedFromTournament(int tournamentId)
        {
            var requestTypeEmail = string.Empty;

            var text = $"Hello,\n\nYou have been removed from tournament with id {tournamentId}.\n\nBest Regards,\nPlayToWin";

            return text;
        }

        private static string GetEmailTemplateForAddedToTournament(int tournamentId)
        {
            var requestTypeEmail = string.Empty;

            var text = $"Hello,\n\nYou have been added to tournament with id {tournamentId}.\n\nBest Regards,\nPlayToWin";

            return text;
        }
    }
}
