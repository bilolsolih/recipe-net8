using AutoMapper;
using RecipeBackend.Features.Notifications.DTOs;
using RecipeBackend.Features.Notifications.Models;

namespace RecipeBackend.Features.Notifications.Profiles;

public class NotificationTemplateProfiles : Profile
{
  public NotificationTemplateProfiles()
  {
    CreateMap<NotificationTemplateCreateDto, NotificationTemplate>();
    CreateMap<NotificationTemplate, NotificationTemplateDetailDto>()
      .ForMember(dest => dest.Created, opts => opts.MapFrom(src => src.Created.ToLocalTime()))
      .ForMember(dest => dest.Updated, opts => opts.MapFrom(src => src.Updated.ToLocalTime()))
      .ForMember(dest => dest.Icon, opts => opts.MapFrom(src => src.NotificationIcon.Icon));
    
    CreateMap<NotificationTemplate, NotificationTemplateListDto>()
      .ForMember(dest => dest.Icon, opts => opts.MapFrom(src => src.NotificationIcon.Icon));
  }
}