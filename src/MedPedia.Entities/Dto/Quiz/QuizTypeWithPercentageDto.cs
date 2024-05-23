using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.QuizTypeWithPercentage;

public class QuizTypeWithPercentageDto : IDto
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public double Percentage { get; set; }
}