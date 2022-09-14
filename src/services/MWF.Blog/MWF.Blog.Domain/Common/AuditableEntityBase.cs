namespace MWF.Blog.Domain.Common;
#nullable enable
public abstract class AuditableEntityBase<Tid> : EntityBase<Tid>
{
    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}
