using System;

namespace Infrastructure.Interfaces
{
    public interface IAuditableInformation
    {
        DateTime CreatedOn { get; set; }
        DateTime? ModifiedOn { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}
