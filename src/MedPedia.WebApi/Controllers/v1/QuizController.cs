using MedPedia.Entities.Dto.Quiz;
using MedPedia.Services.Abstracts;
using MedPedia.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace MedPedia.WebApi.Controllers.v1;

[ApiVersion("1")]
public class QuizController : BaseController
{
    private readonly IQuizService _quizService;


    public QuizController(IHttpContextAccessor httpContextAccessor, IQuizService quizService) : base(
        httpContextAccessor)
    {
        _quizService = quizService;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllLessonWithPercentage(
        CancellationToken cancellationToken)
    {
        var result =
            await _quizService.GetAllLessonWithPercentage(CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]/{lessonId:Int}")]
    public async Task<IActionResult> GetAllQuizCategories(int lessonId, CancellationToken cancellationToken)
    {
        var result = await _quizService.GetAllQuizCategories(lessonId, CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllQuiz([FromQuery] QuizTypeDto quizTypeDto, CancellationToken cancellationToken)
    {
        var result = await _quizService.GetAllQuiz(quizTypeDto, CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }


    [HttpGet("[action]/{quizId:Int}")]
    public async Task<IActionResult> GetQuestionsByQuizId(int quizId, CancellationToken cancellationToken)
    {
        var result = await _quizService.GetQuestionsByQuizId(quizId, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UserQuizAdd([FromBody] UserQuizAddDto userQuizAddDto,
        CancellationToken cancellationToken)
    {
        var result = await _quizService.UserQuizAdd(userQuizAddDto, CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetUserQuizHistory(CancellationToken cancellationToken)
    {
        var result = await _quizService.GetUserQuizHistory(CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddLesson([FromBody] AddLessonDto addLessonDto,
        CancellationToken cancellationToken)
    {
        var result = await _quizService.AddLesson(addLessonDto, CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllLesson(CancellationToken cancellationToken)
    {
        var result = await _quizService.GetAllLesson(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddQuizCategory([FromBody] AddQuizCategoryDto addQuizCategoryDto,
        CancellationToken cancellationToken)
    {
        var result = await _quizService.AddQuizCategory(addQuizCategoryDto, CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddQuiz([FromBody] AddQuizDto addQuizDto,
        CancellationToken cancellationToken)
    {
        var result = await _quizService.AddQuiz(addQuizDto, CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddQuestion([FromBody] AddQuestionDto addQuestionDto,
        CancellationToken cancellationToken)
    {
        var result = await _quizService.AddQuestion(addQuestionDto, CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}