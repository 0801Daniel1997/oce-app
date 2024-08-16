using MediatR;
using RL.Backend.Models;
using System;
using System.Collections.Generic; // Add this using directive

namespace RL.Backend.Commands
{
    public class SelectedListCommand : IRequest<ApiResponse<Unit>>
    {
        public int PlanId { get; set; }
        public int ProcedureId { get; set; }
        public int UserId { get; set; }
        public bool IsChecked { get; set; }
        public string Status { get; set; }
    }
}