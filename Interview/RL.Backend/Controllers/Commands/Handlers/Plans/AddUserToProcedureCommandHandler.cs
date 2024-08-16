using MediatR;
using Microsoft.EntityFrameworkCore;
using RL.Backend.Exceptions;
using RL.Backend.Models;
using RL.Data;
using RL.Data.DataModels;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

namespace RL.Backend.Commands.Handlers.Plans
{
    public class AddUserToProcedureCommandHandler : IRequestHandler<AddUserToProcedureCommand, ApiResponse<Unit>>
    {
        private readonly RLContext _context;

        public AddUserToProcedureCommandHandler(RLContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<Unit>> Handle(AddUserToProcedureCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.PlanId < 1)
                    return ApiResponse<Unit>.Fail(new BadRequestException("Invalid PlanId"));
                if (request.ProcedureId < 1)
                    return ApiResponse<Unit>.Fail(new BadRequestException("Invalid ProcedureId"));
                if (request.UserIds.Count < 1)
                    return ApiResponse<Unit>.Fail(new BadRequestException("Select at least 1 user"));

                var plan = await _context.Plans
                    .Include(p => p.PlanProcedures)
                    .FirstOrDefaultAsync(p => p.PlanId == request.PlanId, cancellationToken);

                var procedure = await _context.Procedures
                    .Include(b => b.ProcedureUsers)
                    .FirstOrDefaultAsync(p => p.ProcedureId == request.ProcedureId, cancellationToken);

                if (plan == null)
                    return ApiResponse<Unit>.Fail(new NotFoundException($"PlanId: {request.PlanId} not found"));
                if (procedure == null)
                    return ApiResponse<Unit>.Fail(new NotFoundException($"ProcedureId: {request.ProcedureId} not found"));

                var users = await _context.Users.ToListAsync(cancellationToken);


                foreach (var userId in request.UserIds)
                {
                 
                    bool exists = _context.ProcedureUsers.Any(pu =>
                        pu.UserId == userId &&
                        pu.ProcedureId == procedure.ProcedureId &&
                        pu.PlanId == plan.PlanId
                    );

                 
                    if (!exists)
                    {
                        var procedureUser = new ProcedureUser
                        {
                            UserId = userId,
                            ProcedureId = procedure.ProcedureId,
                            PlanId = plan.PlanId,
                            CreateDate = DateTime.UtcNow,
                            UpdateDate = DateTime.UtcNow
                        };

                        _context.ProcedureUsers.Add(procedureUser);
                    }
                }




                await _context.SaveChangesAsync();
                return ApiResponse<Unit>.Succeed(Unit.Value);
            }
            catch (Exception e)
            {
                return ApiResponse<Unit>.Fail(e);
            }
        }
    }
}