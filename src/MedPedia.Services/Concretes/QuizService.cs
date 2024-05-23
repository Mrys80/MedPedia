using Mapster;
using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto.Quiz;
using MedPedia.Entities.Dto.QuizTypeWithPercentage;
using MedPedia.Entities.Entities;
using MedPedia.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using Teb.Data.Abstracts;

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
        IRepository<Question> questionRepository, IRepository<Answer> answerRepository)
    {
        _quizRepository = quizRepository;
        _quizTypeRepository = quizTypeRepository;
        _userQuizRepository = userQuizRepository;
        _quizCategoryRepository = quizCategoryRepository;
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
    }


    public async Task<IListResult<List<QuizTypeWithPercentageDto>>> GetAllQuizTypeWithPercentage(
        CancellationToken cancellationToken)
    {
        var quizTypeWithPercentageList = await (
            from qt in _quizTypeRepository.TableNoTracking
            join qc in _quizCategoryRepository.TableNoTracking on qt.Id equals qc.QuizTypeId
            join q in _quizRepository.TableNoTracking on qc.Id equals q.QuizCategoryId
            join uq in _userQuizRepository.TableNoTracking on q.Id equals uq.QuizId into uqGroup
            group new { qt, q, uqGroup } by new { qt.Id, qt.Title }
            into g
            select new QuizTypeWithPercentageDto
            {
                Id = g.Key.Id,
                Title = g.Key.Title,
                Percentage = !g.Any() ? 0 : (double)g.Sum(x => x.uqGroup.Any() ? 1 : 0) / g.Count() * 100
            }).ToListAsync(cancellationToken);

        return new SuccessListResult<List<QuizTypeWithPercentageDto>>(quizTypeWithPercentageList);
    }

    public async Task<IListResult<List<QuizCategoryDto>>> GetAllQuizCategories(CancellationToken cancellationToken)
    {
        var categories = await
            _quizCategoryRepository.GetListAsync(x => x.IsDeleted == false, cancellationToken);

        var categoriesDtos = categories.Adapt<IEnumerable<QuizCategoryDto>>().ToList();

        return new SuccessListResult<List<QuizCategoryDto>>(categoriesDtos);
    }

    public async Task<IListResult<List<QuizDto>>> GetAllQuiz(CancellationToken cancellationToken)
    {
        var quizDtos = await (
            from q in _quizRepository.TableNoTracking
            join uq in _userQuizRepository.TableNoTracking on q.Id equals uq.QuizId into gj
            from uq in gj.DefaultIfEmpty()
            select new QuizDto
            {
                Id = q.Id,
                Title = q.Title,
                IsDone = uq != null
            }
        ).ToListAsync(cancellationToken);

        return new SuccessListResult<List<QuizDto>>(quizDtos);
    }

    public async Task<IListResult<List<QuestionDto>>> GetQuestionsByQuizId(int quizId,
        CancellationToken cancellationToken)
    {
        var questionDtos = await (
                from q in _questionRepository.TableNoTracking
                where q.QuizId == quizId
                join a in _answerRepository.TableNoTracking on q.Id equals a.QuestionId
                select new QuestionDto
                {
                    Id = q.Id,
                    Question = q.Title,
                    Answers = (
                        from answer in _answerRepository.TableNoTracking
                        where answer.QuestionId == q.Id
                        select new AnswerDto
                        {
                            Id = answer.Id,
                            Title = answer.Title,
                            IsCorrect = answer.IsCorrect
                        }).ToList()
                })
            .Distinct()
            .ToListAsync(cancellationToken);

        return new SuccessListResult<List<QuestionDto>>(questionDtos);
    }

    public async Task<IResult> UserQuizAdd(UserQuizAddDto userQuizAddDto, CancellationToken cancellationToken)
    {
        var userQuizEntity = new UserQuiz
        {
            CorrectCount = userQuizAddDto.CorrectCount,
            MistakeCount = userQuizAddDto.MistakeCount
        };
        
        await _userQuizRepository.AddAsync(userQuizEntity,cancellationToken);

        return new SuccessResult(Messages.SuccessFully);

    }
}