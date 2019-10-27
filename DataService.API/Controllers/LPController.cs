using System;
using System.Collections.Generic;
using System.Linq;
using DataService.Model;
using DataService.Workers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LPController : ControllerBase
    {
        private readonly IDataLoader<LPData> _dataLoader;
        private readonly IDataAggregator<LPData> _dataAggregator;
        private readonly ILogger<LPController> _logger;

        public LPController(IDataLoader<LPData> dataLoader, IDataAggregator<LPData> dataAggregator, ILogger<LPController> logger)
        {
            _dataLoader = dataLoader;
            _dataAggregator = dataAggregator;
            _logger = logger;
        }

        [HttpGet]
        public APIResponse Get()
        {
            var response = Enumerable.Empty<EnergyResponse>();
            string status;
            var message = string.Empty;

            try
            {
                var data = _dataLoader.Load();
                response = _dataAggregator.Aggregate(data);
                status = "Success";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong : {ex.Message}");
                status = "Error";
                message = ex.Message;
            }

            return new APIResponse
            {
                Status = status,
                Message = message,
                EnergyResponse = response
            };
        }
    }
}
