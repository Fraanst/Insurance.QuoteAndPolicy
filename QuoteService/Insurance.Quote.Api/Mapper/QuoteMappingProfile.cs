using AutoMapper;
using Insurance.Quote.Api.Request;
using Insurance.Quote.Api.Response;
using Insurance.Quote.Application.Commands;
using Quote.Domain.Entities;

namespace Insurance.Quote.Api.Mapper
{
    public class QuoteMappingProfile : Profile
    {
        public QuoteMappingProfile()
        {
            CreateMap<CreateQuoteRequest, CreateQuoteCommand>();

            CreateMap<QuoteEntity, QuoteResponse>();
        }
    }
}
