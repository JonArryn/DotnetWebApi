using AutoMapper;
using WebApiProject.Entities;
using WebApiProject.Models.Requests;
using WebApiProject.Services;

namespace WebApiProject.Mappings;

public class RegisterRequestMapping : Profile
{
    public RegisterRequestMapping()
    {
        CreateMap<RegisterRequest, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
    }
}