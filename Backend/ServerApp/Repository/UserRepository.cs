using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ServerApp.Model;

namespace ServerApp.Repository
{
    public class UserRepository : DatabaseRepository
    {
        public dynamic CreateAccount(string Name, string Email, string Password)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                return new { 
                StatusCode = 202,
                ErrorMessage = "Field should not be empty!"
                };

            User existingUser = DatabaseContext.Users.SingleOrDefault(user => user.Email == Email);

            if (existingUser == null)
            {
                User newUser = new User
                {
                    Name = Name,
                    Email = Email,
                    Password = Password
                };

                DatabaseContext.Users.Add(newUser);
                DatabaseContext.SaveChanges();
                return new
                {
                    StatusCode = 200,
                    ErrorMessage = "Account Created.Please Login."
                };
            }
            else
                return new
                {
                    StatusCode = 202,
                    ErrorMessage = "User Exist!"
                };

        }

        public dynamic Login(string Email, string Password)
        {

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                var response = new
                {
                    StatusCode = 202,
                    token = "",
                    ErrorMessage = "Field should not be empty!",
                    Id = -1,
                    Name = ""
                };

                return response;
            }

            User authUser = DatabaseContext.Users.SingleOrDefault(user => (user.Email == Email && user.Password == Password) );

            if (authUser == null)
            {
                var response = new
                {
                    StatusCode = 202,
                    token = "",
                    ErrorMessage = "User not found!",
                    Id = -1,
                    Name = ""
                };

                return response;
            }
            else
            {
                string unixTimestamp = Convert.ToString((int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                String data = Email + Password + unixTimestamp;
                String token = GenerateToken(data);

                Console.WriteLine(unixTimestamp);

                var response = new {
                    StatusCode = 200,
                    token,
                    ErrorMessage = "Successfully login",
                    Id = authUser.Id,
                    Name = authUser.Name
                };

                authUser.Token = token;
                DatabaseContext.Users.Update(authUser);
                DatabaseContext.SaveChanges();

                return response;

            }
                

        }

        public string GenerateToken(string stringData)
        {
            byte[] data = Encoding.ASCII.GetBytes(stringData);
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(data);
                return Convert.ToBase64String(hash);
            }
        }


        public int IsValidSession(int userId, string token)
        {
            if (string.IsNullOrEmpty(token))
                return 202;

            User existingUser = DatabaseContext.Users.SingleOrDefault(user => (user.Token == token && user.Id == userId));

            if (existingUser == null)
                return 202;
            else
                return 200;

        }
    }
}
