using AutoMapper;
using FluentValidation;
using GameReview.Application.Exceptions;
using GameReview.Application.Interfaces;
using GameReview.Application.Options;
using GameReview.Application.ViewModels;
using GameReview.Application.ViewModels.UserViews;
using GameReview.Domain.Interfaces.Commom;
using GameReview.Domain.Interfaces.Repositories;
using GameReview.Domain.Interfaces.Storage;
using GameReview.Domain.Models;
using GameReview.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Application.Services
{
    public class UserService: IUserService
    {
        private readonly IValidatorFactory _validatorFactory;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorage _fileStorage;
        private readonly FileApiOptions _fileApiOptions;

        public UserService(IValidatorFactory validatorFactory, IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork, IFileStorage fileStorage, IOptions<FileApiOptions> options)
        {
            _validatorFactory = validatorFactory;
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _fileStorage = fileStorage;
            _fileApiOptions = options.Value;
        }

        public async Task<UserResponse> RegisterAsync(CreateUserRequest model)
        {
            var validation = await _validatorFactory.GetValidator<CreateUserRequest>().ValidateAsync(model);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            var entity = _mapper.Map<User>(model);
            entity.Password = PasswordHasher.Hash(model.Password);
            var result = await _userRepository.RegisterAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<UserResponse>(result);

        }

        public async Task<UserResponse> UpdateAsync(UserRequest model, int id)
        {
            var entity = await _userRepository.FirstAsync(e => e.Id == id) ?? throw new NotFoundRequestException($"Usuario com id: {id} não encontrado.");
            var contextValidation = new ValidationContext<UserRequest>(model);
            contextValidation.RootContextData["userId"] = id;
            var validation = await _validatorFactory.GetValidator<UserRequest>().ValidateAsync(model);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            _mapper.Map<UserRequest, User>(model , entity);

            var result = await _userRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<UserResponse>(result);
        }

        public async Task<UserResponse> UpdatePasswordAsync(PasswordRequest model, int id)
        {
            var entity = await _userRepository.FirstAsync(e => e.Id == id) ?? throw new NotFoundRequestException($"Usuario com id: {id} não encontrado.");
            
            var contextValidation = new ValidationContext<PasswordRequest>(model);
            contextValidation.RootContextData["userId"] = id;
            var validation = await _validatorFactory.GetValidator<PasswordRequest>().ValidateAsync(model);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            entity.Password = PasswordHasher.Hash(model.Password);

            var result = await _userRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<UserResponse>(result);
        }


        public async Task<UserResponse> RemoveAsync(int id)
        {
            var result = await _userRepository.FirstAsync(u => u.Id == id) ?? throw new NotFoundRequestException($"Usuario com id: {id} não encontrado.");
            await _userRepository.DeleteAsync(new User { Id = id });
            await _unitOfWork.CommitAsync();
            return _mapper.Map<UserResponse>(result);
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var result = await _userRepository.FirstAsync(filter: c => c.Id == id) ?? throw new NotFoundRequestException($"Usuario com id: {id} não encontrado.");
            return _mapper.Map<UserResponse>(result);
        }

        public async Task<UserResponse> UploadImg(int id, IFormFile img)
        {
            var entity = await _userRepository.FirstAsyncAsTracking(u => u.Id == id) ?? throw new NotFoundRequestException($"Usuario com id: {id} não encontrado.");

            if (img == null || img.Length == 0)
                throw new BadRequestException("Nenhuma imagem foi fornecida.");

            var extesionFile = Path.GetExtension(img.FileName);

            if (!_fileApiOptions.GameFileTypes.Contains(extesionFile))
                throw new BadRequestException("Formato de imagem invalido.");

            if (entity.imgPath != null)
                await _fileStorage.RemoveFile(entity.imgPath);

            await _fileStorage.IfNotExistCreateDirectory(_fileApiOptions.GameImgDirectory);

            entity.imgPath = Path.Combine(_fileApiOptions.GameImgDirectory, Guid.NewGuid().ToString() + extesionFile);          
            await _fileStorage.UploadFile(img, entity.imgPath);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<UserResponse>(entity);
        }

        public async Task<UserResponse> RemoveImg(int id)
        {
            var entity = await _userRepository.FirstAsyncAsTracking(u => u.Id == id) ?? throw new NotFoundRequestException($"Usuario com id: {id} não encontrado.");
            if(entity.imgPath != null)
            {
                await _fileStorage.RemoveFile(entity.imgPath);
                entity.imgPath = null;
            }
            return _mapper.Map<UserResponse>(entity);
        }

        public FileStream GetImg(int id)
        {
            var entity = _userRepository.FirstAsync(u => u.Id == id).GetAwaiter().GetResult()  ?? throw new NotFoundRequestException($"Usuario com id: {id} não encontrado.");

            var pathImg = _fileApiOptions.DefaultGameImgPath;

            if (entity.imgPath != null)
                pathImg = entity.imgPath;

            if (string.IsNullOrEmpty(pathImg))
                throw new BadRequestException("Nenhuma imagem encotrada.");

            return _fileStorage.GetFile(pathImg);

        }
    }
}
