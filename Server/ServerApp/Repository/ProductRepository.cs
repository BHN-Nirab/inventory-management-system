using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ServerApp.Model;

namespace ServerApp.Repository
{
    public class ProductRepository : DatabaseRepository
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly LogRepository _logRepository = new LogRepository();
        public dynamic Add(string Name, string Description, int Unit, double UnitPrice, int userId, string UserName, string Token)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Token))
                return new
                {
                    StatusCode = 202,
                    Product = new Product()
                };

            int isValidSession = _userRepository.IsValidSession(userId, Token);

            if (isValidSession == 202)
                return new
                {
                    StatusCode = 202,
                    Product = new Product()
                };

            Product product = new Product { 
            Name = Name,
            Description = Description,
            Unit = Unit,
            UnitPrice = UnitPrice,
            UserName = UserName
            };

            DatabaseContext.Products.Add(product);
            DatabaseContext.SaveChanges();

            return new {
                StatusCode = 200,
                Product = product
            };

        }


        public dynamic Update(int Id, string Name, string Description, int Unit, double UnitPrice, int userId, string Token)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Token))
                return new
                {
                    StatusCode = 202,
                    Product = new Product()
                };

            int isValidSession = _userRepository.IsValidSession(userId, Token);

            if (isValidSession == 202)
                return new
                {
                    StatusCode = 202,
                    Product = new Product()
                };

            Product product = DatabaseContext.Products.SingleOrDefault(element => element.Id == Id);

            if(product==null)
                return new
                {
                    StatusCode = 202,
                    Product = new Product()
                };

            product.Id = Id;
            product.Name = Name;
            product.Description = Description;
            product.Unit = Unit;
            product.UnitPrice = UnitPrice;

            DatabaseContext.Products.Update(product);
            DatabaseContext.SaveChanges();

            return new
            {
                StatusCode = 200,
                Product = product
            };

        }

        public int Delete(int Id, int userId, string Token)
        {
            if (string.IsNullOrEmpty(Token))
                return 202;

            int isValidSession = _userRepository.IsValidSession(userId, Token);

            if (isValidSession == 202)
                return 202;

            Product product = DatabaseContext.Products.SingleOrDefault(element => element.Id == Id);

            if (product == null)
                return 202;

            DatabaseContext.Products.Remove(product);
            DatabaseContext.SaveChanges();

            return 200;
        }

        public int Purchase(int Id, int Unit, int userId, string Token)
        {
            if (string.IsNullOrEmpty(Token))
                return 202;

            int isValidSession = _userRepository.IsValidSession(userId, Token);

            if (isValidSession == 202)
                return 202;

            Product product = DatabaseContext.Products.SingleOrDefault(element => element.Id == Id);

            if (product == null)
                return 202;

            product.Unit += Unit;

            _logRepository.CreateLog(userId, Id, 0, Unit);

            DatabaseContext.Products.Update(product);
            DatabaseContext.SaveChanges();

            return 200;
        }

        public int Sale(int Id, int Unit, int userId, string Token)
        {
            if (string.IsNullOrEmpty(Token))
                return 202;

            int isValidSession = _userRepository.IsValidSession(userId, Token);

            if (isValidSession == 202)
                return 202;

            Product product = DatabaseContext.Products.SingleOrDefault(element => element.Id == Id);

            if (product == null)
                return 202;
            else if (Unit > product.Unit)
                return 202;

            product.Unit -= Unit;

            _logRepository.CreateLog(userId, Id, 1, Unit);

            DatabaseContext.Products.Update(product);
            DatabaseContext.SaveChanges();

            return 200;
        }

        public dynamic GetAllProducts()
        {
        
            List<Product> products = DatabaseContext.Products.ToList();

            return new
            {
                StatusCode = 200,
                Products = products
            };
        }

    }
}
