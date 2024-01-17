using BusinessObject.Models;
using IDBMS_API.Supporters.EmailSupporter;
using Microsoft.AspNetCore.Mvc;

namespace IDBMS_API.Services.Background
{
    [ApiExplorerSettings(IgnoreApi = true)]
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
                    var deadlineService = scope.ServiceProvider.GetRequiredService<DeadlineService>();


                    var paymenAbout10ToExpire = paymentStageService.GetAbout10ToExpireStage();
                    foreach (var stage in paymenAbout10ToExpire)
                    {

                        var owner = paymentStageService.GetOwner(stage.Id);
                        string link = _configuration["Server:Frontend"] + "/project/" + stage.ProjectId.ToString() + "/stages";
                        deadlineService.SendDeadlineEmail(owner.Email,owner.Name, link, stage.EndTimePayment.ToString(), stage.EndTimePayment.ToString(),owner.Language==0);
                    }
                    var paymenOutOfDateStage = paymentStageService.GetOutOfDateStage();
                    foreach (var stage in paymenOutOfDateStage)
                    {

                        var owner = paymentStageService.GetOwner(stage.Id);
                        string link = _configuration["Server:Frontend"] + "/project/" + stage.ProjectId.ToString() + "/stages";
                        deadlineService.SendOutDateEmail(owner.Email,owner.Name, link, stage.EndTimePayment.ToString(),owner.Language==0);

                    }

                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
