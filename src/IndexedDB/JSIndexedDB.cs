
using Microsoft.JSInterop;

namespace BlackDigital.Blazor.IndexedDB
{
    internal class JSIndexedDB
    {
        internal JSIndexedDB( IJSRuntime jsRuntime, string dBName)
        {
            JSRuntime = jsRuntime;
            DBName = dBName;
        }

        private readonly string DBName;
        private readonly IJSRuntime JSRuntime;
        private const string BaseMethodPath = "window.blackdigital.dbs.{0}";
        private static string Method(string name) => string.Format(BaseMethodPath, name);

        internal ValueTask CreateDatabase(DBBuilder builder)
        {
            return JSRuntime.InvokeVoidAsync(Method("create"), builder);
        }

        internal ValueTask<List<TEntity>> GetAllAsync<TEntity>(string storeName)
        {
            return JSRuntime.InvokeAsync<List<TEntity>>(Method("getAll"), DBName, storeName);
        }

        internal ValueTask<TEntity> GetAsync<TEntity>(string storeName, object key)
        {
            return JSRuntime.InvokeAsync<TEntity>(Method("get"), DBName, storeName, key);
        }

        internal ValueTask InsertAsync<TEntity>(string storeName, TEntity entity)
        {
            return JSRuntime.InvokeVoidAsync(Method("insert"), DBName, storeName, entity);
        }

        internal ValueTask DeleteAsync<TEntity>(string storeName, object key)
        {
            return JSRuntime.InvokeVoidAsync(Method("delete"), DBName, storeName, key);
        }

        internal ValueTask SaveAsync<TEntity>(string storeName, object key, TEntity entity)
        {
            return JSRuntime.InvokeVoidAsync(Method("save"), DBName, storeName, key, entity);
        }

        internal ValueTask ClearAll(string storeName)
        {
            return JSRuntime.InvokeVoidAsync(Method("clear"), DBName, storeName);
        }
    }
}
