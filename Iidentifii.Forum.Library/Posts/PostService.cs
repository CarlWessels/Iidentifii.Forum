using Iidentifii.Forum.Library.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Iidentifii.Forum.Library.Models;
using System.Data;
using Iidentifii.Forum.Library.Posts.Models;
using System.Reflection.Metadata;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace Iidentifii.Forum.Library.Posts
{
    public class PostService : IPostService
    {
        private IDbConnectionFactory _dbConnectionFactory;

        public PostService(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public int Create(int subforumId, string title, string content, int userId)
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

        public PostView? Get(int postId, int pageNumber, int pageSize, PostSortingOptions? sortingOptions, SortingDirection sortingDirection, PostFilterOptions? postFilterOptions)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "EXEC PostGet @PostId, @PageNumber, @PageSize, @StartDate, @EndDate, @AuthorId, @TagId, @SortBy, @SortOrder, @Output OUTPUT";
                var parameters = new DynamicParameters();
                parameters.Add("PostId", postId);
                parameters.Add("PageNumber", pageNumber);
                parameters.Add("PageSize", pageSize);
                parameters.Add("StartDate", postFilterOptions?.StartDate);
                parameters.Add("EndDate", postFilterOptions?.EndDate);
                parameters.Add("AuthorId", postFilterOptions?.AuthorId);
                parameters.Add("TagId", postFilterOptions?.TagId);
                parameters.Add("SortBy", sortingOptions);
                parameters.Add("SortOrder", sortingDirection.ToString().ToUpper(), direction: ParameterDirection.Input);
                parameters.Add("Output", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                connection.Execute(sql, parameters);
                var output = parameters.Get<string>("Output");

                var postView = JsonConvert.DeserializeObject< PostView >(output);

                return postView;
            }
        }


        public List<PostView>? GetAll(int pageNumber, int pageSize, PostSortingOptions? sortingOptions, SortingDirection sortingDirection, PostFilterOptions? postFilterOptions)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "EXEC PostGetAll @PageNumber, @PageSize, @StartDate, @EndDate, @AuthorId, @TagId, @SortBy, @SortOrder, @Output OUTPUT";
                var parameters = new DynamicParameters();
                parameters.Add("PageNumber", pageNumber);
                parameters.Add("PageSize", pageSize);
                parameters.Add("StartDate", postFilterOptions?.StartDate);
                parameters.Add("EndDate", postFilterOptions?.EndDate);
                parameters.Add("AuthorId", postFilterOptions?.AuthorId);
                parameters.Add("TagId", postFilterOptions?.TagId);
                parameters.Add("SortBy", sortingOptions);
                parameters.Add("SortOrder", sortingDirection.ToString().ToUpper(), direction: ParameterDirection.Input);
                parameters.Add("Output", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
                connection.Execute(sql, parameters);
                var output = parameters.Get<string>("Output");

                var postView = JsonConvert.DeserializeObject<List<PostView>>(output);

                return postView;
            }
        }
    }
}
