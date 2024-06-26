﻿using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using Dapper;
using Iidentifii.Forum.Library.Database;
using Iidentifii.Forum.Library.Models;

namespace Iidentifii.Forum.Library.Auth
{
    public class UserManager : IUserManager
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UserManager(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public (int, string)? Login(string email, string password)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var user = connection.Query<User>("SELECT * FROM dbo.UserLogin_TVF(@Email, @Password)",
                   new
                   {
                       Email = email,
                       Password = password
                   },
                commandType: CommandType.Text).SingleOrDefault();
                if (user == null)
                    return default;
                return (user.Id!.Value!, user.Role!);
            }
        }

        public int Create(string name, string email, string password)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "EXEC UserCreate @Name, @Email, @Password, @Id OUTPUT";
                var parameters = new DynamicParameters();
                parameters.Add("Name", name);
                parameters.Add("Email", email);
                parameters.Add("Password", password);
                parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute(sql, parameters);
                var userId = parameters.Get<int>("Id");
                return userId;
            }
        }

        public void ResetPassword()
        {
            throw new NotImplementedException();
        }

        public void CreateModerator(int userId)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "EXEC ModeratorCreate @UserId";
                var parameters = new DynamicParameters();
                parameters.Add("UserId", userId);
                connection.Execute(sql, parameters);
            }
        }
    }
}
