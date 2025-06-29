using AutoMapper;
using WebApiProject.DataTransfers.Responses;
using WebApiProject.Entities;

namespace WebApiProject.Mappings;

public class CreateHouseholdResponseMapping : Profile
{
    public CreateHouseholdResponseMapping()
    {
        CreateMap<Household, CreateHouseholdResponse>();
            
    }
}