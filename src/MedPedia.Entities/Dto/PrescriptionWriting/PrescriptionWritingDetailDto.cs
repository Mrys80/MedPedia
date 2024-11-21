using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.PrescriptionWriting;

public class PrescriptionWritingDetailDto : IDto
{
    public int Id { get; set; }
    
    public string FaTitle { get; set; }
    
    public string EnTitle { get; set; }
    
    public string? ImageId { get; set; }
    
    public List<int> DrugIds { get; set; }
    
    public int DiseaseIds { get; set; }
    
    public string Description { get; set; }
}