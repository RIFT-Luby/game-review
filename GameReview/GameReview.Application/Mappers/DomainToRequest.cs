
using AutoMapper;
using GameReview.Application.ViewModels.Game;
using GameReview.Domain.Models;

namespace GameReview.Application.Mappers
{
    public class DomainToRequest : Profile
    {
        public DomainToRequest()
        {
            CreateMap<Game, GameRequest>().ReverseMap();
            CreateMap<Game, GameResponse>().ReverseMap();
        }   
    }
}
