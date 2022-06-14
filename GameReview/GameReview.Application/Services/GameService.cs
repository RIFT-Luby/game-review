using AutoMapper;
using FluentValidation;
using GameReview.Application.Exceptions;
using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels.Game;
using GameReview.Domain.Interfaces.Repositories;
using GameReview.Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GameReview.Application.Services
{
    public class GameService : IGameService
    {
        private IGameRepository _gameRepository;
        private readonly IValidator<GameResquest> _validator;
        private IMapper _mapper;

        public GameService(IGameRepository gameRepository, 
                           IValidator<GameResquest> validator)
        {
            _gameRepository = gameRepository;
            _validator = validator;
        }

        public async Task<GameResquest> RegisterAsync(GameResquest gameResquest)
        {

            var validationResult = await _validator.ValidateAsync(gameResquest);

            if (!validationResult.IsValid)
                throw new BadRequestException("Game is invalid");

            var result = _mapper.Map<Game>(gameResquest);
            await _gameRepository.RegisterAsync(result);

            return gameResquest;
        }

        public async Task<GameResquest> UpdateAsync(GameResquest gameResquest, int id)
        {

            var validationResult = await _validator.ValidateAsync(gameResquest);

            if (!validationResult.IsValid)
                throw new BadRequestException("Game is invalid.");

            var gameVerify = await _gameRepository.FirstAsync(x => x.Id == id);

            if (gameVerify == null)
                throw new NotFoundRequestException("Game not found.");

            var result = _mapper.Map(gameResquest, gameVerify);


            return gameResquest;

        }

        public async Task<GameResquest> DeleteAsync(GameResquest gameResquest)
        {
            var validationResult = await _validator.ValidateAsync(gameResquest);

            if (!validationResult.IsValid)
                throw new BadRequestException("Game is invalid");

            var result = _mapper.Map<Game>(gameResquest);
            await _gameRepository.DeleteAsync(result);

            return gameResquest;
        }

        public Task<GameResquest?> FirstAsync(Expression<Func<GameResquest, bool>> filter, Func<IQueryable<GameResquest>, IIncludableQueryable<GameResquest, object>>? include = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GameResquest>> GetDataAsync(Expression<Func<GameResquest, bool>>? filter = null, Func<IQueryable<GameResquest>, IIncludableQueryable<GameResquest, object>>? include = null, int? skip = null, int? take = null)
        {
            throw new NotImplementedException();
        }
       
    }
}
