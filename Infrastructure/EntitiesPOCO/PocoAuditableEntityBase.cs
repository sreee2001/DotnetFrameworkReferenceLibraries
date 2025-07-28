using Infrastructure.Interfaces;
using System;

namespace Infrastructure.EntitiesPOCO
{
    #region POCO classes for Database opereations
    public abstract class PocoAuditableEntityBase : PocoEntityBaseWithName, IAuditableInformation
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
    #endregion
}
