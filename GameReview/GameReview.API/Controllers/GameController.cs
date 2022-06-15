using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.API.Controllers
{
    [ApiController]
    [Route("api/v1/games")]
    public class GameController : ControllerBase
    {

        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        
        [HttpGet]
        public async Task<IEnumerable<GameResponse>> GetAllGames()
        {
            return await _gameService.GetAll();
        }

        [HttpGet("{id:int}")]
        public async Task<GameResponse> GetGame([FromRoute] int id)
        {
            return await _gameService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<GameResponse>> Post([FromBody] GameRequest gameResquest)
        {
            return await _gameService.RegisterAsync(gameResquest);
            
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GameResponse>> Put([FromBody] GameRequest gameResquest, [FromRoute] int id)
        {
            return await _gameService.UpdateAsync(gameResquest, id);
        }

        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GameResponse>> Delete([FromRoute] int id)
        {
            return await _gameService.DeleteAsync(id);
        }
        


    }
}
