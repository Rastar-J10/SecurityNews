using Newtonsoft.Json;
using System;

namespace Practice.ResponseModels
{
    /// <summary>
    /// 统一返回基础model
    /// </summary>
    [Serializable]
    public class ResBaseModel
    {
        /// <summary>
        /// 信息
        /// </summary>
        [JsonProperty("message")]
        public virtual string Message { get; set; } = string.Empty;

        /// <summary>
        /// 代码当HttpStatusCode==200（HttpStatusCode.OK）时， 0 正常 1错误。其他错误用HttpStatusCode
        /// </summary>
        [JsonProperty("status")]
        public virtual bool Status { get; set; }
    }

    /// <summary>
    /// 接口统一返回model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ResModel<T> : ResBaseModel
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// 通用model扩展方法
    /// </summary>
    [Serializable]
    public static class ResModel
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        /// <returns></returns>
        public static ResModel<T> Success<T>(T data, string message = "success") => new ResModel<T>
        {
            Status = true,
            Message = message,
            Data = data
        };

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <returns></returns>
        public static ResModel<T> Failure<T>(string message = "failure") => new ResModel<T>
        {
            Status = false,
            Message = message,
            Data = default
        };
    }
}
