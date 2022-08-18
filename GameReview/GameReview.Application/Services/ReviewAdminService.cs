using AutoMapper;
using GameReview.Application.Exceptions;
using GameReview.Application.Interfaces;
using GameReview.Application.Params;
using GameReview.Application.ViewModels.Review;
using GameReview.Domain.Interfaces.Commom;
using GameReview.Domain.Interfaces.Repositories;
using GameReview.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameReview.Application.Services
{
    public class ReviewAdminService : IReviewAdminService
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameService _gameService;

        public ReviewAdminService(IMapper mapper, IReviewRepository reviewRepository, IUnitOfWork unitOfWork, IGameService gameService)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
            _gameService = gameService;
        }

        public async Task<ReviewResponse> RemoveAsync(int id)
        {
            var reviewExist = await _reviewRepository.FirstAsync(x => x.Id == id)
                ?? throw new BadRequestException(nameof(id), $"Review com {id} não encontrado.");

            await _gameService.UpdateGameScore(0, reviewExist, removeReview: true);

            await _reviewRepository.DeleteAsync(reviewExist);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ReviewResponse>(reviewExist);
        }

        public async Task<IEnumerable<ReviewResponse>> GetAllAsync(Expression<Func<Review, bool>> expression = null, int? skip = null, int? take = null)
        {
            return _mapper.Map<IEnumerable<ReviewResponse>>(
                await _reviewRepository
                .GetDataAsync(filter: expression, skip: skip, take: take, 
                include: i => i.Include(g => g.Game).ThenInclude(gg => gg.GameGender).Include(u => u.User).ThenInclude(u => u.UserRole)));
        }

        public async Task<ReviewResponse> GetByIdAsync(int id)
        {
            var reviewExist = await _reviewRepository.FirstAsync(x => x.Id == id)
                ?? throw new BadRequestException(nameof(id), $"Review {id} não encontrado.");

            return _mapper.Map<ReviewResponse>(reviewExist);
        }

        public async Task<IEnumerable<ReviewResponse>> GetParamsAsync(ReviewAdminParams query)
        {
            return _mapper.Map<IEnumerable<ReviewResponse>>(await _reviewRepository.GetDataAsync(query.Filter(), skip: query.skip, take: query.take));
        }

        public Task<int> CountAll(Expression<Func<ReviewRequest, bool>> filter = null)
        {
            return _reviewRepository.CountAll();
        }
    }
}
