using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Results;
using MedPedia.Common.Utilities.Security.Jwt;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Quiz;
using MedPedia.Entities.Dto.QuizTypeWithPercentage;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using Teb.Data.Abstracts;
using UserQuizHistoryDto = MedPedia.Entities.Dto.Quiz.UserQuizHistoryDto;

namespace MedPedia.Services.Concretes;

public class QuizService : IQuizService, IScopedDependency
{
    private readonly IRepository<QuizType> _quizTypeRepository;
    private readonly IRepository<QuizCategory> _quizCategoryRepository;
    private readonly IRepository<UserQuiz> _userQuizRepository;
    private readonly IRepository<Quiz> _quizRepository;
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<Answer> _answerRepository;

    public QuizService(IRepository<Quiz> quizRepository, IRepository<QuizType> quizTypeRepository,
        IRepository<UserQuiz> userQuizRepository, IRepository<QuizCategory> quizCategoryRepository,
        IRepository<Question> questionRepository, IRepository<Answer> answerRepository
    )
    {
        _quizRepository = quizRepository;
        _quizTypeRepository = quizTypeRepository;
        _userQuizRepository = userQuizRepository;
        _quizCategoryRepository = quizCategoryRepository;
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
    }


    public async Task<IListResult<List<QuizTypeWithPercentageDto>>> GetAllQuizTypeWithPercentage(
        CurrentUserDto currentUser, CancellationToken cancellationToken)
    {
        var userQuizData = await (from qt in _quizTypeRepository.TableNoTracking
            join qc in _quizCategoryRepository.TableNoTracking on qt.Id equals qc.QuizTypeId
            join q in _quizRepository.TableNoTracking on qc.Id equals q.QuizCategoryId
            join uq in _userQuizRepository.TableNoTracking on q.Id equals uq.QuizId into uqGroup
            from uq in uqGroup.Where(u => u.UserId == currentUser.UserId).DefaultIfEmpty()
            group uq by new { qt.Id, qt.Title }
            into g
            select new
            {
                QuizTypeId = g.Key.Id,
                QuizTypeTitle = g.Key.Title,
                TotalQuestions = g.Sum(uq => (uq != null) ? (uq.CorrectCount + uq.MistakeCount) : 0),
                CorrectQuestions = g.Sum(uq => (uq != null) ? uq.CorrectCount : 0)
            }).ToListAsync(cancellationToken);

        var result = userQuizData.Select(data => new QuizTypeWithPercentageDto
        {
            Id = data.QuizTypeId,
            Title = data.QuizTypeTitle,
            Percentage = data.TotalQuestions > 0 ? (double)data.CorrectQuestions / data.TotalQuestions * 100 : 0
        }).ToList();

        return new SuccessListResult<List<QuizTypeWithPercentageDto>>(result);
    }

    public async Task<IListResult<List<QuizCategoryDto>>> GetAllQuizCategories(int quizTypeId,
        CancellationToken cancellationToken)
    {
        var categories = await
            _quizCategoryRepository.GetListAsync(
                x =>
                    x.IsDeleted == false &&
                    x.QuizTypeId == quizTypeId,
                cancellationToken);

        var categoriesDtos = categories.Adapt<IEnumerable<QuizCategoryDto>>().ToList();

        return new SuccessListResult<List<QuizCategoryDto>>(categoriesDtos);
    }

    public async Task<IListResult<List<QuizDto>>> GetAllQuiz(int quizCategoryId, CancellationToken cancellationToken)
    {
        var quizDtos = await (
                from q in _quizRepository.TableNoTracking
                join uq in _userQuizRepository.TableNoTracking on q.Id equals uq.QuizId into gj
                from uq in gj.DefaultIfEmpty()
                where q.QuizCategoryId == quizCategoryId
                select new QuizDto
                {
                    Id = q.Id,
                    Title = q.Title,
                    IsDone = uq != null
                }
            )
            .Distinct()
            .ToListAsync(cancellationToken);

        return new SuccessListResult<List<QuizDto>>(quizDtos);
    }

    public async Task<IListResult<List<QuestionDto>>> GetQuestionsByQuizId(int quizId,
        CancellationToken cancellationToken)
    {
        var questions = await _questionRepository.TableNoTracking
            .Where(q => q.QuizId == quizId)
            .Select(q => new
            {
                q.Id,
                q.Title
            })
            .Distinct()
            .ToListAsync(cancellationToken);

        var questionIds = questions.Select(q => q.Id).ToList();
        var answers = await _answerRepository.TableNoTracking
            .Where(a => questionIds.Contains(a.QuestionId))
            .Select(a => new
            {
                a.Id,
                a.QuestionId,
                a.Title,
                a.IsCorrect,
                a.Hint
            })
            .ToListAsync(cancellationToken);

        var questionDtos = questions
            .Select(q => new QuestionDto
            {
                Id = q.Id,
                Question = q.Title,
                Answers = answers
                    .Where(a => a.QuestionId == q.Id)
                    .Select(a => new AnswerDto
                    {
                        Id = a.Id,
                        Title = a.Title,
                        IsCorrect = a.IsCorrect,
                        Hint = a.Hint
                    })
                    .ToList()
            })
            .ToList();

        return new SuccessListResult<List<QuestionDto>>(questionDtos);
    }


    public async Task<IResult> UserQuizAdd(UserQuizAddDto userQuizAddDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var userQuizEntity = new UserQuiz
        {
            QuizId = userQuizAddDto.QuizId,
            CorrectCount = userQuizAddDto.CorrectCount,
            MistakeCount = userQuizAddDto.MistakeCount,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _userQuizRepository.AddAsync(userQuizEntity, cancellationToken);

        return new SuccessResult(Messages.SuccessFully);
    }

    public async Task<IListResult<List<UserQuizHistoryDto>>> GetUserQuizHistory(CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var userQuizHistories = await (
            from uq in _userQuizRepository.TableNoTracking
            join q in _quizRepository.TableNoTracking on uq.QuizId equals q.Id
            join qc in _quizCategoryRepository.TableNoTracking on q.QuizCategoryId equals qc.Id
            join qt in _quizTypeRepository.TableNoTracking on qc.QuizTypeId equals qt.Id
            where uq.UserId == currentUser.UserId
            select new UserQuizHistoryDto
            {
                Id = uq.Id,
                QuizCategoryTitle = qc.Title,
                QuizTypeTitle = qt.Title,
                QuizTitle = q.Title,
                CorrectCount = uq.CorrectCount,
                MistakeCount = uq.MistakeCount,
                RegDateTime = uq.RegDate
            }
        ).ToListAsync(cancellationToken);

        return new SuccessListResult<List<UserQuizHistoryDto>>(userQuizHistories);
    }
}