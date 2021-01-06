using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VirturlMeetingAssitant.Backend.Db;

namespace VirturlMeetingAssitant.Backend
{
    public interface IMailService
    {
        Task SendMail(string subject, string content, MailType type, IEnumerable<string> recipients);
    }

    public class MailService : IMailService
    {
        private readonly ILogger<MailService> _logger;
        private readonly Mailer.MailerClient _grpcClinet;
        public MailService(ILogger<MailService> logger, IConfiguration configuration)
        {
            _logger = logger;

            var channel = GrpcChannel.ForAddress(configuration["MailServerAddr"]);
            _grpcClinet = new Mailer.MailerClient(channel);
        }
        public async Task SendMail(string subject, string content, MailType type, IEnumerable<string> recipients)
        {
            var request = new MailRequest() { Subject = subject, Content = content, Type = type };
            request.Emails.AddRange(recipients);

            var reply = await _grpcClinet.SendMailAsync(request);

            _logger.Log(LogLevel.Information, $"Received reply {reply}");
        }
    }
}