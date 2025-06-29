using AutoMapper;
using WebApiProject.DataTransfers.Requests;
using WebApiProject.Entities;
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