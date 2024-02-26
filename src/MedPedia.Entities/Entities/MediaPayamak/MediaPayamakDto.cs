using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Entities.MediaPayamak;

public class MediaPayamakDto:IDto
{
    public int BatchSmsId{ get; set; }
    public int Result{ get; set; }
    public string ErrorMessage { get; set; }
}