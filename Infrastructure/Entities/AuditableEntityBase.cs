using Infrastructure.Interfaces;
using System;

namespace Infrastructure.Entities
{
    public abstract class AuditableEntityBase : EntityBaseWithName, IAmAuditable
    {
        private DateTime _createdOn;
        private DateTime? _modifiedOn;
        private string _createdBy;
        private string _modifiedBy;

        protected AuditableEntityBase()
        {
            SetAuditInfo();
        }

        public DateTime CreatedOn
        {
            get => _createdOn;
            set => SetField(ref _createdOn, value);
        }
        public DateTime? ModifiedOn
        {
            get => _modifiedOn;
            set => SetField(ref _modifiedOn, value);
        }
        public string CreatedBy
        {
            get => _createdBy;
            set => SetField(ref _createdBy, value);
        }
        public string ModifiedBy
        {
            get => _modifiedBy;
            set => SetField(ref _modifiedBy, value);
        }

        public void SetAuditInfo()
        {
            if (Id == 0)
            {
                CreatedBy = Environment.UserName; // This can be replaced with actual user information
                CreatedOn = DateTime.UtcNow;
            }
            ModifiedOn = DateTime.Now;
            ModifiedBy = Environment.UserName;
        }

    }
}
