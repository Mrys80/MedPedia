using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.TherapeuticClassification;

public class TherapeuticClassificationGetDto:IDto
{
    public int Id { get; set; }
    public string Title { get; set; }
}