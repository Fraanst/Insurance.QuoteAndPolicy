using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Insurance.Quote.Domain.Exceptions;
using Insurance.Quote.Domain.Interfaces.Ports;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Insurance.Quote.Infrastructure.Adapters
{
    public class QuoteSnsAdapter : IQuoteNotificationPort
    {
        private readonly IAmazonSimpleNotificationService _snsClient;
        private readonly ILogger<QuoteSnsAdapter> _logger;
        public QuoteSnsAdapter(IAmazonSimpleNotificationService snsClient,
            ILogger<QuoteSnsAdapter> logger)
        {
            _snsClient = snsClient;
            _logger = logger;
        }

        public async Task NotifyQuoteApprovedAsync(Guid quoteId, CancellationToken cancellationToken)
        {
            const string topicArn = "quote-approved-topic";

            var messagePayload = new
            {
                QuoteId = quoteId,
                EventType = "QUOTE_APPROVED",
                Timestamp = DateTime.UtcNow
            };

            string jsonMessage = JsonSerializer.Serialize(messagePayload);

            var request = new PublishRequest
            {
                TopicArn = topicArn,
                Message = jsonMessage,
                Subject = $"Quote {quoteId} Aprovada"
            };

            try
            {
                await _snsClient.PublishAsync(request, cancellationToken);
                _logger.LogInformation("Mensagem da Quote {QuoteId} publicada no SNS com sucesso.", quoteId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao publicar mensagem da Quote {QuoteId} no SNS.", quoteId);
                throw new QuoteException($"Ocorreu um erro ao tentar notificar mensagem erro:{ex.Message}");
            }
        }
    }
}
