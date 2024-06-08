using Dapper;
using Iidentifii.Forum.Library.Database;
using Iidentifii.Forum.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Iidentifii.Forum.Library.Subforums
{
    public class SubformuService : ISubforumService
    {
        private IDbConnectionFactory _dbConnectionFactory;

        public SubformuService(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public int Create(string name, string description)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "EXEC SubforumCreate @Name, @Description, @Id OUTPUT";
                var parameters = new DynamicParameters();
                parameters.Add("Name", name);
                parameters.Add("Description", description);
                parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute(sql, parameters);
                var postId = parameters.Get<int>("Id");
                return postId;
            }
        }
    }
}
