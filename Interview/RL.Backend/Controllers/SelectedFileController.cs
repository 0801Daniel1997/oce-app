using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RL.Data;
using RL.Data.DataModels;
using RL.Backend.Commands;
using RL.Backend.Commands.Handlers.Plans;
using MediatR;
using RL.Backend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
namespace RL.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class SelectedListController : ControllerBase
{
    private readonly ILogger<SelectedListController> _logger;
    private readonly RLContext _context;
    private readonly IMediator _mediator;
    public SelectedListController(ILogger<SelectedListController> logger, RLContext context, IMediator mediator)
    {
        _logger = logger;
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [EnableQuery]
    public IEnumerable<SelectedList> Get(int planId, int procedureId)
    {
        return _context.SelectedList
                       .Where(sl => sl.PlanId == planId && sl.ProcedureId == procedureId)
                       .ToList();
    }
    [HttpPost("ModifySelectedList")]
    public async Task<IActionResult> ModifySelectedList(SelectedListCommand command, CancellationToken token)
    {
        var response = await _mediator.Send(command, token);

        return response.ToActionResult();
    }
}
