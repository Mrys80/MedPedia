using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class Province : BaseEntity
{
    public int Id { get; set; }
    
    public string ProvinceName { get; set; }
}