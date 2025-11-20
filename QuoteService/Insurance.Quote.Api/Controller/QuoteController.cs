using AutoMapper;
using Insurance.Quote.Api.Filters;
using Insurance.Quote.Api.Request;
using Insurance.Quote.Api.Response;
using Insurance.Quote.Application.Commands;
using Insurance.Quote.Application.Handlers;
using Microsoft.AspNetCore.Mvc;
using Quote.Application.Handlers;

namespace Quote.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(BusinessExceptionFilter))]
public class QuoteController(
    CreateQuoteHandler createHandler,
    ChangeQuoteStatusHandler changeStatusHandler,
    ListQuotesHandler listHandler,
    GetQuoteByIdHandler getByIdHandler,
    IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<QuoteResponse>> Create(
        [FromBody] CreateQuoteRequest request,
        CancellationToken cancellationToken)
    {
        var quote = await createHandler.HandleAsync(mapper.Map<CreateQuoteCommand>(request), cancellationToken);

        var response = mapper.Map<QuoteResponse>(quote);
        return CreatedAtAction(nameof(GetById), new { id = response.QuoteId }, response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuoteResponse>>> List(
        CancellationToken cancellationToken)
    {
        var quotes = await listHandler.HandleAsync(cancellationToken);
        var response = mapper.Map<List<QuoteResponse>>(quotes);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<QuoteResponse>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var quote = await getByIdHandler.HandleAsync(id, cancellationToken);
        if (quote is null)
            return NotFound();

        var response = mapper.Map<QuoteResponse>(quote);
        return Ok(response);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(
        Guid id,
        [FromBody] ChangeQuoteStatusRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            await changeStatusHandler.HandleAsync(id, request.Status, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
