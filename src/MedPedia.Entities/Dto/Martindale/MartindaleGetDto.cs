using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Martindale;

public class MartindaleGetDto : IDto
{
    public int Id { get; set; }
    public string Title { get; set; }
}