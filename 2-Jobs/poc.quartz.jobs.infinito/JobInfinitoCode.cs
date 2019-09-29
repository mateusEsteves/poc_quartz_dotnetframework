using poc.quartz.utils.interfaces;
using System.Threading;

namespace poc.quartz.jobs.infinito
{
    public class JobInfinitoCode: IWrappedJob
    {
        public void ExecutarJob()
        {            
            while(true) {
                System.Diagnostics.Debug.WriteLine("Job infinito is running");
                Thread.Sleep(1000);
            }
        }
    }
}
