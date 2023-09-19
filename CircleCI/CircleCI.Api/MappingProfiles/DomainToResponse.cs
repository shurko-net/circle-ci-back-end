using AutoMapper;
using CircleCI.Entities.DbSet;
using CircleCI.Entities.DTOs.Responses;

namespace CircleCI.Api.MappingProfiles;

public class DomainToResponse : Profile
{
    public DomainToResponse()
    {
        CreateMap<User, UserAuthResponse>();

        CreateMap<User, UserResponse>();

        CreateMap<CategoryList, Category>()
            .ForMember(dest => dest.CategoryId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CategoryList,
                opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());
        
        CreateMap<Post, GetPostResponse>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.Surname,
                opt => opt.MapFrom(src => src.User.Surname))
            .ForMember(dest => dest.ProfileImageUrl,
                opt => opt.MapFrom(src => src.User.ProfileImageUrl))
            .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src 
                    => src.Category.Select(c => c.CategoryId).ToList()));

        CreateMap<Comment, GetCommentResponse>()
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.Surname,
                opt => opt.MapFrom(src => src.User.Surname))
            .ForMember(dest => dest.ProfileImageUrl,
                opt => opt.MapFrom(src => src.User.ProfileImageUrl));

        CreateMap<CategoryList, GetTagsResponse>();
    }
}