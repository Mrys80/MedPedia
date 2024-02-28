using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class DrugSideEffect:BaseEntity
{
    public int DrugId { get; set; }
    public int SideEffectId { get; set; }

}