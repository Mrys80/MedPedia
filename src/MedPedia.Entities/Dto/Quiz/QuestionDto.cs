using MedPedia.Common.Entities.Abstracts;
using MedPedia.Entities.Entities;

namespace MedPedia.Entities.Dto.Quiz;

public class QuestionDto : IDto
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string Hint { get; set; }
    
    public List<AnswerDto> Answers { get; set; }
}