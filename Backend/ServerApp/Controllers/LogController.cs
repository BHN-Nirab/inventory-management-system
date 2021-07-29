using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServerApp.Model;
using ServerApp.Repository;

namespace ServerApp.Controllers
{
    public class LogController : Controller
    {
        private readonly LogRepository _logRepository = new LogRepository();

        [HttpPost("api/log/dailyreports")]
        public dynamic GetDailyReports([FromBody] dynamic request)
        {

            var data = JObject.Parse(Convert.ToString(request));


            DateTime Day = data.SelectToken("Day");
          
            string Token = Convert.ToString(data.SelectToken("token"));
            int UserId = data.SelectToken("UserId");

            var result = _logRepository.GetDailyReport(Day, UserId, Token);

            string jsonData = JsonConvert.SerializeObject(result);

            return Ok(jsonData);
        }

        [HttpPost("api/log/monthlyreports")]
        public dynamic GetMonthlyReports([FromBody] dynamic request)
        {

            var data = JObject.Parse(Convert.ToString(request));

            DateTime Month = data.SelectToken("Month");
            string Token = Convert.ToString(data.SelectToken("token"));
            int UserId = data.SelectToken("UserId");

            var result = _logRepository.GetMonthlyReport(Month, UserId, Token);

            string jsonData = JsonConvert.SerializeObject(result);

            return Ok(jsonData);
        }

    }
}
