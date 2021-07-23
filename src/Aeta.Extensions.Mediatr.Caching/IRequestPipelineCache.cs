using System;
using System.Threading.Tasks;
using MediatR;

namespace Aeta.Extensions.Mediatr.Caching
{
    public interface IRequestPipelineCache<TRequest>
        where TRequest : IBaseRequest
    {
        Task<TItem> GetItemAsync<TItem>(string key, Func<Task<TItem>> getItemOnMiss);
    }
}