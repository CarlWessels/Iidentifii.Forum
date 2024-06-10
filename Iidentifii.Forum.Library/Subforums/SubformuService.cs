using Dapper;
using Iidentifii.Forum.Library.Database;
using Iidentifii.Forum.Library.Models;
using Iidentifii.Forum.Library.Subforums.Models;
using Newtonsoft.Json;
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

        public List<SubforumView>? GetAll(int pageNumber, int pageSize)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "EXEC SubforumGet @PageNumber, @PageSize, @Output OUTPUT";
                var parameters = new DynamicParameters();
                parameters.Add("PageNumber", pageNumber);
                parameters.Add("PageSize", pageSize);
                parameters.Add("Output", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                connection.Execute(sql, parameters);
                var output = parameters.Get<string>("Output");

                var postView = JsonConvert.DeserializeObject<List<SubforumView>>(output);

                return postView;
            }
        }
    }
}
