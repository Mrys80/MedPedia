using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Drug;

public class KeyValueDto:IDto
{
    public required string Key { get; set; }
    public required string Value { get; set; }
}