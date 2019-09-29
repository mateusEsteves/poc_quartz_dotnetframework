using poc.quartz.utils.interfaces;

namespace poc.quartz.jobs.intervalos
{
    public class JobIntervalosCode : IWrappedJob
    {
        public void ExecutarJob()
        {
            System.Diagnostics.Debug.WriteLine("Job Intervalos is running");
        }
    }
}
