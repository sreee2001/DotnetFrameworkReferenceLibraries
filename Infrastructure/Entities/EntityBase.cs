using Infrastructure.Base;
using System;

namespace Infrastructure.Entities
{
    #region Interfaces

    public interface IEntityBase
    {
        int Id { get; set; }
    }

    public interface IEntityBaseWithName : IEntityBase
    {
        string Name { get; set; }
    }

    public interface IAuditableInformation
    {
        DateTime CreatedOn { get; set; }
        DateTime? ModifiedOn { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }

    #endregion

    #region POCO classes for Database opereations

    public abstract class PocoEntityBase : IEntityBase
    {
        public int Id { get; set; }
        // Additional properties or methods can be added here if needed
    }

    public abstract class PocoEntityBaseWithName : PocoEntityBase, IEntityBaseWithName
    {
        public string Name { get; set; }
        // Additional properties or methods can be added here if needed
    }

    public abstract class PocoAuditableEntityBase : PocoEntityBaseWithName, IAuditableInformation
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }

    #endregion

    #region Entity classes for application domain with change tracking and validation and notification
    
    public abstract class EntityBase : PropertyChangedBase, IEntityBase
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }
    }

    public abstract class EntityBaseWithName : EntityBase, IEntityBaseWithName
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }
    }

    public abstract class AuditableEntityBase : EntityBaseWithName, IAuditableInformation
    {
        private DateTime _createdOn;
        private DateTime? _modifiedOn;
        private string _createdBy;
        private string _modifiedBy;
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
    }
    #endregion
}
