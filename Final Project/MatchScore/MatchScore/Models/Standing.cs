namespace MatchScore.Models
{
	public class Standing
	{

		public Standing(string playerName, int points)
		{
			PlayerName = playerName;
			Points = points;
		}

		public int Id { get; set; }
		public int TournamentId { get; set; }
		public Tournament Tournament { get; set; }
		public string PlayerName { get; set; }
		public int Points { get; set; }
		public int Goals { get; set; }
	}
}
