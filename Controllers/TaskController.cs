using Microsoft.AspNetCore.Mvc;
using MyToDoList.Models;

namespace MyToDoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private static List<TodoTask> tasks = new();
        private static int currentId = 1;

        [HttpGet("{userId}")]
        public IActionResult GetTasksByUser(string userId)
        {
            var userTasks = tasks.Where(t => t.UserId == userId).ToList();
            return Ok(userTasks);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TodoTask task)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            task.Id = currentId++;
            tasks.Add(task);
            return CreatedAtAction(nameof(GetTasksByUser), new { userId = task.UserId }, task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TodoTask updatedTask)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.IsCompleted = updatedTask.IsCompleted;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            tasks.Remove(task);
            return NoContent();
        }
    }
}
