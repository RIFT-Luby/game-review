using AutoMapper;
using FluentValidation;
using GameReview.Application.Exceptions;
using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels.Review;
using GameReview.Domain.Interfaces.Commom;
using GameReview.Domain.Interfaces.Repositories;
using GameReview.Domain.Models;

namespace GameReview.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<ReviewRequest> _validator;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IMapper mapper, IValidator<ReviewRequest> validator, IReviewRepository reviewRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _validator = validator;
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReviewResponse> CreateAsync(ReviewRequest model)
        {
            var validation = await _validator.ValidateAsync(model);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            var result = _mapper.Map<Review>(model);

            var created = await _reviewRepository.RegisterAsync(result);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ReviewResponse>(model);
        }

        public async Task<ReviewResponse> UpdateAsync(ReviewRequest model, int id)
        {
            var reviewExist = await _reviewRepository.FirstAsync(x => x.Id == id);
            if (reviewExist == null)
                throw new NotFoundRequestException();

            var validation = await _validator.ValidateAsync(model);
            if (!validation.IsValid)
                throw new BadRequestException(validation);


            var result = _mapper.Map(model, reviewExist);

            var updated = await _reviewRepository.UpdateAsync(result);

            return _mapper.Map<ReviewResponse>(updated);
        }

        public async Task RemoveAsync(int id)
        {
            var reviewExist = await _reviewRepository.FirstAsync(x => x.Id == id);
            if (reviewExist == null)
                throw new NotFoundRequestException();

            await _reviewRepository.DeleteAsync(reviewExist);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ReviewResponse>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<ReviewResponse>>(await _reviewRepository.GetDataAsync());
        }

        public async Task<ReviewResponse> GetByIdAsync(int id)
        {
            return _mapper.Map<ReviewResponse>(await _reviewRepository.FirstAsync(x => x.Id == id));
        }
    }
}
