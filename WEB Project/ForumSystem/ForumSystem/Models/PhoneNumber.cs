namespace ForumSystem.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }  
        public string Number { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
