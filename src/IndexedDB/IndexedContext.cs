
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BlackDigital.Blazor.IndexedDB
{
    public abstract class IndexedContext
    {
        public IndexedContext(IJSRuntime jsRuntime)
        {
            JSRuntime = jsRuntime;
            StartIndexedDB();
        }

        private JSIndexedDB? IndexedDB;
        private readonly IJSRuntime JSRuntime;


        private void StartIndexedDB()
        {
            DBBuilder builder = new(this.GetType().Name);
            builder = CreateIndexedDB(builder);
            IndexedDB = new JSIndexedDB(JSRuntime, builder.Name, builder);
            SetIndexedSetProperties(builder);
            //await IndexedDB.CreateDatabase(builder);
        }

        private void SetIndexedSetProperties(DBBuilder builder)
        {
            var properties = GetIndexedSetProperties();

            foreach (var property in properties)
            {
                var type = property.PropertyType.GetGenericArguments()[0];
                var store = builder.Stores.Where(store => store.Name == type.Name).FirstOrDefault();
                
                if (store != null)
                {
                    BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
                    object[] parameters = new object[] { store.Name, IndexedDB };
                    var inst = Activator.CreateInstance(property.PropertyType, flags, null, parameters, null);

                    property.SetValue(this, inst);
                }
            }
        }

        private IEnumerable<PropertyInfo> GetIndexedSetProperties()
        {
            return this.GetType().GetProperties()
                                 .Where(p => p.PropertyType.IsGenericType
                                        && p.PropertyType.GetGenericTypeDefinition() == typeof(IndexedSet<>));
        }

        public virtual DBBuilder CreateIndexedDB(DBBuilder builder)
        {
            var properties = GetIndexedSetProperties();

            foreach (var property in properties)
            {
                var type = property.PropertyType.GetGenericArguments()[0];
                var store = builder.AddStore(type);
                CreateStore(type, store);
            }

            return builder;
        }

        public virtual DBStoreBuilder CreateStore(Type storeType, DBStoreBuilder storeBuilder)
        {
            var keyProperties = storeType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                          .Where(p => p.CanRead && p.CanWrite
                                          && p.GetAttributes<KeyAttribute>().Length > 0)
                                          .ToArray();

            foreach (var keyProperty in keyProperties)
            {
                if (Array.IndexOf(keyProperties, keyProperty) == 0)
                {
                    var keyPath = char.ToLower(keyProperty.Name[0]) + keyProperty.Name.Substring(1);
                    storeBuilder.AddKeyPath(keyPath);
                }
                else
                {
                    var dbIndex = storeBuilder.AddIndex(keyProperty.Name);
                    dbIndex = CreateIndex(storeType, keyProperty, dbIndex);
                }
            }

            return storeBuilder;
        }

        public virtual DBIndexBuilder CreateIndex(Type storeType, PropertyInfo propertyIndex, DBIndexBuilder indexBuilder)
        {
            return indexBuilder;
        }
    }
}
