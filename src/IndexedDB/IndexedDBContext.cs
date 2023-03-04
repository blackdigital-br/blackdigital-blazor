
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;

namespace BlackDigital.Blazor.IndexedDB
{
    public abstract class IndexedDBContext
    {
        public IndexedDBContext(int currentVersion)
        {
            CurrentVersion = currentVersion;
        }

        public readonly int CurrentVersion;

        public async void StartIndexdDB()
        {
            DBBuilder builder = new(this.GetType().Name, CurrentVersion);
            builder = CreateIndexedDB(builder);
            
        }

        public virtual DBBuilder CreateIndexedDB(DBBuilder builder)
        {
            var properties = this.GetType().GetProperties()
                                           .Where(p => p.PropertyType.IsGenericType 
                                                    && p.PropertyType.GetGenericTypeDefinition() == typeof(IndexedSet<>));


            foreach (var property in properties)
            {
                var type = property.PropertyType.GetGenericArguments()[0];
                var store = builder.AddStore(type);

                store = CreateStore(type, store);
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
                    storeBuilder.AddKeyPath(keyProperty.Name);
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
