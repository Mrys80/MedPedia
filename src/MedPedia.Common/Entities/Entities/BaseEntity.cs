using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Common.Entities.Entities;

public abstract class BaseEntity<TKey> : IEntity
{
    public TKey Id { get; set; }
    public DateTime RegDate { get; set; }
    public int RegUserId { get; set; }
    public bool IsDeleted { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>
{
}