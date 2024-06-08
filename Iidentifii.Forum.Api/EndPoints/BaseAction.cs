using FastEndpoints;
using Iidentifii.Forum.Api.EndPoints.Post.Create;
using System.Security.Claims;

namespace Iidentifii.Forum.Api.EndPoints
{
    public abstract class BaseAction<TRequest, TResponse> : Endpoint<TRequest, TResponse> where TRequest : notnull
    {
        public int UserId => GetUserId();
        private int GetUserId()
        {
            var userIdString = User.FindFirst("UserId")?.Value;
            var parsed = int.TryParse(userIdString, out int userId);
            if (!parsed)
                throw new Exception("Unable to determine UserId");
            return userId;
        }
    }
}
