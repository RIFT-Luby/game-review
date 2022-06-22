﻿using AutoMapper;
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
        private readonly IGameRepository _gameRepository;
        private readonly IAuthService _authService;

        public ReviewService(IMapper mapper, IValidator<ReviewRequest> validator, IReviewRepository reviewRepository, IUnitOfWork unitOfWork, IGameRepository gameRepository, IAuthService authService)
        {
            _mapper = mapper;
            _validator = validator;
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
            _gameRepository = gameRepository;
            _authService = authService;
        }

        public async Task<ReviewResponse> CreateAsync(ReviewRequest model)
        {
            var validation = await _validator.ValidateAsync(model);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            var result = _mapper.Map<Review>(model);
            result.UserId = _authService.Id;

            await updateGameScore(result.Score, result);

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

            await updateGameScore(model.Score, reviewExist);

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

            await updateGameScore(0, reviewExist, removeReview: true);

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
