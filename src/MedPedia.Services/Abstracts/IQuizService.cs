using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto.Quiz;
using MedPedia.Entities.Dto.QuizTypeWithPercentage;

namespace MedPedia.Services.Abstracts;

public interface IQuizService
{
    Task<IListResult<List<QuizTypeWithPercentageDto>>> GetAllQuizTypeWithPercentage(CancellationToken cancellationToken);

    Task<IListResult<List<QuizCategoryDto>>> GetAllQuizCategories(int quizTypeId,CancellationToken cancellationToken);
    Task<IListResult<List<QuizDto>>> GetAllQuiz(int quizCategoryId,CancellationToken cancellationToken);

    Task<IListResult<List<QuestionDto>>> GetQuestionsByQuizId(int quizId, CancellationToken cancellationToken);
    
    Task<IResult> UserQuizAdd(UserQuizAddDto userQuizaddDto, CancellationToken cancellationToken);
    
    Task<IListResult<List<UserQuizHistoryDto>>> GetUserQuizHistory(int userId, CancellationToken cancellationToken);
    
}