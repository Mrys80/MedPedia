using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Session;


public class SessionAddDto : IDto
{
    public int RegUserId { get; set; }
}