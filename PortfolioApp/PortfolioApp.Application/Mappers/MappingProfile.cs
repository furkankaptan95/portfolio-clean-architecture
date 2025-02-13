using AutoMapper;
using PortfolioApp.Core.DTOs.Web.Comment;
using PortfolioApp.Core.Entities;

namespace PortfolioApp.Application.Mappers;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CommentEntity, CommentWebDto>()
    .ForMember(dest => dest.Commenter, opt => opt.MapFrom(src =>
        !string.IsNullOrEmpty(src.UnsignedCommenterName) ? src.UnsignedCommenterName : src.SignedCommenterName
    ));

    }
}
