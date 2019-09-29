using poc.quartz.jobs.infinito;
using poc.quartz.jobs.intervalos;
using poc.quartz.jobs.scheduler.JobWrapper;
using Quartz;
using Quartz.Impl;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace poc.quartz.jobs.scheduler
{
    public class InicializadorScheduler
    {
        public async Task InicializarScheduler()
        {
            var factory = new StdSchedulerFactory(ObterConfiguracaoDoScheduler());
            var scheduler = await factory.GetScheduler();

            await scheduler.Start();

            if (scheduler.IsStarted)
            {
                var inicializacaoDeJobs = ObterConfiguracaoDeJobs()
                    .Select(jobInfo => scheduler.ScheduleJob(jobInfo.Job, jobInfo.Trigger))
                    .ToArray();

                await Task.WhenAll(inicializacaoDeJobs);
            }
        }

        private NameValueCollection ObterConfiguracaoDoScheduler()
        {
            return new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
        }

        private List<(IJobDetail Job, ITrigger Trigger)> ObterConfiguracaoDeJobs()
        {
            return new List<(IJobDetail, ITrigger)>
            {
                ObterConfiguracaoJobInfinito(),
                ObterConfiguracaoJobIntervalos()
            };
        }

        private (IJobDetail Job, ITrigger Trigger) ObterConfiguracaoJobInfinito()
        {
            var jobInfinito = JobBuilder.Create<JobWrapper<JobInfinitoCode>>()
                .WithIdentity("JobInfinito", "JobsExemplo")
                .Build();

            var triggerJobInfinito = TriggerBuilder.Create()
                .WithIdentity("TriggerJobInfinito", "TriggersExemplo")
                .StartNow()
                .Build();

            return (jobInfinito, triggerJobInfinito);
        }

        private (IJobDetail Job, ITrigger Trigger) ObterConfiguracaoJobIntervalos()
        {
            var jobIntervalos = JobBuilder.Create<JobWrapper<JobIntervalosCode>>()
                .WithIdentity("JobIntervalos", "JobsExemplo")
                .Build();

            var triggerJobIntervalos = TriggerBuilder.Create()
                .WithIdentity("TriggerJobIntervalos", "TriggersExeplo")
                .WithSimpleSchedule(scheduleBuilder => scheduleBuilder.WithIntervalInMinutes(5).RepeatForever())
                .StartNow()
                .Build();

            return (jobIntervalos, triggerJobIntervalos);
        }
    }
}
