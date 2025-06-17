
using Microsoft.JSInterop;

namespace BlackDigital.Blazor.IndexedDB
{
    internal class JSIndexedDB
    {
        internal JSIndexedDB( IJSRuntime jsRuntime, string dBName, DBBuilder builder)
        {
            JSRuntime = jsRuntime;
            DBName = dBName;
            _builder = builder;
            _loaded = false;
        }

        private bool _loaded;
        private readonly string DBName;
        private readonly DBBuilder _builder;
        private readonly IJSRuntime JSRuntime;
        private const string BaseMethodPath = "window.blackdigital.dbs.{0}";
        private static string Method(string name) => string.Format(BaseMethodPath, name);

        private async ValueTask ValidateCreationAsync()
        {
            if (!_loaded)
            {
                await CreateDatabase();
                _loaded = true;
            }
        }

        internal ValueTask CreateDatabase()
        {
            return JSRuntime.InvokeVoidAsync(Method("create"), _builder);
        }

        internal async ValueTask<List<TEntity>> GetAllAsync<TEntity>(string storeName)
        {
            await ValidateCreationAsync();
            return await JSRuntime.InvokeAsync<List<TEntity>>(Method("getAll"), DBName, storeName);
        }

        internal async ValueTask<TEntity> GetAsync<TEntity>(string storeName, object key)
        {
            await ValidateCreationAsync();
            return await JSRuntime.InvokeAsync<TEntity>(Method("get"), DBName, storeName, key);
        }

        internal async ValueTask InsertAsync<TEntity>(string storeName, TEntity entity)
        {
            await ValidateCreationAsync();
            await JSRuntime.InvokeVoidAsync(Method("insert"), DBName, storeName, entity);
        }

        internal async ValueTask DeleteAsync<TEntity>(string storeName, object key)
        {
            await ValidateCreationAsync();
            await JSRuntime.InvokeVoidAsync(Method("delete"), DBName, storeName, key);
        }

        internal async ValueTask SaveAsync<TEntity>(string storeName, object key, TEntity entity)
        {
            await ValidateCreationAsync();
            await JSRuntime.InvokeVoidAsync(Method("save"), DBName, storeName, key, entity);
        }

        internal async ValueTask ClearAll(string storeName)
        {
            await ValidateCreationAsync();
            await JSRuntime.InvokeVoidAsync(Method("clear"), DBName, storeName);
        }
    }
}
