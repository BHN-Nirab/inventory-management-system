using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServerApp.Model;
using ServerApp.Repository;

namespace ServerApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository = new UserRepository();

        [HttpPost("api/user/createaccount")]
        public dynamic CreateAccount([FromBody] dynamic request)
        {

            var data = JObject.Parse(Convert.ToString(request));
         
            string Name = Convert.ToString(data.SelectToken("Name"));
            string Email = Convert.ToString(data.SelectToken("Email"));
            string Password = Convert.ToString(data.SelectToken("Password"));

            var result = _userRepository.CreateAccount(Name, Email, Password);

            string jsonData = JsonConvert.SerializeObject(result);

            return Ok(jsonData);
        }


        [HttpPost("api/user/login")]
        public dynamic Login([FromBody] dynamic request)
        {

            var data = JObject.Parse(Convert.ToString(request));

            string Email = Convert.ToString(data.SelectToken("Email"));
            string Password = Convert.ToString(data.SelectToken("Password"));

            var result = _userRepository.Login(Email, Password);

            string jsonData = JsonConvert.SerializeObject(result);

            return Ok(jsonData);
        }

        [HttpPost("api/user/isvalidsession")]
        public dynamic IsValidSession([FromBody] dynamic request)
        {
            string Token = "";
            int Id = -1;
            try
            {
                var data = JObject.Parse(Convert.ToString(request));

                Token = Convert.ToString(data.SelectToken("token"));
                Id = data.SelectToken("Id");
            }
            catch (Exception e) { 
            }


            var result = _userRepository.IsValidSession(Id, Token);

            return Ok(result);
        }

    }
}
