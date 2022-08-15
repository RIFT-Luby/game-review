using Agenda.Application.ViewModels.Pagination;
using GameReview.Application.Constants;
using GameReview.Application.Interfaces;
using GameReview.Application.Params;
using GameReview.Application.ViewModels.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Roles = Roles.Admin)]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class GameController : ControllerBase
    {

        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }


        [HttpGet]
        public async Task<PaginationResponse<GameResponse>> GetAllGames([FromQuery] GameParams query)
        {
            return new PaginationResponse<GameResponse>
            {
                Info = await _gameService.GetAll(query),
                TotalPages = await _gameService.CountAll(),
                Skip = query.skip,
                Take = query.take,
            };
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetGame([FromRoute] int id)
        {
            var result = await _gameService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GameRequest gameResquest)
        {
            var result = await _gameService.RegisterAsync(gameResquest);
            return Ok(result);
            
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromBody] GameRequest gameResquest, [FromRoute] int id)
        {
            var result = await _gameService.UpdateAsync(gameResquest, id);
            return Ok(result);
        }

        [HttpPut("img/{id:int}")]
        public async Task<ActionResult> PutImg([FromForm]IFormFile img, [FromRoute] int id)
        {
            var result = await _gameService.UploadImg(id, img);
            return Ok(result);
        }

        [HttpDelete("img/{id:int}")]
        public async Task<ActionResult> DeleteImg([FromRoute] int id)
        {
            var result = await _gameService.RemoveImg(id);
            return Ok(result);
        }

        [HttpGet("img/{id:int}")]
        [AllowAnonymous]
        public ActionResult GetImg([FromRoute] int id)
        {
            var result = _gameService.GetImg(id);
            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var result = await _gameService.DeleteAsync(id);
            return Ok(result);
        }
        


    }
}
