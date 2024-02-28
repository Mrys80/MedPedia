using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.PharmacologicalClassification;

public class PharmacologicalClassificationGetDto:IDto
{
    public int Id { get; set; }
    public string Title { get; set; }
}