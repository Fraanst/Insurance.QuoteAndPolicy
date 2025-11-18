using AutoMapper;
using Insurance.Policy.Application.Commands;
using Insurance.Policy.Domain.Entities;

namespace Insurance.Policy.Api.Mappers
{
    public class PolicyMappingProfile : Profile
    {
        public PolicyMappingProfile()
        {
            CreateMap<ContractQuoteRequest, ContractQuoteCommand>();

            CreateMap<PolicyEntity, PolicyResponse>();
        }
    }
}
