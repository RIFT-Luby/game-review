using AutoMapper;
using FluentValidation;
using GameReview.Application.Exceptions;
using GameReview.Application.Interfaces;
using GameReview.Application.Options;
using GameReview.Application.Params;
using GameReview.Application.ViewModels.Game;
using GameReview.Application.ViewModels.GameGender;
using GameReview.Domain.Core;
using GameReview.Domain.Interfaces.Commom;
using GameReview.Domain.Interfaces.Repositories;
using GameReview.Domain.Interfaces.Storage;
using GameReview.Domain.Models;
using GameReview.Domain.Models.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace GameReview.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GameRequest> _validator;
        private readonly IMapper _mapper;
        private readonly FileSettings _fileApiOptions;
        private readonly IFileStorage _fileStorage;
        private readonly IReviewRepository _reviewRepository;
        public GameService(IGameRepository gameRepository,
                           IUnitOfWork unitOfWork,
                           IMapper mapper,
                           IValidator<GameRequest> validator,
                           IOptions<FileSettings> options,
                           IFileStorage fileStorage, 
                           IReviewRepository reviewRepository)
        {
            _gameRepository = gameRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _fileApiOptions = options.Value;
            _fileStorage = fileStorage;
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<GameResponse>> GetAll(GameParams query)
        {
            var results = await _gameRepository
                .GetDataAsync(query.Filter(), skip: query.skip, take: query.take, include: i => i.Include(r => r.GameGender));
            return _mapper.Map<IEnumerable<GameResponse>>(results);
        }

        public async Task<GameResponse> GetById(int id)
        {
            var result = await _gameRepository.FirstAsync(filter: c => c.Id == id, include: i => i.Include(r => r.GameGender)) 
                ?? throw new BadRequestException(nameof(id), $"{id} não encontrado.");

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

            var entity = _mapper.Map<Game>(gameResquest);
            var result = await _gameRepository.RegisterAsync(entity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<GameResponse>(result);
        }

        public async Task<GameResponse> UpdateAsync(GameRequest gameResquest, int id)
        {
            var entity = await _gameRepository.FirstAsync(x => x.Id == id)
                    ?? throw new BadRequestException(nameof(id), $"{id} não encontrado.");

            var validationResult = await _validator.ValidateAsync(gameResquest);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult);


            _mapper.Map(gameResquest, entity);
            var result = await _gameRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<GameResponse>(result);

        }

        public async Task<GameResponse> DeleteAsync(int id)
        {
            var result = await _gameRepository.FirstAsync(u => u.Id == id)
                ?? throw new BadRequestException(nameof(id), $" Jogo com {id} não encontrado.");

            await _gameRepository.DeleteAsync(new Game { Id = id });
            await _unitOfWork.CommitAsync();

            return _mapper.Map<GameResponse>(result);
        }

        public async Task<GameResponse> UploadImg(int id, IFormFile img)
        {
            var entity = await _gameRepository.FirstAsyncAsTracking(u => u.Id == id)
                ?? throw new BadRequestException(nameof(id), $"Jogo com {id} não encontrado.");

            if (img == null || img.Length == 0)
                throw new BadRequestException("Nenhuma imagem foi fornecida.");

            var extesionFile = Path.GetExtension(img.FileName);

            if (!_fileApiOptions.GameFileTypes.Contains(extesionFile))
                throw new BadRequestException("Formato de imagem invalido.");

            if (entity.ImgPath != null)
                await _fileStorage.RemoveFile(entity.ImgPath);

            await _fileStorage.IfNotExistCreateDirectory(_fileApiOptions.GameImgDirectory);

            entity.ImgPath = Path.Combine(_fileApiOptions.GameImgDirectory, Guid.NewGuid().ToString() + extesionFile);
            await _fileStorage.UploadFile(img, entity.ImgPath);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<GameResponse>(entity);
        }

        public async Task<GameResponse> RemoveImg(int id)
        {
            var entity = await _gameRepository.FirstAsyncAsTracking(u => u.Id == id)
                ?? throw new BadRequestException(nameof(id), $"Jogo com {id} não encontrado.");

            if (entity.ImgPath != null)
            {
                await _fileStorage.RemoveFile(entity.ImgPath);
                entity.ImgPath = null;
                await _unitOfWork.CommitAsync();
                return _mapper.Map<GameResponse>(entity);
            }
            throw new BadRequestException($"Não existe nenhuma imagem no game com id: {id}");
        }

        public FileStream GetImg(int id)
        {
            var entity = _gameRepository.FirstAsync(u => u.Id == id).GetAwaiter().GetResult()
                ?? throw new BadRequestException(nameof(id), $"Jogo com {id} não encontrado.");

            var pathImg = _fileApiOptions.DefaultGameImgPath;

            if (entity.ImgPath != null)
                pathImg = entity.ImgPath;

            if (string.IsNullOrEmpty(pathImg))
                throw new BadRequestException("Nenhuma imagem encotrada.");

            return _fileStorage.GetFile(pathImg);

        }
        public async Task UpdateGameScore(int newScore, Review review, bool removeReview = false)
        {
            var game = await _gameRepository.FirstAsyncAsTracking(filter: c => c.Id == review.GameId)
                    ?? throw new BadRequestException(nameof(review.GameId), $"Jogo com {review.GameId} não encontrado.");

            decimal countReview = (decimal)_reviewRepository.QueryData<int>(q => q.Count(r => r.GameId == review.GameId));
            decimal sumScore = 0;
            if (review.Id == 0)
            {
                sumScore = game.Score * countReview;
                countReview++;
            }
            else
            {
                sumScore = game.Score * countReview - review.Score;
                if (removeReview)
                {
                    countReview--;
                    newScore = 0;
                }

            }
            if (countReview == 0)
            {
                game.Score = 0;
            }
            else
            {
                game.Score = (sumScore + (decimal)newScore) / countReview;
            }
        }

        public Task<int> CountAll(Expression<Func<GameRequest, bool>> filter = null)
        {
            return _gameRepository.CountAll();
        }

        public IEnumerable<GameGenderResponse> GetGameTypes()
        {
            return _mapper.Map<IEnumerable<GameGenderResponse>>(Enumeration.GetAll<GameGender>());
        }
    }
}
