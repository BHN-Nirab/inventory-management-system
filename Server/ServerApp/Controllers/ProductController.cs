using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServerApp.Model;
using ServerApp.Repository;

namespace ServerApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository = new ProductRepository();

        [HttpPost("api/product/add")]
        public dynamic Add([FromBody] dynamic request)
        {

            var data = JObject.Parse(Convert.ToString(request));

            var Product = data.SelectToken("Product");

            string Name = Convert.ToString(Product.SelectToken("Name"));
            string Description = Convert.ToString(Product.SelectToken("Description"));
            int Unit = Product.SelectToken("Unit");
            double UnitPrice = Product.SelectToken("UnitPrice");

            string Token = Convert.ToString(data.SelectToken("token"));
            int UserId = data.SelectToken("UserId");
            string UserName = Convert.ToString(data.SelectToken("userName"));

            var result = _productRepository.Add(Name, Description, Unit, UnitPrice, UserId, UserName, Token);

            string jsonData = JsonConvert.SerializeObject(result);

            return Ok(jsonData);
        }


        [HttpPost("api/product/update")]
        public dynamic Update([FromBody] dynamic request)
        {

            var data = JObject.Parse(Convert.ToString(request));

            var Product = data.SelectToken("Product");

            int Id = Product.SelectToken("Id");
            string Name = Convert.ToString(Product.SelectToken("Name"));
            string Description = Convert.ToString(Product.SelectToken("Description"));
            int Unit = Product.SelectToken("Unit");
            double UnitPrice = Product.SelectToken("UnitPrice");

            string Token = Convert.ToString(data.SelectToken("token"));
            int UserId = data.SelectToken("UserId");

            var result = _productRepository.Update(Id, Name, Description, Unit, UnitPrice, UserId, Token);

            string jsonData = JsonConvert.SerializeObject(result);

            return Ok(jsonData);
        }

        [HttpPost("api/product/delete")]
        public dynamic Delete([FromBody] dynamic request)
        {

            var data = JObject.Parse(Convert.ToString(request));

            int Id = data.SelectToken("Id");
            string Token = Convert.ToString(data.SelectToken("token"));
            int UserId = data.SelectToken("UserId");

            var result = _productRepository.Delete(Id, UserId, Token);

            return Ok(result);
        }

        [HttpPost("api/product/purchase")]
        public dynamic Purchase([FromBody] dynamic request)
        {

            var data = JObject.Parse(Convert.ToString(request));

            int Id = data.SelectToken("Id");
            int Unit = data.SelectToken("Unit");
            string Token = Convert.ToString(data.SelectToken("token"));
            int UserId = data.SelectToken("UserId");

            var result = _productRepository.Purchase(Id, Unit, UserId, Token);

            return Ok(result);
        }

        [HttpPost("api/product/sale")]
        public dynamic Sale([FromBody] dynamic request)
        {

            var data = JObject.Parse(Convert.ToString(request));

            int Id = data.SelectToken("Id");
            int Unit = data.SelectToken("Unit");
            string Token = Convert.ToString(data.SelectToken("token"));
            int UserId = data.SelectToken("UserId");

            var result = _productRepository.Sale(Id, Unit, UserId, Token);

            return Ok(result);
        }

        [HttpPost("api/product/getAll")]
        public dynamic GetAll()
        {

            var result = _productRepository.GetAllProducts();

            return Ok(result);
        }

    }
}
