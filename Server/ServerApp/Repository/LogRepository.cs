using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ServerApp.Model;

namespace ServerApp.Repository
{
    public class ViewModelLog
    {
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Unit { get; set; }
        public double UnitPrice { get; set; }
        public string TransactionType { get; set; }
        public DateTime Created { get; set; }
    }
    public class LogRepository : DatabaseRepository
    {
        private readonly UserRepository _userRepository = new UserRepository();
        public int CreateLog(int userId, int productId, int transactionType, int unit)
        {
            Log log = new Log {
                ProductId = productId,
                TransactionType = transactionType,
                Unit = unit,
                UserId = userId,
                Created = DateTime.Now,
            };

            DatabaseContext.Logs.Add(log);
            DatabaseContext.SaveChanges();

            return 200;
        }


        public dynamic GetDailyReport(DateTime day, int userId, string token)
        {
            int isValidSession = _userRepository.IsValidSession(userId, token);
            if(isValidSession==202)
            {
                return new {
                StatusCode = 202,
                Reports = new Log()
                };
            }

            List<Log> logs = DatabaseContext.Logs.ToList();
            List<ViewModelLog> reports = new List<ViewModelLog> { };

            foreach (Log log in logs)
            {
                if (log.Created.Date == day.Date)
                {
                    User user = DatabaseContext.Users.SingleOrDefault(element => element.Id==log.UserId);
                    Product product = DatabaseContext.Products.SingleOrDefault(element => element.Id==log.ProductId);

                    if(product!=null)
                    {
                        ViewModelLog viewModel = new ViewModelLog
                        {
                            UserName = user.Name,
                            ProductName = product.Name,
                            ProductDescription = product.Description,
                            Unit = product.Unit,
                            UnitPrice = product.UnitPrice,
                            TransactionType = log.TransactionType == 0 ? "Purchase" : "Sale",
                            Created = log.Created
                        };

                        reports.Add(viewModel);
                    }
                    
                
                }
            }

            return new
            {
                StatusCode = 200,
                Reports = reports
            };

        }

        public dynamic GetMonthlyReport(DateTime month, int userId, string token)
        {
            int isValidSession = _userRepository.IsValidSession(userId, token);
            if (isValidSession == 202)
            {
                return new
                {
                    StatusCode = 202,
                    Reports = new Log()
                };
            }

            List<Log> logs = DatabaseContext.Logs.ToList();
            List<ViewModelLog> reports = new List<ViewModelLog> { };

            foreach (Log log in logs)
            {
                if (log.Created.Month == month.Month)
                {
                    User user = DatabaseContext.Users.SingleOrDefault(element => element.Id == log.UserId);
                    Product product = DatabaseContext.Products.SingleOrDefault(element => element.Id == log.ProductId);
                    if(product!=null)
                    {
                        ViewModelLog viewModel = new ViewModelLog
                        {
                            UserName = user.Name,
                            ProductName = product.Name,
                            ProductDescription = product.Description,
                            Unit = product.Unit,
                            UnitPrice = product.UnitPrice,
                            TransactionType = log.TransactionType == 0 ? "Purchase" : "Sale",
                            Created = log.Created
                        };

                        reports.Add(viewModel);
                    }
                    

                }
            }

            return new
            {
                StatusCode = 200,
                Reports = reports
            };

        }
    }
}
