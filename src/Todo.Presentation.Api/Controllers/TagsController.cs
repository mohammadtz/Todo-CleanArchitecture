using Microsoft.AspNetCore.Mvc;
using Todo.Application.Contract.Tag;
using Todo.Presentation.Api.Utils;

namespace Todo.Presentation.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ITagApplication _tagApplication;

    public TagsController(ITagApplication tagApplication)
    {
        _tagApplication = tagApplication;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<TagDto>>>> GetTagsList()
    {
        try
        {
            var result = await _tagApplication.GetAll();

            return new Response<List<TagDto>>(result);
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine(e);
            return BadRequest(this.HandleArgumentNullException(e));
        }
        catch (Exception e)
        {
            return StatusCode(500, this.UnHandledException(e));
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<TagDto>>> GetTag(int id)
    {
        try
        {
            var result = await _tagApplication.GetById(id);

            return new Response<TagDto>(result);
        }
        catch (ArgumentNullException e)
        {
            return BadRequest(this.HandleArgumentNullException(e));
        }
        catch (Exception e)
        {
            return StatusCode(500, this.UnHandledException(e));
        }
    }

    [HttpPost]
    public async Task<ActionResult<Response<bool>>> CreateTag([FromBody] TagCommand command)
    {
        try
        {
            await _tagApplication.Create(command);

            return new Response<bool>(true);
        }
        catch (ArgumentNullException e)
        {
            return BadRequest(this.HandleArgumentNullException(e));
        }
        catch (Exception e)
        {
            return StatusCode(500, this.UnHandledException(e));
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Response<bool>>> CreateTag(int id, [FromBody] TagCommand command)
    {
        try
        {
            await _tagApplication.Update(id, command);

            return new Response<bool>(true);
        }
        catch (ArgumentNullException e)
        {
            return BadRequest(this.HandleArgumentNullException(e));
        }
        catch (Exception e)
        {
            return StatusCode(500, this.UnHandledException(e));
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> RemoveTag(int id)
    {
        try
        {
            await _tagApplication.Delete(id);

            return new Response<bool>(true);
        }
        catch (ArgumentNullException e)
        {
            return BadRequest(this.HandleArgumentNullException(e));
        }
        catch (Exception e)
        {
            return StatusCode(500, this.UnHandledException(e));
        }
    }
}
