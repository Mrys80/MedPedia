using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class PrescriptionWriting : BaseEntity
{
    public string FaTitle { get; set; }
    
    public string EnTitle { get; set; }
    
    public string? ImageId { get; set; }

    public string DrugIds { get; set; }
    
    public int DiseaseId { get; set; }

    public string Description { get; set; }
}