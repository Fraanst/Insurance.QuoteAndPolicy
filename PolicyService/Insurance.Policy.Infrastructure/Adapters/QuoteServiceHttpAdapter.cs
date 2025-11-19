using Insurance.Policy.Domain.Dto;
using Insurance.Policy.Domain.Interfaces.Ports;
using Microsoft.Extensions.Logging;
using System.Text.Json;

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
            try
            {
                var response = await _httpClient.GetAsync($"api/quote/{quoteId}", cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Erro ao buscar proposta código: {response.StatusCode}");
                    throw new Exception($"Falha ao buscar proposta. Código: {response.StatusCode}");
                }

                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var quoteDto = JsonSerializer.Deserialize<QuoteDto>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (quoteDto == null)
                {
                    _logger.LogError($"Falha ao deserializar o DTO da Proposta");
                    throw new Exception("Falha ao deserializar o DTO da Proposta.");
                }

                return quoteDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao tentar buscar proposta");
                throw new Exception("Ocorreu um erro ao tentar buscar proposta.");
            }
        }
    }
}
