using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RL.Data;
using RL.Data.DataModels;
using RL.Backend.Commands;
using RL.Backend.Commands.Handlers.Plans;
using MediatR;
using RL.Backend.Models;
using System;
namespace RL.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ProcedureUserController : ControllerBase
{
    private readonly ILogger<ProcedureUserController> _logger;
    private readonly RLContext _context;
    private readonly IMediator _mediator;
    public ProcedureUserController(ILogger<ProcedureUserController> logger, RLContext context, IMediator mediator)
    {
        _logger = logger;
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [EnableQuery]
    public IEnumerable<ProcedureUser> Get()
    {
        return _context.ProcedureUsers;
    }
    [HttpPost("AddUserToProcedure")]
    public async Task<IActionResult> AddUserToProcedure(AddUserToProcedureCommand command, CancellationToken token)
    {
        var response = await _mediator.Send(command, token);

        return response.ToActionResult();
    }
}
