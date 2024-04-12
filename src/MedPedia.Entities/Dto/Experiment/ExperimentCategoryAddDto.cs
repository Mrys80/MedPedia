using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Experiment;

public class ExperimentCategoryAddDto : IDto
{
    public string Title { get; set; }
    public string? ImageId { get; set; }
}