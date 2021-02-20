using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleHomeBroker.Host.Alice.Models;
using SimpleHomeBroker.Host.Alice.Services;

namespace SimpleHomeBroker.Host.Alice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AliceController : ControllerBase
    {
        private readonly IAliceRequestService _aliceRequestService;
        private readonly ILogger<AliceController> _logger;

        public AliceController(IAliceRequestService aliceRequestService,
            ILogger<AliceController> logger)
        {
            _aliceRequestService = aliceRequestService ?? throw new ArgumentNullException(nameof(aliceRequestService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<AliceResponse> WebHook(AliceRequest request)
        {
            string responseText;

            try
            {
                responseText = await _aliceRequestService.HandleRequestAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Не удалось выполнить команду через Алису");

                responseText = $"Не удалось выполнить команду. {ex.Message}";
            }

            return new AliceResponse(responseText);
        }
    }
}