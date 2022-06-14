using AutoMapper;
using GameReview.Application.ViewModels;
using GameReview.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Application.Mappers
{
    public class RequestToDomainProfile: Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<UserRequest, User>();
        }
    }
}
