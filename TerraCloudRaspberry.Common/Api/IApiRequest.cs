namespace TerraCloudRaspberry.Common.Api
{
    public interface IApiRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<TResult> PostAsync<TResult, TBody>(string endpoint, TBody body);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<ErrorResponse?> OnlyPostAsync<TBody>(string endpoint, TBody body);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<ErrorResponse?> OnlyPatchAsync<TBody>(string endpoint, TBody body);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        Task<TResult> GetAsync<TResult>(string endpoint);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        Task<TResult> PutAsync<TResult>(string endpoint);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        Task<ErrorResponse?> DeleteAsync(string endpoint);
    }
}
