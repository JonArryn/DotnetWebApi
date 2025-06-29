using AutoMapper;
using WebApiProject.DataTransfers.Responses;
using WebApiProject.Entities;

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