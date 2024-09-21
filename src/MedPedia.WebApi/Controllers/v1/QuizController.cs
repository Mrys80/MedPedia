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
    public async Task<IActionResult> GetAllQuizTypeWithPercentage(
        CancellationToken cancellationToken)
    {
        var result =
            await _quizService.GetAllQuizTypeWithPercentage(CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]/{quizTypeId:Int}")]
    public async Task<IActionResult> GetAllQuizCategories(int quizTypeId, CancellationToken cancellationToken)
    {
        var result = await _quizService.GetAllQuizCategories(quizTypeId, CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]/{quizCategoryId:Int}")]
    public async Task<IActionResult> GetAllQuiz(int quizCategoryId, CancellationToken cancellationToken)
    {
        var result = await _quizService.GetAllQuiz(quizCategoryId, CurrentUser, cancellationToken);

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
    public async Task<IActionResult> AddQuizType([FromBody] AddQuizTypeDto addQuizTypeDto,
        CancellationToken cancellationToken)
    {
        var result = await _quizService.AddQuizType(addQuizTypeDto, CurrentUser, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllQuizType(CancellationToken cancellationToken)
    {
        var result = await _quizService.GetAllQuizType(cancellationToken);

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