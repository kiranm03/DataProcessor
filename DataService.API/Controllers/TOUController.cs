using System;
using System.Collections;
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
    public class TOUController : ControllerBase
    {
        private readonly IDataLoader<TOUData> _dataLoader;
        private readonly IDataAggregator<TOUData> _dataAggregator;
        private readonly ILogger<TOUController> _logger;

        public TOUController(IDataLoader<TOUData> dataLoader, IDataAggregator<TOUData> dataAggregator, ILogger<TOUController> logger)
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
