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
        /// <summary>
        /// Send a new email to the specified recipients
        /// </summary>
        Task SendMail(string subject, string content, MailType type, IEnumerable<string> recipients);
    }

    public class MailService : IMailService
    {
        private readonly ILogger<MailService> _logger;
        private readonly Mailer.MailerClient _grpcClinet;
        public MailService(ILogger<MailService> logger, IConfiguration configuration)
        {
            _logger = logger;

            // Setup gRPC server connection to mail sevrver.
            var channel = GrpcChannel.ForAddress(configuration["MailServerAddr"]);
            _grpcClinet = new Mailer.MailerClient(channel);
        }
        public async Task SendMail(string subject, string content, MailType type, IEnumerable<string> recipients)
        {
            // Create a new mail reqeust.
            var request = new MailRequest() { Subject = subject, Content = content, Type = type };
            request.Emails.AddRange(recipients);

            // Send the request to mail server
            var reply = await _grpcClinet.SendMailAsync(request);

            _logger.Log(LogLevel.Information, $"Received reply {reply}");
        }
    }
}