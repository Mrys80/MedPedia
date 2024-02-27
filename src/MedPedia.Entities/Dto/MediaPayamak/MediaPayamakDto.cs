using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.MediaPayamak;

public class MediaPayamakDto:IDto
{
    public int BatchSmsId{ get; set; }
    public int Result{ get; set; }
    public string ErrorMessage { get; set; }
}