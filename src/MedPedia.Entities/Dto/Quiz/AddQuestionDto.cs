using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Quiz;

public class AddQuestionDto : IDto
{
    public string Title { get; set; }
    public string Hint { get; set; }

    public int QuizId { get; set; }

    public List<AnswerDto> Answers { get; set; }
}