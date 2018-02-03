using Microsoft.AspNetCore.Mvc;
using services;

namespace frontend.Controllers
{
    public class TasksController : Controller
    {
        private ITaskService TaskService { get; }

        public TasksController(ITaskService taskService)
        {
            TaskService = taskService;
        }
        
        public IActionResult Long()
        {
            TaskService.LongRunningTask();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Repeating()
        {
            TaskService.RepeatingTask();
            return RedirectToAction("Index", "Home");
        }
    }
}