using AutoMapper;
using FluentValidation;
using GameReview.Application.Exceptions;
using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels.Game;
using GameReview.Application.ViewModels.GameGender;
using GameReview.Domain.Interfaces.Commom;
using GameReview.Domain.Interfaces.Repositories;
using GameReview.Domain.Models;


namespace GameReview.Application.Services
{
    public class GameService : IGameService
    {
        private IGameRepository _gameRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IValidator<GameRequest> _validator;
        private IMapper _mapper;

        public GameService(IGameRepository gameRepository, 
                           IValidator<GameRequest> validator)
        {
            _gameRepository = gameRepository;
            _validator = validator;
        }

        public async Task<IEnumerable<GameResponse>> GetAll()
        {
            var results = await _gameRepository.GetDataAsync();
            return _mapper.Map<IEnumerable<GameResponse>>(results);
        }

        public async Task<GameResponse> GetById(int id)
        {
            var result = await _gameRepository.FirstAsync(filter: c => c.Id == id) 
                ?? throw new NotFoundRequestException($"Jogo com id: {id} não encontrado.");

            return _mapper.Map<GameResponse>(result);
        }

        public async Task<IEnumerable<GameGenderResponse>> GetAllGender()
        {
            var results = await _gameRepository.GetDataAsync();
            return _mapper.Map<IEnumerable<GameGenderResponse>>(results);
        }

        public async Task<GameResponse> RegisterAsync(GameRequest gameResquest)
        {

            var validationResult = await _validator.ValidateAsync(gameResquest);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult);

            var result = _mapper.Map<Game>(gameResquest);
            await _gameRepository.RegisterAsync(result);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<GameResponse>(gameResquest);
        }

        public async Task<GameResponse> UpdateAsync(GameRequest gameResquest, int id)
        {

            var validationResult = await _validator.ValidateAsync(gameResquest);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult);

            var gameVerify = await _gameRepository.FirstAsync(x => x.Id == id) 
                ?? throw new NotFoundRequestException($"Jogo com id: {id} não encontrado.");           

            var result = _mapper.Map(gameResquest, gameVerify);
            await _gameRepository.UpdateAsync(result);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<GameResponse>(gameResquest);

        }

        public async Task<GameResponse> DeleteAsync(int id)
        {
            var result = await _gameRepository.FirstAsync(u => u.Id == id) 
                ?? throw new NotFoundRequestException($"Jogo com id: {id} não encontrado.");

            await _gameRepository.DeleteAsync(new Game { Id = id });
            await _unitOfWork.CommitAsync();

            return _mapper.Map<GameResponse>(result);
        }  
    }
}
