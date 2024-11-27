using System.Net.Http.Json;
using WhoKnowsGame.Shared.Interfaces;
using WhoKnowsGame.Shared.Models;

namespace WhoKnowsGame.Client.Services
{
    public class GameService : IGameService
    {
        private readonly HttpClient httpClient;

        public GameService(HttpClient httpClient) => this.httpClient = httpClient;

        public async Task<Game> GetGame(int gameId) => await httpClient.GetFromJsonAsync<Game>($"game/{gameId}");
    }
}
