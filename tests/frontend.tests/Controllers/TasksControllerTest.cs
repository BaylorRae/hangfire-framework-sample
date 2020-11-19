using frontend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using services;
using testbase;

namespace frontend.tests.Controllers
{
    using static LetTestHelper.LetHelper;
        
    public class TasksControllerTestBase : TestBase
    {
        protected Mock<ITaskService> MockTaskService => Let(() => new Mock<ITaskService>());
        protected ITaskService TaskService => MockTaskService.Object;
        
        protected TasksController Subject => Let(() => new TasksController(TaskService));
    }

    [TestFixture, Category(nameof(TasksController))]
    public class TasksController_Long : TasksControllerTestBase
    {
        [Test]
        public void ItQueuesALongRunningTask()
        {
            Subject.Long();
            MockTaskService.Verify(s => s.LongRunningTask(), Times.Once);
        }

        [Test]
        public void ItRedirectsToHomeIndex()
        {
            var result = Subject.Long() as RedirectToActionResult;
            Assert.That($"{result?.ControllerName}#{result?.ActionName}", Is.EqualTo("Home#Index"));
        }
    }
}