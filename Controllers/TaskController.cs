using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    [HttpGet]
    public IActionResult GetTasks()
    {
        // Retorna lista de tarefas do usuário logado
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateTask([FromBody] TodoTask task)
    {
        // Cria uma nova tarefa
        return Ok();
    }

    // Demais endpoints aqui...
}
