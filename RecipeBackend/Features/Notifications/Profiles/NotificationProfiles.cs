using AutoMapper;
using RecipeBackend.Features.Notifications.DTOs;
using RecipeBackend.Features.Notifications.Models;

namespace RecipeBackend.Features.Notifications.Profiles;

public class NotificationProfiles : Profile
{
  public NotificationProfiles()
  {
    CreateMap<NotificationCreateDto, Notification>();
    CreateMap<Notification, NotificationDetailDto>();
    CreateMap<Notification, NotificationListDto>()
      .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.NotificationTemplate.Title))
      .ForMember(dest => dest.Subtitle, opts => opts.MapFrom(src => src.NotificationTemplate.Subtitle))
      .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.ScheduledDate.ToLocalTime()));
  }
}