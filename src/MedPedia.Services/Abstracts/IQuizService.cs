using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Quiz;
using MedPedia.Entities.Dto.QuizTypeWithPercentage;

namespace MedPedia.Services.Abstracts;

public interface IQuizService
{
    Task<IListResult<List<QuizTypeWithPercentageDto>>> GetAllQuizTypeWithPercentage(CurrentUserDto currentUser,
        CancellationToken cancellationToken);

    Task<IListResult<List<QuizCategoryDto>>> GetAllQuizCategories(int quizTypeId, CancellationToken cancellationToken);

    Task<IListResult<List<QuizDto>>> GetAllQuiz(int quizCategoryId,CurrentUserDto currentUser, CancellationToken cancellationToken);

    Task<IListResult<List<QuestionDto>>> GetQuestionsByQuizId(int quizId, CancellationToken cancellationToken);

    Task<IResult> UserQuizAdd(UserQuizAddDto userQuizaddDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken);

    Task<IListResult<List<UserQuizHistoryDto>>> GetUserQuizHistory(CurrentUserDto currentUser,
        CancellationToken cancellationToken);

    Task<IResult> AddQuizType(AddQuizTypeDto addQuizTypeDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken);

    Task<IListResult<List<QuizTypeDto>>> GetAllQuizType(
        CancellationToken cancellationToken);

    Task<IResult> AddQuizCategory(AddQuizCategoryDto addQuizCategoryDto,CurrentUserDto currentUser,
        CancellationToken cancellationToken);

    Task<IResult> AddQuiz(AddQuizDto addQuizDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken);

    Task<IResult> AddQuestion(AddQuestionDto addQuestionDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken);


}