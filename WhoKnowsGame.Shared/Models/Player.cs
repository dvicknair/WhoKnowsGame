namespace WhoKnowsGame.Shared.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Game> Games { get; set; }
    }
}
