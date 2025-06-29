using AutoMapper;
using WebApiProject.DataTransfers.Requests;
using WebApiProject.DataTransfers.Responses;
using WebApiProject.Entities;

namespace WebApiProject.Mappings;

public class CreateHouseholdRequestMapping : Profile
{
    public CreateHouseholdRequestMapping()
    {
        CreateMap<CreateHouseholdRequest, Household>();
    }
}