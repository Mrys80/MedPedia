using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.ClassificationOfPregnancy;

public class ClassificationOfPregnancyGetDto:IDto
{
    public int Id { get; set; }
    public string Title { get; set; }

}