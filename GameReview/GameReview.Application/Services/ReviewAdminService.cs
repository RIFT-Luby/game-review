using AutoMapper;
using GameReview.Application.Exceptions;
using GameReview.Application.Interfaces;
using GameReview.Application.Params;
using GameReview.Application.ViewModels.Review;
using GameReview.Domain.Interfaces.Commom;
using GameReview.Domain.Interfaces.Repositories;
using GameReview.Domain.Models;
using System.Linq.Expressions;

namespace GameReview.Application.Services
{
    public class ReviewAdminService : IReviewAdminService
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameRepository _gameRepository;

        public ReviewAdminService(IMapper mapper, IReviewRepository reviewRepository, IUnitOfWork unitOfWork, IGameRepository gameRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
            _gameRepository = gameRepository;
        }

        public async Task<ReviewResponse> RemoveAsync(int id)
        {
            var reviewExist = await _reviewRepository.FirstAsync(x => x.Id == id) ?? throw new NotFoundRequestException($"Review com id: {id} não encontrado.");

            await updateGameScore(0, reviewExist, removeReview: true);

            await _reviewRepository.DeleteAsync(reviewExist);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ReviewResponse>(reviewExist);
        }

        public async Task<IEnumerable<ReviewResponse>> GetAllAsync(Expression<Func<Review, bool>> expression = null, int? skip = null, int? take = null)
        {
            return _mapper.Map<IEnumerable<ReviewResponse>>(await _reviewRepository.GetDataAsync(filter: expression, skip: skip, take: take));
        }

        public async Task<ReviewResponse> GetByIdAsync(int id)
        {
            var reviewExist = await _reviewRepository.FirstAsync(x => x.Id == id) ?? throw new NotFoundRequestException($"Review com id: {id} não encontrado.");

            return _mapper.Map<ReviewResponse>(reviewExist);
        }

        public async Task<IEnumerable<ReviewResponse>> GetParamsAsync(ReviewAdminParams reviewAdminParams)
        {
            return _mapper.Map<IEnumerable<ReviewResponse>>(await _reviewRepository.GetDataAsync(reviewAdminParams.Filter()));
        }

        public async Task updateGameScore(int newScore, Review review , bool removeReview = false)
        {
            var game = await _gameRepository.FirstAsyncAsTracking(filter: c => c.Id == review.GameId) ?? throw new NotFoundRequestException($"Jogo com id: {review.GameId} não encontrado.");
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

        
    }
}
