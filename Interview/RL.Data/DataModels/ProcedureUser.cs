using RL.Data.DataModels.Common;
using System;

namespace RL.Data.DataModels;
public class ProcedureUser : IChangeTrackable
{
	public int UserId { get; set; } 
	public int ProcedureId { get; set; } 
    public int PlanId { get; set; } 
    public DateTime CreateDate { get; set; }
	public DateTime UpdateDate { get; set; }

}