using AutoMapper;
using GameReview.Application.ViewModels;
using GameReview.Application.ViewModels.Game;
using GameReview.Application.ViewModels.Review;
using GameReview.Application.ViewModels.UserViews;
using GameReview.Domain.Models;
using GameReview.Domain.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Application.Mappers
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<Review, ReviewResponse>();
            CreateMap<UserRole, UserRoleResponse>();
            CreateMap<Game, GameResponse>();
        }
    }
}
