using poc.quartz.utils.interfaces;
using Quartz;
using System.Threading.Tasks;

namespace poc.quartz.jobs.scheduler.JobWrapper
{
    public class JobWrapper<T> : IJob where T : IWrappedJob, new()
    {
        public Task Execute(IJobExecutionContext context)
        {
            new T().ExecutarJob();
            return Task.CompletedTask;
        }
    }
}
