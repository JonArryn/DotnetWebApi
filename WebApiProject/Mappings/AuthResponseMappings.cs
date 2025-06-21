using AutoMapper;
using WebApiProject.Entities;
using WebApiProject.Models.Responses;

namespace WebApiProject.Mappings;

public class RegisterResponseMapping : Profile
{
    public RegisterResponseMapping()
    {
        CreateMap<UserEntity, RegisterResponse>();
    }
}

public class LoginResponseMapping : Profile
{
    public LoginResponseMapping()
    {
        CreateMap<UserEntity, LoginResponse>();
    }
}