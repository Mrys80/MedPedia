using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.PharmaceuticalForms;

public class PharmaceuticalFormsGetDto:IDto
{
    public int Id { get; set; }
    public string Title { get; set; }

}