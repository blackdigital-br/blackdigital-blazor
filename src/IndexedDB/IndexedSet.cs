
using System.Collections;
using System.ComponentModel;

namespace BlackDigital.Blazor.IndexedDB
{
    public sealed class IndexedSet<TEntity> : IListSource
    {
        internal IndexedSet(string name, JSIndexedDB jsIndexedDB)
        {
            Name = name;
            JsIndexedDB = jsIndexedDB;
        }

        private readonly string Name;

        private readonly JSIndexedDB JsIndexedDB;

        public bool ContainsListCollection => false;

        public IList GetList() => GetListAsync().Result;

        public ValueTask<List<TEntity>> GetListAsync() => JsIndexedDB.GetAllAsync<TEntity>(Name);
        public ValueTask<TEntity> GetAsync(object key) => JsIndexedDB.GetAsync<TEntity>(Name, key);
        public ValueTask InsertAsync(TEntity entity) => JsIndexedDB.InsertAsync(Name, entity);
        public ValueTask DeleteAsync(object key) => JsIndexedDB.DeleteAsync<TEntity>(Name, key);
        public ValueTask SaveAsync(object key, TEntity entity) => JsIndexedDB.SaveAsync(Name, key, entity);
        public ValueTask ClearAll() => JsIndexedDB.ClearAll(Name);
    }
}
