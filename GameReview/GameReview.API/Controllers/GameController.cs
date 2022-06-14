using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.API.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {

        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /* Em testes
        [HttpGet]
        public async IEnumerable<GameResquest> GetAllGames()
        {

        }

        [HttpGet("{id:int}")]
        public async Task<GameResquest> GetGame(int id)
        {

        }
        */

        [HttpPost]
        public async Task<ActionResult<GameResquest>> Post([FromBody] GameResquest gameResquest)
        {
            return await _gameService.RegisterAsync(gameResquest);
            
        }

        [HttpPut]
        public async Task<ActionResult<GameResquest>> Put([FromBody] GameResquest gameResquest, int id)
        {
            return await _gameService.UpdateAsync(gameResquest, id);
        }

        /* Em testes
        [HttpDelete]
        public async Task<ActionResult<GameResquest>> Delete(int id)
        {
            //return await _gameService.DeleteAsync();
        }
        */


    }
}
