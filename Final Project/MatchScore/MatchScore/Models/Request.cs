using MatchScore.Models.Enums;

namespace MatchScore.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public RequestStatus Status { get; set; }
        public RequestType Type { get; set; }
        public string PlayerFullName { get; set; }
    }
}
