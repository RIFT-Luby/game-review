using AutoMapper;
using GameReview.Application.ViewModels;
using GameReview.Application.ViewModels.UserViews;
using GameReview.Domain.Models;
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
        }
    }
}
