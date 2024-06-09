using Dapper;
using Iidentifii.Forum.Library.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Iidentifii.Forum.Library.Comments
{
    public class CommentService : ICommentService
    {
        private IDbConnectionFactory _dbConnectionFactory;
    

        public CommentService(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public int CreateComment(int postId, string comment, int userId)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "EXEC CommentCreate @PostId, @Comment, @UserId, @Id OUTPUT";
                var parameters = new DynamicParameters();
                parameters.Add("PostId", postId);
                parameters.Add("Comment", comment);
                parameters.Add("UserId", userId);
                parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute(sql, parameters);
                var commentId = parameters.Get<int>("Id");
                return commentId;
            }
        }
    }
}
