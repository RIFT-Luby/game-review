using AutoMapper;
using FluentValidation;
using GameReview.Application.Exceptions;
using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels.Review;
using GameReview.Domain.Interfaces.Commom;
using GameReview.Domain.Interfaces.Repositories;
using GameReview.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameReview.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<ReviewRequest> _validator;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IGameService _gameService;

        public ReviewService(IMapper mapper, IValidator<ReviewRequest> validator, IReviewRepository reviewRepository, IUnitOfWork unitOfWork, IAuthService authService, IGameService gameService)
        {
            _mapper = mapper;
            _validator = validator;
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
            _authService = authService;
            _gameService = gameService;
        }

        public async Task<ReviewResponse> CreateAsync(ReviewRequest model)
        {
            var validation = await _validator.ValidateAsync(model);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            var result = _mapper.Map<Review>(model);
            result.UserId = _authService.Id;

            await _gameService.UpdateGameScore(result.Score, result);

            var created = await _reviewRepository.RegisterAsync(result);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ReviewResponse>(created);
        }

        public async Task<ReviewResponse> UpdateAsync(ReviewRequest model, int id)
        {
            var reviewExist = await _reviewRepository.FirstAsync(x => x.Id == id) ?? throw new NotFoundRequestException($"Review com id: {id} não encontrado.");

            if (reviewExist.UserId != _authService.Id)
                throw new NotAuthorizedException();

            var validation = await _validator.ValidateAsync(model);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            await _gameService.UpdateGameScore(model.Score, reviewExist);

            var result = _mapper.Map(model, reviewExist);
            var updated = await _reviewRepository.UpdateAsync(result);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ReviewResponse>(updated);
        }

        public async Task<ReviewResponse> RemoveAsync(int id)
        {
            var reviewExist = await _reviewRepository.FirstAsync(x => x.Id == id) ?? throw new NotFoundRequestException($"Review com id: {id} não encontrado.");

            if (reviewExist.UserId != _authService.Id)
                throw new NotAuthorizedException();

            await _gameService.UpdateGameScore(0, reviewExist, removeReview: true);

            await _reviewRepository.DeleteAsync(reviewExist);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ReviewResponse>(reviewExist);
        }

        public async Task<IEnumerable<ReviewResponse>> GetAllAsync(Expression<Func<Review, bool>> expression = null, int? skip = null, int? take = null)
        {
            return _mapper.Map<IEnumerable<ReviewResponse>>(await _reviewRepository.GetDataAsync(x => x.UserId == _authService.Id, skip: skip, take: take));
        }

        public async Task<ReviewResponse> GetByIdAsync(int id)
        {
            var reviewExist = await _reviewRepository.FirstAsync(x => x.Id == id && x.UserId == _authService.Id) ?? throw new NotFoundRequestException($"Review com id: {id} não encontrado.");

            return _mapper.Map<ReviewResponse>(reviewExist);
        }

        public async Task<IEnumerable<ReviewResponse>> GetReviewsPerDate(DateTime minDate, DateTime? maxDate)
        {
            if (!maxDate.HasValue)
                maxDate = DateTime.Now;

            var result = await _reviewRepository.GetDataAsync(x => x.CreatedAt >= minDate && x.CreatedAt <= maxDate);
            return _mapper.Map<IEnumerable<ReviewResponse>>(result);
        }

        public Task<int> CountAll(Expression<Func<Review, bool>> filter = null)
        {
            return _reviewRepository.CountAll(filter: r => r.UserId == _authService.Id);
        }
    }
}
