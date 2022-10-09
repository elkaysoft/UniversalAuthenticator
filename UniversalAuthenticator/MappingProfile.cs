using AutoMapper;
using UniversalAuthenticator.Common.Models.DTO;
using UniversalAuthenticator.Domain.Entities;

namespace UniversalAuthenticator.Api
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserOnboardingDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<SystemConfiguration, SystemConfigurationDto>();
            CreateMap<SMSTemplate, SmsTemplateDto>();
            CreateMap<EmailTemplate, EmailTemplateDto>();
        }
    }
}
