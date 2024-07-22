using MedPedia.Common.Entities.Abstracts;
using MedPedia.Entities.Entities;

namespace MedPedia.Entities.Dto.Quiz;

public class AddAnswerDto:IDto
{
    public int QuestionId { get; set; }
    List<AnswerDto> Answers { get; set; }
}