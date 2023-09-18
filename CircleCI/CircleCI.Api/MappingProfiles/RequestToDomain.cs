using AutoMapper;
using CircleCI.Entities.DbSet;
using CircleCI.Entities.DTOs.Requests;

namespace CircleCI.Api.MappingProfiles;

public class RequestToDomain : Profile
{
    public RequestToDomain()
    {
        CreateMap<UserRegistrationRequest, User>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Token,
                opt => opt.MapFrom(src => new Token()));

        CreateMap<CreateCommentRequest, Comment>()
            .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(src => DateTime.UtcNow));
        
    }
}