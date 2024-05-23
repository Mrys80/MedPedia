using MedPedia.Entities.Dto.Quiz;
using MedPedia.Services.Abstracts;
using MedPedia.WebFramework.Api;
using Microsoft.AspNetCore.Mvc;

namespace MedPedia.WebApi.Controllers.v1;

[ApiVersion("1")]
public class QuizController : BaseController
{
    private readonly IQuizService _quizService;

    
    public QuizController(IHttpContextAccessor httpContextAccessor, IQuizService quizService) : base(httpContextAccessor)
    {
        _quizService = quizService;
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllQuizTypeWithPercentage(
        CancellationToken cancellationToken)
    {
        var result =
            await _quizService.GetAllQuizTypeWithPercentage(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllQuizCategories(CancellationToken cancellationToken)
    {
        var result = await _quizService.GetAllQuizCategories(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllQuiz(CancellationToken cancellationToken)
    {
        var result = await _quizService.GetAllQuiz(cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    
    [HttpGet("[action]/{quizId:Int}")]
    public async Task<IActionResult> GetQuestionsByQuizId(int quizId , CancellationToken cancellationToken)
    {
        var result = await _quizService.GetQuestionsByQuizId( quizId , cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> UserQuizAdd([FromBody] UserQuizAddDto userQuizAddDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _quizService.UserQuizAdd(userQuizAddDto, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
    
}