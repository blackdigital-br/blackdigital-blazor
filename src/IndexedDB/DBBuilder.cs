
namespace BlackDigital.Blazor.IndexedDB
{
    public class DBBuilder
    {
        internal DBBuilder(string name)
        {
            Name = name;
            Version = 1;
            Stores = new List<DBStoreBuilder>();
        }

        public string Name { get; private set; }

        public int Version { get; private set; }

        public List<DBStoreBuilder> Stores { get; set; }

        public DBBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public DBBuilder WithVersion(int version)
        {
            Version = version;
            return this;
        }

        public DBStoreBuilder AddStore<T>()
        {
            return AddStore(typeof(T));
        }

        public DBStoreBuilder AddStore(Type type)
        {
            var builder = new DBStoreBuilder(type);
            Stores.Add(builder);

            return builder;
        }
    }
}
