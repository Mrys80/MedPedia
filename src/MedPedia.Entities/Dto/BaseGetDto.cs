using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto;

public class BaseGetDto:IDto
{
    public int Offset { get; set; }
    public int Limit { get; set; }
}