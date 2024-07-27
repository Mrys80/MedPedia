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
using Teb.Data.Concretes.Context;
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
        var query = from quizType in _quizTypeRepository.TableNoTracking
            join quizCategory in _quizCategoryRepository.TableNoTracking on quizType.Id equals quizCategory.QuizTypeId
            join quiz in _quizRepository.TableNoTracking on quizCategory.Id equals quiz.QuizCategoryId into quizzesGroup
            from quiz in quizzesGroup.DefaultIfEmpty()
            join userQuiz in _userQuizRepository.TableNoTracking.Where(uq => uq.UserId == currentUser.UserId) on quiz.Id
                equals userQuiz.QuizId into userQuizzesGroup
            from userQuiz in userQuizzesGroup.DefaultIfEmpty()
            group new { quizType, quiz, userQuiz } by quizType
            into g
            select new
            {
                QuizTypeId = g.Key.Id,
                QuizTypeTitle = g.Key.Title,
                TotalCount = g.Where(q => q.quiz != null)
                    .Select(q => q.quiz.Id)
                    .Distinct()
                    .Count(),
                CompletedCount = g.Where(q => q.userQuiz != null)
                    .Select(q => q.userQuiz.QuizId)
                    .Distinct()
                    .Count()
            };

        var result = await Task.Run(() => query
            .Select(q => new QuizTypeWithPercentageDto
            {
                Id = q.QuizTypeId,
                Title = q.QuizTypeTitle,
                Percentage = q.TotalCount == 0 ? 0 : (double)q.CompletedCount / q.TotalCount * 100
            })
            .ToList(), cancellationToken);

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

    public async Task<IListResult<List<QuizDto>>> GetAllQuiz(int quizCategoryId, CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var quizzes = await (
            from q in _quizRepository.TableNoTracking
            where q.QuizCategoryId == quizCategoryId
            select q
        ).ToListAsync(cancellationToken);

        var userQuizIds = await (
            from uq in _userQuizRepository.TableNoTracking
            where uq.UserId == currentUser.UserId
            select uq.QuizId
        ).Distinct().ToListAsync(cancellationToken);

        var quizDtos = quizzes.Select(q => new QuizDto
        {
            Id = q.Id,
            Title = q.Title,
            IsDone = userQuizIds.Contains(q.Id)
        }).ToList();

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
            UserId = currentUser.UserId,
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

    public async Task<IResult> AddQuizType(AddQuizTypeDto addQuizTypeDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var quizType = new QuizType
        {
            Title = addQuizTypeDto.Title,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _quizTypeRepository.AddAsync(quizType, cancellationToken);

        return new SuccessResult(Messages.SuccessFully);
    }

    public async Task<IListResult<List<QuizTypeDto>>> GetAllQuizType(CancellationToken cancellationToken)
    {
        var quizTypes = await _quizTypeRepository.GetListAsync(x => x.IsDeleted == false, cancellationToken);

        var quizTypeDtos = quizTypes.Adapt<List<QuizTypeDto>>();

        return new SuccessListResult<List<QuizTypeDto>>(quizTypeDtos);
    }

    public async Task<IResult> AddQuizCategory(AddQuizCategoryDto addQuizCategoryDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var quizCategory = new QuizCategory
        {
            Title = addQuizCategoryDto.Title,
            QuizTypeId = addQuizCategoryDto.QuizTypeId,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _quizCategoryRepository.AddAsync(quizCategory, cancellationToken);

        return new SuccessResult(Messages.SuccessFully);
    }

    public async Task<IResult> AddQuiz(AddQuizDto addQuizDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var quiz = new Quiz
        {
            Title = addQuizDto.Title,
            QuizCategoryId = addQuizDto.QuizCategoryId,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _quizRepository.AddAsync(quiz, cancellationToken);

        return new SuccessResult(Messages.SuccessFully);
    }

    public async Task<IResult> AddQuestion(AddQuestionDto addQuestionDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var question = new Question
        {
            QuizId = addQuestionDto.QuizId,
            Title = addQuestionDto.Title,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _questionRepository.AddAsync(question, cancellationToken);

        foreach (var answer in addQuestionDto.Answers.Select(answerDto => new Answer
                 {
                     QuestionId = question.Id,
                     Title = answerDto.Title,
                     IsCorrect = answerDto.IsCorrect,
                     Hint = answerDto.Hint,
                     RegUserId = currentUser.UserId,
                     RegDate = DateTime.Now,
                     IsDeleted = false
                 }))
        {
            await _answerRepository.AddAsync(answer, cancellationToken);
        }

        return new SuccessResult(Messages.SuccessFully);
    }
}