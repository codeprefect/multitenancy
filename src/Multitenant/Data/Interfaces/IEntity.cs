using System;

namespace MultiTenant.Data.Interfaces
{
    public interface IEntity {
        object Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        byte[] Version { get; set; }
        DateTime? Deleted { get; set; }
    }

    public interface IEntity<T> : IEntity {
        new T Id { get; set; }
    }
}
