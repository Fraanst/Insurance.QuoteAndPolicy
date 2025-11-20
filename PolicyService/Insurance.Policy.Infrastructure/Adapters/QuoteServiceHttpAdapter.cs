using Insurance.Policy.Domain.Dto;
using Insurance.Policy.Domain.Exceptions;
using Insurance.Policy.Domain.Interfaces.Ports;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Insurance.Policy.Infrastructure.Adapters
{
    public class QuoteServiceHttpAdapter : IQuoteServicePort
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<QuoteServiceHttpAdapter> _logger;

        public QuoteServiceHttpAdapter(HttpClient httpClient, ILogger<QuoteServiceHttpAdapter> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<QuoteDto> GetQuoteAsync(Guid quoteId, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"api/quote/{quoteId}", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Erro ao buscar proposta código: {response.StatusCode}");
                throw new QuoteNotFoundException(response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var quoteDto = JsonSerializer.Deserialize<QuoteDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (quoteDto == null)
            {
                _logger.LogError($"Falha ao deserializar o DTO da Proposta");
                throw new PolicyException("Ocorreu um erro interno ao processar a resposta do serviço de cotação.", 502);
            }

            return quoteDto;
        }
    }
}
