using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.SideEffect;

public class SideEffectAddDto : IDto
{
    public string Title { get; set; }
    public string Description { get; set; }

}