using RL.Data.DataModels.Common;
using System;

namespace RL.Data.DataModels;
public class SelectedList : IChangeTrackable
{
    public int UserId { get; set; }
    public int ProcedureId { get; set; }
    public int PlanId { get; set; }
    public bool IsChecked { get; set; }
    public string Status { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }

}