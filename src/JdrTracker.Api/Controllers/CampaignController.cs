using JdrTracker.Application.Campaigns.Commands.CreateCampaign;
using JdrTracker.Application.Campaigns.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace JdrTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // Définit la route de base
public class CampaignController : ControllerBase
{
    private readonly IMediator _mediator;

    public CampaignController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //POST api/campaigns : crée une campagne, le créateur en devient automatiquement MJ
    //Le controller ne fait que transmettre la command à MediatR, toute la logique est dans le handler
    [HttpPost]
    public async Task<ActionResult<CampaignDto>> Create(
    CreateCampaignCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
