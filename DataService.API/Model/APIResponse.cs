using System;
using System.Collections.Generic;

namespace DataService.Model
{
    public class APIResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public IEnumerable<EnergyResponse> EnergyResponse { get; set; }
    }
}
