using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Image;

public class ImageDto:IDto
{
    public byte[] Image { get; set; }
}