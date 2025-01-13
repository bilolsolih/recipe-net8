using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using RecipeBackend.Features.Authentication.DTOs;
using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Authentication.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateDto, User>()
            .ForMember(u => u.Id, opt => opt.Ignore())
            .ForMember(u => u.Gender, opt => opt.Ignore())
            .ForMember(u => u.ProfilePhoto, opt => opt.Ignore())
            .ForMember(u => u.Recipes, opt => opt.Ignore())
            .ForMember(u => u.Reviews, opt => opt.Ignore())
            .ForMember(u => u.Created, opt => opt.Ignore())
            .ForMember(u => u.Updated, opt => opt.Ignore())
            .ForMember(u => u.BirthDate, opts => opts.MapFrom(src => DateOnly.Parse(src.BirthDate)));

        CreateMap<UserUpdateDto, User>()
            .ForMember(dest => dest.Presentation, opts => opts.MapFrom(src => src.Bio))
            .ForMember(dest => dest.BirthDate, opts => opts.MapFrom(src => DateOnly.Parse(src.BirthDate!)))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}