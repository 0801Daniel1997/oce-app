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
    public class SelectedListCommandHandler : IRequestHandler<SelectedListCommand, ApiResponse<Unit>>
    {
        private readonly RLContext _context;

        public SelectedListCommandHandler(RLContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<Unit>> Handle(SelectedListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.PlanId < 1)
                    return ApiResponse<Unit>.Fail(new BadRequestException("Invalid PlanId"));
                if (request.ProcedureId < 1)
                    return ApiResponse<Unit>.Fail(new BadRequestException("Invalid ProcedureId"));
                if (request.UserId < 1)
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


                bool exists = _context.SelectedList.Any(sl =>
                    sl.UserId == request.UserId &&
                    sl.ProcedureId == request.ProcedureId &&
                    sl.PlanId == request.PlanId
                );

                if (!exists)
                {
                    var selectedList = new SelectedList
                    {
                        UserId = request.UserId,
                        ProcedureId = request.ProcedureId,
                        PlanId = request.PlanId,
                        IsChecked = true,
                        Status = "Logged",
                        CreateDate = DateTime.UtcNow,
                        UpdateDate = DateTime.UtcNow
                    };

                    _context.SelectedList.Add(selectedList);
                }
                else
                {
                    var selectedList = _context.SelectedList.First(sl =>
                        sl.UserId == request.UserId &&
                        sl.ProcedureId ==request.ProcedureId &&
                        sl.PlanId == request.PlanId
                    );

                 
                    selectedList.IsChecked = request.IsChecked;
                    selectedList.Status = "Logged";
                    selectedList.UpdateDate = DateTime.UtcNow;

                    _context.SelectedList.Update(selectedList);
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