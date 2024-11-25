using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Drug;

public class DrugDto:IDto
{
    
    public int Id { get; set; }
    public string FaTitle { get; set; }
    public string EnTitle { get; set; }
    public string Brand { get; set; }
    public string CategoryFaTitle { get; set; }
    public string CategoryEnTitle { get; set; }
}