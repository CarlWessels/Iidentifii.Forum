namespace Iidentifii.Forum.Api.EndPoints
{
    public class BaseDataResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
