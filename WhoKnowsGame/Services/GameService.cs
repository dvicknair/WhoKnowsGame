using Microsoft.EntityFrameworkCore;
using WhoKnowsGame.Data;
using WhoKnowsGame.Shared.Interfaces;
using WhoKnowsGame.Shared.Models;

namespace WhoKnowsGame.Services
{
    public class GameService : IGameService
    {
        private readonly WhoKnowsDbContext db;

        public GameService(WhoKnowsDbContext db) => this.db = db;

        public async Task<Game> GetGame(int gameId) => await db.Games.Include(x => x.Riddles).ThenInclude(x => x.Answers).FirstOrDefaultAsync(x => x.Id == gameId);
    }
}
