using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.MethodOfUse;

public class MethodOfUseGetDto:IDto
{
    public int Id { get; set; }
    public string Title { get; set; }
}