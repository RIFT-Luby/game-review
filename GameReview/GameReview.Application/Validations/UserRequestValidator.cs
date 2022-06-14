using FluentValidation;
using GameReview.Application.ViewModels;
using GameReview.Domain.Core;
using GameReview.Domain.Interfaces.Repositories;
using GameReview.Domain.Models;
using GameReview.Domain.Models.Enumerations;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Application.Validations
{
    public class UserRequestValidator: AbstractValidator<UserRequest>
    {
        private readonly IUserRepository _userRepository;
        public UserRequestValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;   

            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(x => x.Email)
                .MustAsync(( model, email, context, cancelToken) => VerifyHasAny(x => x.Email == email, context))
                .WithMessage("{PropertyName} Já existe um usuário com e-mail informado.");

            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(x => x.Password)
                .MinimumLength(3)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(x => x.UserRoleId)
                .Must(type => Enumeration.GetAll<UserRole>().Any(x => x.Id == type))
                .WithMessage("{Propertyname} Cargo de usuário inválido");
        }

        public async Task<bool> VerifyHasAny(Expression<Func<User, bool>> func, ValidationContext<UserRequest> context)
        {
            var predicate = PredicateBuilder.New<User>();
            predicate = predicate.And(func);
            if (context.RootContextData.TryGetValue("userId", out object? userId)
                && userId != null)
            {
                var id = userId as int?;
                predicate.And(x => x.Id != id!);
            }

            return !(await _userRepository.HasAnyAsync(predicate));
        }
    }
}
