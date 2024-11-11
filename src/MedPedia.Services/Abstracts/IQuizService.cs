using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Quiz;
using LessonsWithPercentageDto = MedPedia.Entities.Dto.Quiz.LessonsWithPercentageDto;

namespace MedPedia.Services.Abstracts;

public interface IQuizService
{
    Task<IListResult<List<LessonsWithPercentageDto>>> GetAllLessonWithPercentage(CurrentUserDto currentUser,
        CancellationToken cancellationToken);

    Task<IListResult<List<QuizCategoryDto>>> GetAllQuizCategories(int lessonId,CurrentUserDto currentUser, CancellationToken cancellationToken);

    Task<IListResult<List<QuizDto>>> GetAllQuiz(QuizTypeDto quizTypeDto,CurrentUserDto currentUser, CancellationToken cancellationToken);

    Task<IListResult<List<QuestionDto>>> GetQuestionsByQuizId(int quizId, CancellationToken cancellationToken);

    Task<IResult> UserQuizAdd(UserQuizAddDto userQuizaddDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken);

    Task<IListResult<List<UserQuizHistoryDto>>> GetUserQuizHistory(CurrentUserDto currentUser,
        CancellationToken cancellationToken);

    Task<IResult> AddLesson(AddLessonDto addLessonDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken);

    Task<IListResult<List<LessonsDto>>> GetAllLesson(
        CancellationToken cancellationToken);

    Task<IResult> AddQuizCategory(AddQuizCategoryDto addQuizCategoryDto,CurrentUserDto currentUser,
        CancellationToken cancellationToken);

    Task<IResult> AddQuiz(AddQuizDto addQuizDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken);

    Task<IResult> AddQuestion(AddQuestionDto addQuestionDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken);


}