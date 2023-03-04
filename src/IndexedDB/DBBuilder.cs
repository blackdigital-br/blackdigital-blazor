
namespace BlackDigital.Blazor.IndexedDB
{
    public class DBBuilder
    {
        internal DBBuilder(string name, int version)
        {
            Name = name;
            Version = version;
            Objects = new List<DBStoreBuilder>();
        }

        public string Name { get; set; }

        public int Version { get; set; }

        public List<DBStoreBuilder> Objects { get; set; }

        public DBStoreBuilder AddStore<T>()
        {
            return AddStore(typeof(T));
        }

        public DBStoreBuilder AddStore(Type type)
        {
            var builder = new DBStoreBuilder(type);
            Objects.Add(builder);

            return builder;
        }
    }
}
