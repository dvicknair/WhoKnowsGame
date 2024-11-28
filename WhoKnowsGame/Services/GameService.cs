using Microsoft.EntityFrameworkCore;
using WhoKnowsGame.Data;
using WhoKnowsGame.Shared.Dtos;
using WhoKnowsGame.Shared.Interfaces;
using WhoKnowsGame.Shared.Models;

namespace WhoKnowsGame.Services
{
    public class GameService : IGameService
    {
        private readonly WhoKnowsDbContext db;

        public GameService(WhoKnowsDbContext db) => this.db = db;

        public async Task<Player> EnterGame(EnterGameDto enterGameDto)
        {
            var newPlayer = new Player
            {
                FirstName = enterGameDto.FirstName,
                LastName = enterGameDto.LastName
            };
            newPlayer.Games = new List<Game> { db.Games.FirstOrDefault(x => x.Id == enterGameDto.GameId) };
            db.Players.Add(newPlayer);
            await db.SaveChangesAsync();

            //newPlayer.Image = null;
            return newPlayer;
        }

        public async Task<Game> StartGame(int gameId)
        {
            var game = db.Games.Find(gameId);
            game.Status = 1;
            await db.SaveChangesAsync();
            return game;
        }

        public async Task<Game> GetGame(int gameId) => await db.Games.Include(x => x.Riddles).ThenInclude(x => x.Answers).FirstOrDefaultAsync(x => x.Id == gameId);

        public async Task<List<Player>> GetPlayers(int gameId) => await db.Players
            //.Include(x => x.Image)
            .Where(x => x.Games.Any(g => g.Id == gameId)).ToListAsync();

        public async Task AnswerRiddle(AnswerRiddleDto answerRiddleDto)
        {
            var playerRiddleAnswer = new PlayerRiddleAnswer
            {
                PlayerId = answerRiddleDto.PlayerId,
                RiddleId = answerRiddleDto.RiddleId,
                AnswerId = answerRiddleDto.AnswerId
            };
            db.PlayerRiddleAnswers.Add(playerRiddleAnswer);
            await db.SaveChangesAsync();
        }
    }
}
