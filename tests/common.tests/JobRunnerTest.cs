using System;
using System.Linq;
using System.Linq.Expressions;
using core;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Moq;
using NUnit.Framework;
using testbase;

namespace common.tests
{
    using static LetTestHelper.LetHelper;
    
    [TestFixture, Category(nameof(JobRunner))]
    public class JobRunnerTest : TestBase
    {
        private Mock<IBackgroundJobClient> MockBackgroundJobClient => Let(() => new Mock<IBackgroundJobClient>());
        private IBackgroundJobClient BackgroundJobClient => MockBackgroundJobClient.Object;

        private JobRunner Subject => Let(() => new JobRunner(BackgroundJobClient));
        
        [Test]
        public void ItEnqueuesAJob()
        {
            Subject.Queue<MyTestJob, MyTestJobOptions>(options =>
            {
                options.DefaultIsOneButShouldBeLeet = 1337;
            });
            
            MockBackgroundJobClient.Verify(c =>
                c.Create(
                    It.Is<Job>(job =>
                        job.Type == typeof(MyTestJob) &&
                        ((MyTestJobOptions) job.Args.Single()).DefaultIsOneButShouldBeLeet == 1337
                    ),
                    It.IsAny<EnqueuedState>()
                ),
                Times.Once
            );
        }

        class MyTestJobOptions : IJobOptions
        {
            public int DefaultIsOneButShouldBeLeet { get; set; } = 1;
        }

        class MyTestJob : IJob<MyTestJobOptions>
        {
            public void Perform(MyTestJobOptions jobOptions)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}