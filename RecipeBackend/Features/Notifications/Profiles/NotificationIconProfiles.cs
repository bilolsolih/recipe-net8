using AutoMapper;
using RecipeBackend.Features.Notifications.DTOs;
using RecipeBackend.Features.Notifications.Models;

namespace RecipeBackend.Features.Notifications.Profiles;

public class NotificationIconProfiles : Profile
{
  public NotificationIconProfiles()
  {
    CreateMap<NotificationIconCreateDto, NotificationIcon>();
    CreateMap<NotificationIcon, NotificationIconDetailDto>()
      .ForMember(dest => dest.Created, opts => opts.MapFrom(src => src.Created.ToLocalTime()))
      .ForMember(dest => dest.Updated, opts => opts.MapFrom(src => src.Updated.ToLocalTime()));

    CreateMap<NotificationIcon, NotificationIconListDto>();
  }
}