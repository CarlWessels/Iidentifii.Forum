using Dapper;
using Iidentifii.Forum.Library.Database;
using Iidentifii.Forum.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Iidentifii.Forum.Library.Likes
{
    public class LikeService : ILikeService
    {
        private IDbConnectionFactory _dbConnectionFactory;

        public LikeService(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public void LikeOrUnlike(int postId, int userId)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "EXEC LikeOrUnlike @PostId, @UserId";
                var parameters = new DynamicParameters();
                parameters.Add("PostId", postId);
                parameters.Add("UserId", userId);
                connection.Execute(sql, parameters);
            }
        }
    }
}
