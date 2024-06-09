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

namespace Iidentifii.Forum.Library.Tags
{
    public class TagService : ITagService
    {
        private IDbConnectionFactory _dbConnectionFactory;

        public TagService(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public void Create(int postId, int tagId, int userId)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "EXEC TagCreate @PostId, @TagId, @UserId";
                var parameters = new DynamicParameters();
                parameters.Add("PostId", postId);
                parameters.Add("TagId", tagId);
                parameters.Add("UserId", userId);
                connection.Execute(sql, parameters);
            }
        }

        public int CreateTagLU(string name, string? description)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "EXEC TagLUCreate @Name, @Description, @Id OUTPUT";
                var parameters = new DynamicParameters();
                parameters.Add("Name", name);
                parameters.Add("Description", description);
                parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute(sql, parameters);
                var postId = parameters.Get<int>("Id");
                return postId;
            }
        }

        public IEnumerable<TagLU>? GetTagLUs()
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var tags = connection.Query<TagLU>("SELECT * FROM dbo.TagLU",
                   new
                   {},
                commandType: CommandType.Text);
                return tags;
            }
        }
    }
}
