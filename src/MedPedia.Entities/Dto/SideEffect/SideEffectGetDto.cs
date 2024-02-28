using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.SideEffect;

public class SideEffectGetDto : IDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}