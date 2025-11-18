using AutoMapper;
using Insurance.Policy.Application.Commands;
using Insurance.Policy.Application.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Proposal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PolicyController(
    ContractQuoteHandler contractQuoteHandler, 
    GetPolicyByIdHandler getPolicyByIdHandler,
    IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PolicyResponse>> ContractQuote([FromBody]ContractQuoteRequest contractQuoteRequest, CancellationToken cancellationToken)
    {
        var policy = await contractQuoteHandler.HandleAsync(mapper.Map<ContractQuoteCommand>(contractQuoteRequest), cancellationToken);

        if (policy is null)
            return NotFound();

        return CreatedAtAction(nameof(GetPolicyById), new { id = policy.Id }, mapper.Map<PolicyResponse>(policy));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PolicyResponse>> GetPolicyById(Guid id, CancellationToken cancellationToken)
    {
        var policy = await getPolicyByIdHandler.HandlerAsync(id, cancellationToken);
        return Ok(mapper.Map<PolicyResponse>(policy));
    }
}