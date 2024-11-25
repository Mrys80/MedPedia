using MedPedia.Common.Entities.Abstracts;
using MedPedia.Entities.Dto.Disease;
using MedPedia.Entities.Dto.Drug;

namespace MedPedia.Entities.Dto.PrescriptionWriting;

public class PrescriptionWritingDetailDto : IDto
{
    public int Id { get; set; }

    public string FaTitle { get; set; }

    public string EnTitle { get; set; }

    public string? ImageId { get; set; }

    public List<DrugDto> Drugs { get; set; }

    public DiseaseGetDto Disease { get; set; }

    public string Description { get; set; }
}