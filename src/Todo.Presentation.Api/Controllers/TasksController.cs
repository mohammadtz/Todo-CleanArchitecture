using Microsoft.AspNetCore.Mvc;
using Todo.Application.Contract.Todo;
using Todo.Presentation.Api.Utils;

namespace Todo.Presentation.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ITodoApplication _todoApplication;

    public TasksController(ITodoApplication todoApplication)
    {
        _todoApplication = todoApplication;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<TodoDto>>>> GetTasksList()
    {
        try
        {
            var result = await _todoApplication.GetAll();

            return new Response<List<TodoDto>>(result);
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
    public async Task<ActionResult<Response<TodoDto>>> GetTodo(int id)
    {
        try
        {
            var result = await _todoApplication.GetById(id);

            return new Response<TodoDto>(result);
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
    public async Task<ActionResult<Response<bool>>> CreateTodo([FromBody] TodoCommand command)
    {
        try
        {
            await _todoApplication.Create(command);

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
    public async Task<ActionResult<Response<bool>>> CreateTodo(int id, [FromBody] TodoCommand command)
    {
        try
        {
            await _todoApplication.Update(id, command);

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

    [HttpPatch("changeStatus/{id:int}")]
    public async Task<ActionResult<Response<bool>>> CreateTodo(int id)
    {
        try
        {
            await _todoApplication.ChangeStatus(id);

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
    public async Task<ActionResult<Response<bool>>> RemoveTodo(int id)
    {
        try
        {
            await _todoApplication.Delete(id);

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
