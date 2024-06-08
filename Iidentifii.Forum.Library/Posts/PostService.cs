using Iidentifii.Forum.Library.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Iidentifii.Forum.Library.Models;
using System.Data;

namespace Iidentifii.Forum.Library.Posts
{
    public class PostService : IPostService
    {
        private IDbConnectionFactory _dbConnectionFactory;

        public PostService(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public int CreatePost(int subforumId, string title, string content, int userId)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "EXEC PostCreate @SubforumId, @Title, @Content, @UserId, @Id OUTPUT";
                var parameters = new DynamicParameters();
                parameters.Add("SubforumId", subforumId);
                parameters.Add("Title", title);
                parameters.Add("Content", content);
                parameters.Add("UserId", userId);
                parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute(sql, parameters);
                var postId = parameters.Get<int>("Id");
                return postId;
            }
        }
    }
}
