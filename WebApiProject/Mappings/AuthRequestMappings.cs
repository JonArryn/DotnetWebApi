using AutoMapper;
using WebApiProject.Entities;
using WebApiProject.Models.Requests;
using WebApiProject.Services;

namespace WebApiProject.Mappings;

public class RegisterRequestMapping : Profile
{
    public RegisterRequestMapping()
    {
        CreateMap<RegisterRequest, UserEntity>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
    }
}