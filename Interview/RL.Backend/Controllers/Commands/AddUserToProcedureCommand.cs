using MediatR;
using RL.Backend.Models;
using System.Collections.Generic; // Add this using directive

namespace RL.Backend.Commands
{
    public class AddUserToProcedureCommand : IRequest<ApiResponse<Unit>>
    {
        public int PlanId { get; set; }
        public int ProcedureId { get; set; }
        public List<int> UserIds { get; set; } // Change to List<int>
    }
}