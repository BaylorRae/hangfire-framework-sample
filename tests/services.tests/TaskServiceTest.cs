using System;
using core;
using Moq;
using NUnit.Framework;
using services.Jobs;
using testbase;

namespace services.tests
{
    using static LetTestHelper.LetHelper;
    
    public class TaskServiceTestBase : TestBase
    {
        protected Mock<IJobRunner> MockJobRunner => Let(() => new Mock<IJobRunner>());
        protected IJobRunner JobRunner => MockJobRunner.Object;
        
        protected TaskService Subject => Let(() => new TaskService(JobRunner));
    }

    [TestFixture, System.ComponentModel.Category(nameof(TaskService))]
    public class TaskService_LongRunningTask : TaskServiceTestBase
    {

        [Test]
        public void ItQueuesALongRunningTask()
        {
            var options = new LongRunningJobOptions
            {
                TaskTime = 99
            };
            
            MockJobRunner.Setup(r =>
                    r.Queue<LongRunningJob, LongRunningJobOptions>(
                        It.IsAny<Action<LongRunningJobOptions>>()
                    )
                )
                .Callback((Action<LongRunningJobOptions> cb) => { cb(options); });
            
            Subject.LongRunningTask();
            
            Assert.That(options.TaskTime, Is.EqualTo(10));
        }
        
    }
}