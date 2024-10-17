using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Quiz;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using Teb.Data.Abstracts;
using UserQuizHistoryDto = MedPedia.Entities.Dto.Quiz.UserQuizHistoryDto;

namespace MedPedia.Services.Concretes;

public class QuizService : IQuizService, IScopedDependency
{
    private readonly IRepository<Lessons> _lessonRepository;
    private readonly IRepository<QuizCategory> _quizCategoryRepository;
    private readonly IRepository<UserQuiz> _userQuizRepository;
    private readonly IRepository<Quiz> _quizRepository;
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<Answer> _answerRepository;

    public QuizService(IRepository<Quiz> quizRepository, IRepository<Lessons> lessonRepository,
        IRepository<UserQuiz> userQuizRepository, IRepository<QuizCategory> quizCategoryRepository,
        IRepository<Question> questionRepository, IRepository<Answer> answerRepository
    )
    {
        _quizRepository = quizRepository;
        _lessonRepository = lessonRepository;
        _userQuizRepository = userQuizRepository;
        _quizCategoryRepository = quizCategoryRepository;
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
    }


    public async Task<IListResult<List<LessonsWithPercentageDto>>> GetAllLessonWithPercentage(
        CurrentUserDto currentUser, CancellationToken cancellationToken)
    {
        var query = from lessons in _lessonRepository.TableNoTracking
            join quizCategory in _quizCategoryRepository.TableNoTracking on lessons.Id equals quizCategory.LessonId
            join quiz in _quizRepository.TableNoTracking on quizCategory.Id equals quiz.QuizCategoryId into quizzesGroup
            from quiz in quizzesGroup.DefaultIfEmpty()
            join userQuiz in _userQuizRepository.TableNoTracking.Where(uq => uq.UserId == currentUser.UserId) on quiz.Id
                equals userQuiz.QuizId into userQuizzesGroup
            from userQuiz in userQuizzesGroup.DefaultIfEmpty()
            group new { lessons, quiz, userQuiz } by lessons
            into g
            select new
            {
                LessonId = g.Key.Id,
                LessonTitle = g.Key.Title,
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
            .Select(q => new LessonsWithPercentageDto
            {
                Id = q.LessonId,
                Title = q.LessonTitle,
                Percentage = q.TotalCount == 0 ? 0 : (double)q.CompletedCount / q.TotalCount * 100
            })
            .OrderByDescending(q => q.Percentage)
            .Take(4)
            .ToList(), cancellationToken);

        return new SuccessListResult<List<LessonsWithPercentageDto>>(result);
    }


    public async Task<IListResult<List<QuizCategoryDto>>> GetAllQuizCategories(int lessonId,
        CurrentUserDto currentUser, CancellationToken cancellationToken)
    {
        var query = from quizCategory in _quizCategoryRepository.TableNoTracking
            where quizCategory.LessonId == lessonId
            join quiz in _quizRepository.TableNoTracking on quizCategory.Id equals quiz.QuizCategoryId into quizzesGroup
            from quiz in quizzesGroup.DefaultIfEmpty()
            join userQuiz in _userQuizRepository.TableNoTracking.Where(uq => uq.UserId == currentUser.UserId)
                on quiz.Id equals userQuiz.QuizId into userQuizzesGroup
            from userQuiz in userQuizzesGroup.DefaultIfEmpty()
            group new { quizCategory, quiz, userQuiz } by quizCategory
            into g
            select new
            {
                CategoryId = g.Key.Id,
                CategoryTitle = g.Key.Title,
                TotalQuizzes = g.Where(q => q.quiz != null)
                    .Select(q => q.quiz.Id)
                    .Distinct()
                    .Count(),
                CompletedQuizzes = g.Where(q => q.userQuiz != null)
                    .Select(q => q.userQuiz.QuizId)
                    .Distinct()
                    .Count()
            };

        var queryResult = await query.ToListAsync(cancellationToken);

        var result = queryResult.Select(q => new QuizCategoryDto
        {
            Id = q.CategoryId,
            Title = q.CategoryTitle,
            Percentage = q.TotalQuizzes > 0
                ? ((double)q.CompletedQuizzes / q.TotalQuizzes * 100).ToString("0.##") + "%"
                : "0%"
        }).ToList();

        return new SuccessListResult<List<QuizCategoryDto>>(result);
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


        var quizQuestionCounts = await (
            from question in _questionRepository.TableNoTracking
            group question by question.QuizId
            into quizGroup
            select new
            {
                QuizId = quizGroup.Key,
                QuestionCount = quizGroup.Count()
            }
        ).ToListAsync(cancellationToken);

        var questionCountDict = quizQuestionCounts.ToDictionary(q => q.QuizId, q => q.QuestionCount);


        var quizDtos = quizzes.Select(q => new QuizDto
        {
            Id = q.Id,
            Title = q.Title,
            IsDone = userQuizIds.Contains(q.Id),
            QuestionCount = questionCountDict.TryGetValue(q.Id, out var value) ? value : 0
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
        var quizQuestionCounts = await (
            from question in _questionRepository.TableNoTracking
            group question by question.QuizId
            into quizGroup
            select new
            {
                QuizId = quizGroup.Key,
                QuestionCount = quizGroup.Count()
            }
        ).ToListAsync(cancellationToken);

        var questionCountDict = quizQuestionCounts.ToDictionary(q => q.QuizId, q => q.QuestionCount);

        var userQuizHistories = await (
            from uq in _userQuizRepository.TableNoTracking
            join q in _quizRepository.TableNoTracking on uq.QuizId equals q.Id
            join qc in _quizCategoryRepository.TableNoTracking on q.QuizCategoryId equals qc.Id
            join qt in _lessonRepository.TableNoTracking on qc.LessonId equals qt.Id
            where uq.UserId == currentUser.UserId
            select new
            {
                uq.Id,
                QuizCategoryTitle = qc.Title,
                LessonTitle = qt.Title,
                QuizTitle = q.Title,
                uq.CorrectCount,
                uq.MistakeCount,
                RegDateTime = uq.RegDate,
                QuizId = q.Id
            }
        ).ToListAsync(cancellationToken);

        var userQuizHistoryDtos = userQuizHistories.Select(uq => new UserQuizHistoryDto
        {
            Id = uq.Id,
            QuizId = uq.QuizId,
            QuizCategoryTitle = uq.QuizCategoryTitle,
            LessonTitle = uq.LessonTitle,
            QuizTitle = uq.QuizTitle,
            CorrectCount = uq.CorrectCount,
            MistakeCount = uq.MistakeCount,
            RegDateTime = uq.RegDateTime,
            QuestionCount = questionCountDict.TryGetValue(uq.QuizId, out var questionCount) ? questionCount : 0
        }).ToList();

        return new SuccessListResult<List<UserQuizHistoryDto>>(userQuizHistoryDtos);
    }


    public async Task<IResult> AddLesson(AddLessonDto addLessonDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var lessons = new Lessons
        {
            Title = addLessonDto.Title,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await _lessonRepository.AddAsync(lessons, cancellationToken);

        return new SuccessResult(Messages.SuccessFully);
    }

    public async Task<IListResult<List<LessonsDto>>> GetAllLesson(CancellationToken cancellationToken)
    {
        var lessons = await _lessonRepository.GetListAsync(x => x.IsDeleted == false, cancellationToken);

        var lessonsDtos = lessons.Adapt<List<LessonsDto>>();

        return new SuccessListResult<List<LessonsDto>>(lessonsDtos);
    }

    public async Task<IResult> AddQuizCategory(AddQuizCategoryDto addQuizCategoryDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var quizCategory = new QuizCategory
        {
            Title = addQuizCategoryDto.Title,
            LessonId = addQuizCategoryDto.LessonId,
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