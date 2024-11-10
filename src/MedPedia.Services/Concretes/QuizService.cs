using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto;
using MedPedia.Entities.Dto.Quiz;
using MedPedia.Entities.Entities;
using MedPedia.Entities.Enums;
using MedPedia.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using Teb.Data.Abstracts;
using UserQuizHistoryDto = MedPedia.Entities.Dto.Quiz.UserQuizHistoryDto;

namespace MedPedia.Services.Concretes;

public class QuizService(
    IRepository<Quiz> quizRepository,
    IRepository<Lessons> lessonRepository,
    IRepository<UserQuiz> userQuizRepository,
    IRepository<QuizCategory> quizCategoryRepository,
    IRepository<Question> questionRepository,
    IRepository<Answer> answerRepository,
    IRepository<QuestionDetail> detailRepository
)
    : IQuizService, IScopedDependency
{
    public async Task<IListResult<List<LessonsWithPercentageDto>>> GetAllLessonWithPercentage(
        CurrentUserDto currentUser, CancellationToken cancellationToken)
    {
        var query = from lessons in lessonRepository.TableNoTracking
            join quizCategory in quizCategoryRepository.TableNoTracking on lessons.Id equals quizCategory.LessonId
            join quiz in quizRepository.TableNoTracking on quizCategory.Id equals quiz.QuizCategoryId into quizzesGroup
            from quiz in quizzesGroup.DefaultIfEmpty()
            join userQuiz in userQuizRepository.TableNoTracking.Where(uq => uq.UserId == currentUser.UserId) on quiz.Id
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
        var query = from quizCategory in quizCategoryRepository.TableNoTracking
            where quizCategory.LessonId == lessonId
            join quiz in quizRepository.TableNoTracking on quizCategory.Id equals quiz.QuizCategoryId into quizzesGroup
            from quiz in quizzesGroup.DefaultIfEmpty()
            join userQuiz in userQuizRepository.TableNoTracking.Where(uq => uq.UserId == currentUser.UserId)
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


    public async Task<IListResult<List<QuizDto>>> GetAllQuiz(QuizTypeDto quizTypeDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var quizzes = await (
            from q in quizRepository.TableNoTracking
            where q.QuizCategoryId == quizTypeDto.QuizCategoryId && q.QuizTypeId == quizTypeDto.QuizTypeId
            select q
        ).ToListAsync(cancellationToken);

        var userQuizIds = await (
            from uq in userQuizRepository.TableNoTracking
            where uq.UserId == currentUser.UserId
            select uq.QuizId
        ).Distinct().ToListAsync(cancellationToken);

        var quizQuestionCounts = await (
            from question in questionRepository.TableNoTracking
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
            Minutes = q.Minutes,
            IsDone = userQuizIds.Contains(q.Id),
            QuestionCount = questionCountDict.TryGetValue(q.Id, out var value) ? value : 0
        }).ToList();

        return new SuccessListResult<List<QuizDto>>(quizDtos);
    }


    public async Task<IListResult<List<QuestionDto>>> GetQuestionsByQuizId(int quizId, CancellationToken cancellationToken)
{
    var questions = await questionRepository.TableNoTracking
        .Where(q => q.QuizId == quizId)
        .Select(q => new
        {
            q.Id,
            q.Title,
            q.Hint
        })
        .Distinct()
        .ToListAsync(cancellationToken);

    var questionIds = questions.Select(q => q.Id).ToList();

    var answers = await answerRepository.TableNoTracking
        .Where(a => questionIds.Contains(a.QuestionId))
        .Select(a => new
        {
            a.Id,
            a.QuestionId,
            a.Title,
            a.IsCorrect 
        })
        .ToListAsync(cancellationToken);

    var questionDetails = await detailRepository.TableNoTracking
        .Where(qd => questionIds.Contains(qd.QuestionId))
        .GroupBy(qd => new { qd.QuestionId, qd.AnswerId })
        .Select(group => new
        {
            group.Key.QuestionId,
            group.Key.AnswerId,
            Count = group.Count()
        })
        .ToListAsync(cancellationToken);

    var totalResponsesPerQuestion = questionDetails
        .GroupBy(qd => qd.QuestionId)
        .Select(group => new
        {
            QuestionId = group.Key,
            TotalResponses = group.Sum(g => g.Count)
        })
        .ToList();

    var questionDtos = questions
        .Select(q => new QuestionDto
        { 
            Id = q.Id,
            Question = q.Title,
            Hint = q.Hint,
            Answers = answers
                .Where(a => a.QuestionId == q.Id)
                .Select(a => {
                    var answerDetail = questionDetails.FirstOrDefault(qd => qd.QuestionId == q.Id && qd.AnswerId == a.Id);
                    var totalResponses = totalResponsesPerQuestion.FirstOrDefault(tr => tr.QuestionId == q.Id)?.TotalResponses ?? 0;
                    var percentage = totalResponses > 0 ? (answerDetail?.Count ?? 0) * 100 / totalResponses : 0;

                    return new AnswerDto
                    {
                        Id = a.Id,
                        Title = a.Title,
                        IsCorrect = a.IsCorrect,
                        Percentage = percentage.ToString()
                    };
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
            CorrectCount = userQuizAddDto.QuestionDetails.Count(x => x.Status == Status.Correct),
            UserId = currentUser.UserId,
            MistakeCount = userQuizAddDto.QuestionDetails.Count(x => x.Status == Status.InCorrect),
            None = userQuizAddDto.QuestionDetails.Count(x => x.Status == Status.None),
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await userQuizRepository.AddAsync(userQuizEntity, cancellationToken);

        var questionDetails = userQuizAddDto.QuestionDetails
            .Select(q => new QuestionDetail
            {
                QuizId = userQuizAddDto.QuizId,
                QuestionId = q.QuestionId,
                AnswerId = q.AnswerId,
                Status = q.Status,
                UserId = currentUser.UserId
            }).ToList();

        await detailRepository.AddRangeAsync(questionDetails, cancellationToken);


        return new SuccessResult(Messages.SuccessFully);
    }

    public async Task<IListResult<List<UserQuizHistoryDto>>> GetUserQuizHistory(CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var quizQuestionCounts = await (
            from question in questionRepository.TableNoTracking
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
            from uq in userQuizRepository.TableNoTracking
            join q in quizRepository.TableNoTracking on uq.QuizId equals q.Id
            join qc in quizCategoryRepository.TableNoTracking on q.QuizCategoryId equals qc.Id
            join qt in lessonRepository.TableNoTracking on qc.LessonId equals qt.Id
            where uq.UserId == currentUser.UserId
            select new
            {
                uq.Id,
                QuizCategoryTitle = qc.Title,
                LessonTitle = qt.Title,
                QuizTitle = q.Title,
                uq.CorrectCount,
                uq.MistakeCount,
                uq.None,
                RegDateTime = uq.RegDate,
                QuizId = q.Id
            }
        ).ToListAsync(cancellationToken);

        var userQuizHistoryDtos = userQuizHistories
            .OrderByDescending(uq => uq.RegDateTime)
            .Select(uq => new UserQuizHistoryDto
            {
                Id = uq.Id,
                QuizId = uq.QuizId,
                QuizCategoryTitle = uq.QuizCategoryTitle,
                LessonTitle = uq.LessonTitle,
                QuizTitle = uq.QuizTitle,
                CorrectCount = uq.CorrectCount,
                None = uq.None,
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

        await lessonRepository.AddAsync(lessons, cancellationToken);

        return new SuccessResult(Messages.SuccessFully);
    }

    public async Task<IListResult<List<LessonsDto>>> GetAllLesson(CancellationToken cancellationToken)
    {
        var lessons = await lessonRepository.GetListAsync(x => x.IsDeleted == false, cancellationToken);

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

        await quizCategoryRepository.AddAsync(quizCategory, cancellationToken);

        return new SuccessResult(Messages.SuccessFully);
    }

    public async Task<IResult> AddQuiz(AddQuizDto addQuizDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var quiz = new Quiz
        {
            Title = addQuizDto.Title,
            QuizCategoryId = addQuizDto.QuizCategoryId,
            QuizTypeId = addQuizDto.QuizType,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await quizRepository.AddAsync(quiz, cancellationToken);

        return new SuccessResult(Messages.SuccessFully);
    }

    public async Task<IResult> AddQuestion(AddQuestionDto addQuestionDto, CurrentUserDto currentUser,
        CancellationToken cancellationToken)
    {
        var question = new Question
        {
            QuizId = addQuestionDto.QuizId,
            Title = addQuestionDto.Title,
            Hint = addQuestionDto.Hint,
            RegUserId = currentUser.UserId,
            RegDate = DateTime.Now,
            IsDeleted = false
        };

        await questionRepository.AddAsync(question, cancellationToken);

        foreach (var answer in addQuestionDto.Answers.Select(answerDto => new Answer
                 {
                     QuestionId = question.Id,
                     Title = answerDto.Title,
                     IsCorrect = answerDto.IsCorrect,
                     RegUserId = currentUser.UserId,
                     RegDate = DateTime.Now,
                     IsDeleted = false
                 }))
        {
            await answerRepository.AddAsync(answer, cancellationToken);
        }

        return new SuccessResult(Messages.SuccessFully);
    }
}