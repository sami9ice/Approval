using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BackgroundService
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="IHostedService" />
    public class NotificationWorkerService : IHostedService, IDisposable
    {
        // private readonly IApplicationDbContext applicationDbContext;
        // private readonly ILogger<NotificationWorkerService> logger;
        // private readonly IHttpClientFactory httpClientFactory;
        private readonly IServiceScopeFactory serviceScopeFactory;

        private readonly ILogger<NotificationWorkerService> logger;
        // private IServiceScope scope;
        private Timer timer;
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationWorkerService"/> class.
        /// </summary>

        public NotificationWorkerService(IServiceScopeFactory serviceScopeFactory, ILogger<NotificationWorkerService> logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //try
            //{
            //    timer = new Timer(async o =>
            //    {
            //        await SendMail(cancellationToken);
            //        logger.LogInformation("Mail Service called");
            //    }, null,
            //        TimeSpan.Zero,
            //        TimeSpan.FromSeconds(60));
            //}
            //catch (Exception ex)
            //{
            //    logger.LogError(ex.Message);
            //}
            logger.LogInformation("Service Stopped!");
            return Task.CompletedTask;
        }

        //private async Task SendMail(CancellationToken cancellationToken)
        //{
        //    var scope = serviceScopeFactory.CreateScope();
        //    //while (!cancellationToken.IsCancellationRequested)
        //    //{
        //    var applicationDbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

        //    var mailProvider = await applicationDbContext.MailConfigurations.FirstOrDefaultAsync(x => x.IsDefault, cancellationToken: cancellationToken);
        //    var mail = await applicationDbContext.NotificationMessages.Where(x => !x.Sent).ToArrayAsync();
        //    // logger.LogInformation($"Mail Service Running - Timer {watch.ElapsedMilliseconds} milliseconds");
        //    if (mailProvider is { ProviderType: Domain.Common.EmailProviderType.ELASTICMAIL })
        //    {
        //        if (mail.Any())
        //        {
        //            var payload = new Dictionary<string, string>{
        //                { "apikey", mailProvider.ApiKey},
        //                { "from", mailProvider.From },
        //                { "fromName", mailProvider.FromName },
        //                { "isTransactional", "true" }};

        //            var mailUrl = mailProvider.Url;
        //            var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
        //            HttpClient httpClient = httpClientFactory.CreateClient("HttpClient");
        //            foreach (var item in mail)
        //            {
        //                payload.TryAdd("to", item.To);
        //                payload.TryAdd("subject", item.NotificationActionType.ToString());
        //                payload.TryAdd("bodyHtml", item.Body);
        //                var formContent = new FormUrlEncodedContent(payload);
        //                HttpResponseMessage response = await httpClient.PostAsync(mailUrl, formContent, cancellationToken);
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    item.Sent = true;
        //                    item.DateModified = DateTime.UtcNow;
        //                    await applicationDbContext.SaveChangesAsync(cancellationToken);
        //                    logger.LogInformation($"Sending mail to {item.To}");
        //                }
        //                else
        //                {
        //                    logger.LogInformation(response.ReasonPhrase);
        //                }
        //            }
        //        }
        //    }
        //    else if (mailProvider is { ProviderType: Domain.Common.EmailProviderType.SENDGRID })
        //    {
        //        if (mail.Any())
        //        {
        //            foreach (var item in mail)
        //            {
        //                var client = new SendGridClient(mailProvider.ApiKey);
        //                var from = new EmailAddress(mailProvider.From, mailProvider.FromName);
        //                var subject = item.NotificationActionType.ToString();
        //                var to = new EmailAddress(item.To);
        //                var msg = MailHelper.CreateSingleEmail(from, to, subject, "", item.Body);
        //                SendGrid.Response response = await client.SendEmailAsync(msg, cancellationToken);

        //                if (response is { StatusCode: HttpStatusCode.Accepted })
        //                {
        //                    item.Sent = true;
        //                    await applicationDbContext.SaveChangesAsync(cancellationToken);
        //                    logger.LogInformation($"---- mail has been successfully sent to {item.To} using Send Grid Mail Provider -----");
        //                }
        //                else
        //                {
        //                    if (response != null)
        //                    {
        //                        Dictionary<string, dynamic> resBody = await response.DeserializeResponseBodyAsync(response.Body);

        //                        foreach (var i in resBody)
        //                        {
        //                            logger.LogInformation(i.Key);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    // await Task.Delay(1000 * 5, cancellationToken);
        //}

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            // logger.LogInformation("Service Stopped!");

            return Task.CompletedTask;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}