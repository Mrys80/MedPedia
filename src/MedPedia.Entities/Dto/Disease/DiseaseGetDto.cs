using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Disease;

public class DiseaseGetDto : IDto
{
    public int Id { get; set; }
    public string FaTitle { get; set; }
    public string EnTitle { get; set; }
    public string? ImageId { get; set; }
}