namespace WhoKnowsGame.Shared.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        //public int PlayerImageId { get; set; }
        //public PlayerImage Image { get; set; }
        public List<Game> Games { get; set; }
    }
}
