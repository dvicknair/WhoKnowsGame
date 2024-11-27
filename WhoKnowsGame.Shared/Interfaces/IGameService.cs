using WhoKnowsGame.Shared.Models;

namespace WhoKnowsGame.Shared.Interfaces
{
    public interface IGameService
    {
        Task<Game> GetGame(int gameId);
    }
}