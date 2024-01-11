using BusinessObject.Models;
using IDBMS_API.Supporters.EmailSupporter;
using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Services.Background
{
    public class DeadlineBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public DeadlineBackgroundService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var paymentStageService = scope.ServiceProvider.GetRequiredService<PaymentStageService>();

                    var paymenOutOfDate = paymentStageService.GetOutOfDateStage();
                    foreach (var stage in paymenOutOfDate)
                    {
                        var owner = paymentStageService.GetOwner(stage.Id);
                        string link = _configuration["Server:Frontend"] + "/project/" + stage.ProjectId.ToString() + "/stages";

                        EmailSupporter.SendDeadlineEnglishEmail(owner.Email, link, stage.EndTimePayment.ToString(), stage.EndTimePayment.ToString());
                    }
                    var paymenAboutDate = paymentStageService.GetOutOfDateStage();
                    foreach (var stage in paymenAboutDate)
                    {
                        var owner = paymentStageService.GetOwner(stage.Id);
                        string link = _configuration["Server:Frontend"] + "/project/" + stage.ProjectId.ToString() + "/stages";

                        EmailSupporter.SendDeadlineEnglishEmail(owner.Email, link, stage.EndTimePayment.ToString(), stage.EndTimePayment.ToString());
                    }

                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
