using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VirturlMeetingAssitant.Backend.Db;

namespace VirturlMeetingAssitant.Backend
{
    public class MeetingNotifyHostedService : BackgroundService
    {
        private readonly ILogger<MeetingNotifyHostedService> _logger;
        private readonly IMailService _mailService;
        private readonly IServiceScopeFactory _scopeFactory;

        public MeetingNotifyHostedService(IServiceScopeFactory scopeFactory, ILogger<MeetingNotifyHostedService> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;

            _scopeFactory = scopeFactory;
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            // What's period of checking every meeting. (Unit: sec)
            var checkPeriod = 10;
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                // Inject IMeetingRepository to here
                var meetingRepo = scope.ServiceProvider.GetRequiredService<IMeetingRepository>();

                var meetings = await meetingRepo.GetAll();
                foreach (var m in meetings)
                {
                    // If there's a meeting will begin less then one hour. Then we send email notification to attendees
                    var diff = (m.FromDate.AddHours(-1) - DateTime.UtcNow).TotalSeconds;
                    if (diff <= checkPeriod && diff >= 0)
                    {
                        _logger.LogInformation($"Send a meeting nofication email to {m.Title} attendees");
                        await _mailService.SendMail("Your meeting will start soon",
                            $"<p>Your meeting '{m.Title}' will start about one hour later.</p><br>" +
                            $"<p>Description: { (String.IsNullOrEmpty(m.Description) ? "Empty" : m.Description) }</p>" +
                            $"<p>The Host: { m.Creator.Name }</p>" +
                            $"<p>The location is {m.Location.Name}</p>" +
                            $"<p>The Departments of this meeting is {String.Join(",", m.Departments.Select(x => x.Name))} </p>" +
                            $"<p>The meeting will start on {m.FromDate}. End on {m.ToDate} </p>",
                            MailType.MeetingNotifying,
                            m.Attendees.Select(x => x.Email)
                        );
                    }
                }

                // Make this task not run very frequently.
                await Task.Delay(1000 * checkPeriod);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Meeting Notify Hosted Service running.");

            await DoWork(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Meeting Notify Service Hosted Service is stopping.");
            await base.StopAsync(stoppingToken);
        }
    }

}