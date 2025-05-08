using Quartz;
using Quartz.Impl;

namespace TaskStorage.Jobs;

/// <summary>
/// Класс, задающий расписание выполнения обновления.
/// </summary>
public class UpdateScheduler
{
    public static async Task Start(IConfiguration configuration)
    {
        var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        await scheduler.Start();

        var job = JobBuilder.Create<StorageUpdater>().Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("trigger1", "group1")
            .StartNow()
            .WithCronSchedule(configuration["UpdateCronSchedule"])
            .Build();
        
        await scheduler.ScheduleJob(job, trigger);
    }

}