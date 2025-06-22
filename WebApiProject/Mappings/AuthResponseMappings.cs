using AutoMapper;
using WebApiProject.Entities;
using WebApiProject.Models.Responses;

namespace WebApiProject.Mappings;

public class RegisterResponseMapping : Profile
{
    public RegisterResponseMapping()
    {
        CreateMap<User, RegisterResponse>();
    }
}

public class LoginResponseMapping : Profile
{
    public LoginResponseMapping()
    {
        CreateMap<User, LoginResponse>();
    }
}